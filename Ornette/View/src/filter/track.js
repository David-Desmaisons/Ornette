import { timeSpan } from "./time";

function join(value, separator = ",") {
  return value.join(separator);
}

function trackName(value) {
  if (value === null) {
    return "";
  }
  const {
    MetaData: {
      Album: { Artists },
      Duration,
      Name,
      TrackNumber: { Position }
    }
  } = value;
  return `${Position} - ${join(Artists)} - ${Name} (${timeSpan(Duration)})`;
}

function album(value) {
  if (value === null) {
    return null;
  }
  const {
    MetaData: { Album }
  } = value;
  return Album;
}

function albumImage(value, index = 0) {
  if (value === null) {
    return null;
  }
  const { Images } = value;
  return Images.length > 0 ? Images[index].Uri : null;
}

function albumHasImage(value) {
  const { Images } = value;
  return Images.length > 0;
}

export { album, albumImage, albumHasImage, join, trackName };
