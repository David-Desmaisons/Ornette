<template>
  <div class="simple-player">
    <p class="time">{{ player.PositionInSeconds | formatTime }}</p>

    <marquee class="track-name">{{ player.CurrentTrack | track }}</marquee>

    <v-slider
      v-if="player.CurrentTrack"
      v-model="player.PositionInSeconds"
      :max="player.CurrentTrack | totalSeconds"
      class="slider player"
      :dense="true"
    />

    <play-control :player="player" class="control"/>

    <play-mode :player="player" class="control-minor" />

    <volume class="slider volume" v-model="player.Volume" />

  </div>
</template>

<script>
import volume from "./volume";
import playMode from "./playMode";
import playControl from "./playControl";

export default {
  props: {
    player: {
      required: true,
      type: Object
    }
  },
  components: {
    playMode,
    playControl,
    volume
  },
  computed: {
    toogleValue() {
      const res = [];
      if (this.player.AutoReplay) res.push(1);
      if (this.player.RandomPlay) res.push(0);
      return res;
    }
  },
  methods: {
    changeToogle(evt) {
      this.player.AutoReplay = evt.includes(1);
      this.player.RandomPlay = evt.includes(0);
    }
  }
};
</script>

<style scoped="true">
.simple-player {
  width: 210px;
  height: 120px;
  display: grid;
  grid-template-columns: 2fr 3fr 2fr;
  grid-template-rows: 25px 18px 40px 30px;
  grid-column-gap: 2px;
}

.simple-player div {
  align-self: center;
  place-self: center;
}

.simple-player .time {
  grid-column: 1 / span 1;
  grid-row-start: 1;
  margin-bottom: 0;
  font-size: 20px;
  text-align: center;
  align-self: center;
  place-self: center;
}

.simple-player {
  font-size: 10px;
}

.simple-player .slider.player {
  height: 30px;
  width: 100%;
  grid-column: 1 / span 3;
  grid-row-start: 2;
}

.simple-player .slider.volume {
  width: 100%;
  grid-column: 1 / span 4;
  grid-row-start: 4;
}

.simple-player .track-name {
  width: 100%;
  grid-column: 2 / span 2;
  grid-row-start: 1;
  place-self: center;
}

.simple-player .control {
  grid-column: 2 / span 2;
  grid-row-start: 3;
  justify-self: end;
}

.simple-player .control-minor {
  grid-row-start: 3;
  grid-column: 1 / span 1;
}
</style>
