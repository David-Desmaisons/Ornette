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

export { join, track };
