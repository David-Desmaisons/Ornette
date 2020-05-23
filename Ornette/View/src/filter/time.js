function format(value, decimal = 2) {
  return String(value).padStart(decimal, "0");
}

function timeSpan(value, type = "M") {
  if (value === null) {
    return null;
  }
  const { Minutes, Hours, Seconds, Milliseconds } = value;
  const addMinutes = Hours * 60;
  const formattedMinute = `${format(addMinutes + Minutes)}:${format(Seconds)}`;
  return type === "M"
    ? formattedMinute
    : `${formattedMinute}:${format(Milliseconds, 3)}`;
}

function totalSeconds(value, defaultValue = 0) {
  if (value === null) {
    return defaultValue;
  }
  const {
    MetaData: { Duration: { TotalSeconds } }
  } = value;
  return Math.round(TotalSeconds);
}

function formatTime(value, defaultValue = null) {
  if (value === null) {
    return defaultValue;
  }
  const minutes = Math.trunc(value / 60);
  const seconds = value % 60;
  return `${format(minutes)}:${format(seconds)}`;
}

export { timeSpan, formatTime, totalSeconds };
