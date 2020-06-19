function onCommand({ id }) {
  const {
    context: { vm }
  } = onCommand;
  const player = getPlayer(vm);
  switch (id) {
    case 13:
      return onPrevious(player);

    case 14:
      return onNext(player);

    case 15:
      return onPause(player);

    case 16:
      return onPlay(player);

    case 17:
      return onStop(player);
  }
}

let playId;

function onPlay(player) {
  if (playId) {
    return;
  }
  player.State = {
    intValue: 4
  };
  playId = setInterval(() => {
    player.PositionInSeconds++;
  }, 1000);
}

function onPause(player) {
  player.State = {
    intValue: 3
  };
  clearInterval(playId);
  playId = null;
}

function onStop(player) {
  player.State = {
    intValue: 2
  };
  player.PositionInSeconds = 0;
  onPause();
}

function onNext(player) {
  changeTrack(player, +1);
}

function onPrevious(player) {
  changeTrack(player, -1);
}

function changeTrack(player, increment) {
  player.PositionInSeconds = 0;
  const index = trackIndex(player) + increment;
  player.CurrentTrack = getTrack(player, index);
}

function getTrack(player, index) {
  const adjustedIndex =
    index < 0 ? 0 : index >= player.Tracks.length ? 0 : index;
  return player.Tracks[adjustedIndex];
}

function trackIndex(player) {
  return player.Tracks.indexOf(player.CurrentTrack);
}

const getPlayer = vm => vm.CurrentViewModel.Player;

export { onCommand };
