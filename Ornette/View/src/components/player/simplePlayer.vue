<template>
  <div class="simple-player">
    <icon-button icon="mdi-skip-backward" :command="player.Back" />

    <icon-button icon="mdi-pause" :command="player.Pause" />
    
    <icon-button icon="mdi-play" :command="player.Play" />

    <icon-button icon="mdi-stop" :command="player.Stop" />

    <icon-button icon="mdi-skip-forward" :command="player.Next" />

    <p class="full">{{ player.CurrentTrack | track }}</p>
    
    <v-slider
      v-if="player.CurrentTrack"
      v-model="player.PositionInSeconds"
      :max="player.CurrentTrack | totalSeconds"
      class="full slider"
      :dense="true"
    />
    
    <p>{{ player.PositionInSeconds | formatTime }}</p>
  </div>
</template>

<script>
import iconButton from "../iconButton";

export default {
  props: {
    player: {
      required: true,
      type: Object
    }
  },
  components: {
    iconButton
  }
};
</script>

<style scoped="true">
.simple-player {
  width: 100px;
  height: 50px;
  display: grid;
  grid-template-columns: 1fr 1fr 1fr 1fr 1fr;
  grid-template-rows: 1fr 1fr;
}

.simple-player div {
  align-self: center;
  place-self: center;
}

.simple-player .full {
  grid-column: 1 / span 5;
  align-self: center;
  place-self: center;
}

.simple-player p {
  font-size: 10px;
}

.simple-player .slider {
  width: 100%;
}
</style>
