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
      TrackNumber: {Position}
    }
  } = value;
  return `${Position} - ${Artists.join(", ")} - ${Name} (${timeSpan(
    Duration
  )})`;
}

export { track };
