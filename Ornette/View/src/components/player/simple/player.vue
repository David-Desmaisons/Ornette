<template>
  <v-card class="simple-player" :class="{ retracted }">
    <p class="time">{{ player.PositionInSeconds | formatTime("--:--") }}</p>

    <marquee class="track-name">{{ player.CurrentTrack | track }}</marquee>

    <position dot class="position" :player="player" />

    <play-control :player="player" class="control" />

    <play-mode :player="player" class="control-minor" />

    <volume
      class="volume"
      v-model="player.Volume"
      vertical
      expandable
      @retractChange="retractChange"
    />
  </v-card>
</template>

<script>
import volume from "./volume";
import playMode from "./playMode";
import playControl from "./playControl";
import position from "./position/position";

export default {
  name: "simplePlayer",
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
  },
  data() {
    return {
      retracted: true
    };
  },
  methods: {
    retractChange(value) {
      this.retracted = value;
    }
  }
};
</script>

<style lang="sass">
$themeColor: orange
@import '~vue-slider-component/lib/theme/default.scss'
</style>

<style lang="sass" scoped>
.simple-player
  width: fit-content
  height: fit-content
  padding: 12px
  display: grid
  grid-template-columns: 60px 150px 30px
  grid-template-rows: 30px 32px 30px
  grid-template-areas: "time track volume" "position position volume" "playmode play volume"
  grid-column-gap: 5px
  font-size: 10px

  &.retracted
    grid-template-areas: "time track track" "position position position" "playmode play volume"
    .position
      grid-area: position

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
    grid-area: none
    grid-column: 1 / 4
    grid-row: 2
    width: 100%

  .volume
    height: 100%
    grid-area: volume
    align-self: end
    transition: all 0.5s ease-in
    z-index: 10

  .track-name
    width: 100%
    grid-area: track
    place-self: center

  .control
    grid-area: play

  .control-minor
    grid-area: playmode
</style>
