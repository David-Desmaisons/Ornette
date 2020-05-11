<template>
  <v-card class="simple-player">
    <p class="time">{{ player.PositionInSeconds | formatTime("--:--") }}</p>

    <marquee class="track-name">{{ player.CurrentTrack | track }}</marquee>

    <position dot class="position" :player="player" />

    <play-control :player="player" class="control" />

    <play-mode :player="player" class="control-minor" />

    <volume class="volume" vertical v-model="player.Volume" />
  </v-card>
</template>

<script>
import volume from "./volume";
import playMode from "./playMode";
import playControl from "./playControl";
import position from "./position/position";

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
    position,
    volume
  }
};
</script>

<style lang="sass">
$themeColor: orange
@import '~vue-slider-component/lib/theme/default.scss'
</style>

<style lang="sass" scoped>
.simple-player
  padding: 12px
  display: grid
  grid-template-columns: 2fr auto 1fr
  grid-template-rows: 30px 32px auto
  grid-template-areas: "time track volume" "position position volume" "playmode play volume"
  grid-column-gap: 5px
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

  .position
    margin-left: 0px
    margin-right: 0px
    grid-area: position

  .volume
    height: 100%
    grid-area: volume

  .track-name
    width: 100%
    grid-area: track
    place-self: center

  .control
    grid-area: play

  .control-minor
    grid-area: playmode
</style>
