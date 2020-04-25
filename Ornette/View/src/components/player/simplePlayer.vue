<template>
  <div class="simple-player">

    <p class="time">{{ player.PositionInSeconds | formatTime }}</p>

    <marquee class="track-name">{{ player.CurrentTrack | track }}</marquee>

    <icon-button icon="mdi-skip-backward" :command="player.Back" />

    <icon-button icon="mdi-play" :command="player.Play" />

    <icon-button icon="mdi-pause" :command="player.Pause" />  

    <icon-button icon="mdi-stop" :command="player.Stop" />

    <icon-button icon="mdi-skip-forward" :command="player.Next" />

    <v-slider
      v-if="player.CurrentTrack"
      v-model="player.PositionInSeconds"
      :max="player.CurrentTrack | totalSeconds"
      class="full"
      :dense="true"
    />
    
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
  grid-template-rows: 20px 1fr auto;
}

.simple-player div {
  align-self: center;
  place-self: center;
}

.simple-player .full {
  grid-column: 1 / span 5;
  align-self: center;
  place-self: center;
  width: 100%;
}

.simple-player .time {
  grid-column: 1 / span 2;
  margin-bottom: 0;
  font-size: 18px;
  text-align: center;
  align-self: center;
  place-self: center;
}

.simple-player {
  font-size: 10px;
}

.simple-player .track-name {
  width: 100%;
  grid-column: 3 / span 3;
  align-self: center;
  place-self: center;
}
</style>
