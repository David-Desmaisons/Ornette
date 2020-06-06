import { timeSpan } from "./time";

function join(value, separator = ",") {
  return value.join(separator);
}

function track(value) {
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

function albumImage(value, index = 0) {
  const { Images } = value;
  return Images.length > 0 ? Images[index].Uri : null;
}

function albumHasImage(value) {
  const { Images } = value;
  return Images.length > 0;
}

export { albumImage, albumHasImage, join, track };
