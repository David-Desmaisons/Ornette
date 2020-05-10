function onCommand({ id }) {
  const {
    context: { vm }
  } = onCommand;
  const player = getPlayer(vm);
  switch (id) {
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
  playId = setInterval(() => {
    player.PositionInSeconds++;
  }, 1000);
  window.console.log(playId);
}

function onPause() {
  clearInterval(playId);
  playId = null;
}

function onStop(player) {
  player.PositionInSeconds = 0;
  onPause();
}

const getPlayer = vm => vm.CurrentViewModel.Player;

export { onCommand };
