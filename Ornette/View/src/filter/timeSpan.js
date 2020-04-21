function format(value, decimal = 2) {
  return String(value).padStart(decimal, '0');
}

function timeSpan(value, type = "M") {
  if (value === null) {
    return null;
  }
  const { Minutes, Hours, Seconds, Milliseconds } = value;
  const addMinutes = Hours * 60;
  const formattedMinute = `${format(addMinutes + Minutes)}:${format(Seconds)}`;
  return (type === 'M') ? formattedMinute : `${formattedMinute}:${format(Milliseconds, 3)}`;
}

export {
  timeSpan
}