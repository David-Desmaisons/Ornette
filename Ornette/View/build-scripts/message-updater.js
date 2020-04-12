const xml2js = require('xml2js');
const fs = require('fs');
const { messagePath, supportedLanguages } = require('./config');

const parser = new xml2js.Parser({ attrkey: "ATTR" });

function existsPath(path) {
  return new Promise((resolve) => {
    fs.exists(path, resolve);
  });
}

function parseFile(result) {
  const { root: { data } } = result;
  const Resource = data.reduce((curr, { ATTR: { name }, value }) => {
    curr[name] = value[0];
    return curr;
  }, {});

  return {
    Resource
  };
}

function getLanguageFromPath(path) {
  return new Promise((resolve, reject) => {
    fs.readFile(path, { encoding: 'utf-8' }, (error, file) => {
      if (error !== null) {
        reject(error);
        return;
      }

      parser.parseString(file, function (parseError, file) {
        if (parseError !== null) {
          reject(parseError);
          return;
        }
        const data = parseFile(file);
        resolve(data);
      });
    });
  });
}

async function getLanguage(language, defaultValue) {
  const path = `../Resource.${language}.resx`;
  const exists = await existsPath(path);
  if (!exists) {
    return { ...defaultValue };
  }
  const data = await getLanguageFromPath(path);
  const result = Object.assign({}, defaultValue, data);
  return result;
}

async function createMessage() {
  const messages = {};
  const defaultValue = await getLanguageFromPath("../Resource.resx");

  for (const language of supportedLanguages) {
    const result = await getLanguage(language, defaultValue);
    messages[language] = result;
  }
  return messages;
}

async function updateMessage() {
  const file = await createMessage();
  fs.writeFile(messagePath, JSON.stringify(file, null, 2), function (err) {
    if (err) {
      console.log(err);
      return;
    }
    console.log("updated message JSON file");
  });
}

updateMessage();