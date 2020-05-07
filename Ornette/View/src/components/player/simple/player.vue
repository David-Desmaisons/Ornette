<template>
  <v-card class="simple-player">
    <p class="time">{{ player.PositionInSeconds | formatTime }}</p>

    <marquee class="track-name">{{ player.CurrentTrack | track }}</marquee>

    <v-slider
      class="slider player"
      v-if="player.CurrentTrack"
      v-model="player.PositionInSeconds"
      :max="player.CurrentTrack | totalSeconds"
      :hide-details="true"
      :dense="true"
    />

    <play-control :player="player" class="control" />

    <play-mode :player="player" class="control-minor" />

    <volume class="volume" v-model="player.Volume" />
  </v-card>
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

<style lang="sass" scoped>
.simple-player
  padding: 12px
  display: grid
  grid-template-columns: 2fr 5fr
  grid-template-rows: 30px 32px auto
  grid-template-areas: "time track" "position position" "playmode play" "volume volume"
  grid-column-gap: 10px
  font-size: 10px

  div
    align-self: center
    place-self: center

  .time
    grid-area: time
    margin-bottom: 0
    font-size: 20px
    text-align: center
    align-self: center
    place-self: center

  .slider.player
    width: 100%
    grid-area: position
    ::v-deep
      .v-input__slot
        margin-bottom: 0

  .volume
    width: 100%
    grid-area: volume

  .track-name
    width: 100%
    grid-area: track
    place-self: center

  .control
    grid-area: play
    justify-self: end

  .control-minor
    grid-area: playmode
</style>
