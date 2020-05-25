import { timeSpan } from "./time";

function track(value) {
  if (value === null) {
    return "";
  }
  const {
    MetaData: {
      Album: { Artists },
      Duration,
      Name,
      TrackNumber
    }
  } = value;
  return `${TrackNumber} - ${Artists.join(", ")} - ${Name} (${timeSpan(
    Duration
  )})`;
}

export { track };
