import { timeSpan } from "./time";

function track(value) {
  if (value === null) {
    return "";
  }
  const {
    MetaData: { Duration, Name, TrackNumber }
  } = value;
  return `${TrackNumber}-${Name}-${timeSpan(Duration)}`;
}

export { track };
