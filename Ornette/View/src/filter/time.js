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

function totalSeconds(value) {
  if (value === null) {
    return 0;
  }
  const {
    MetaData: { Duration }
  } = value;
  return Duration.TotalSeconds;
}

function formatTime(value) {
  window.console.log(value);
  if (value === null) {
    return null;
  }
  const minutes = Math.trunc(value / 60);
  const seconds = value % 60;
  return `${format(minutes)}:${format(seconds)}`;
}

export { timeSpan, formatTime, totalSeconds };
