<template>
  <v-card class="bar">
    <cover v-if="album" :album="album" size="54px" />

    <div v-if="album" class="album-artist">{{ album.Artists | join }}</div>

    <span v-if="player.CurrentTrack" class="track">{{ metaData.Name }}</span>

    <position class="position" :player="player" />

    <textualPosition class="textual-position" :player="player" />

    <playControl class="play-control" :player="player" />

    <volume class="volume-control" v-model="player.Volume" :asButton="false" />
  </v-card>
</template>
<script>
import cover from "../cover/cover";
import volume from "./basic/volume";
import position from "./position/basicPosition";
import textualPosition from "./position/textualPosition";

import playControl from "./playControl/completeCompactPlay";
import { album } from "@/filter/track";

export default {
  name: "compact-bar-control",
  props: {
    player: {
      type: Object,
      required: true
    }
  },
  computed: {
    album() {
      return album(this.player.CurrentTrack);
    },
    metaData() {
      return this.player.CurrentTrack.MetaData;
    }
  },
  components: {
    cover,
    volume,
    playControl,
    position,
    textualPosition
  }
};
</script>
<style lang="sass" scoped>
$height: 54px
$position-height: 4px
$mid-heigth: $height/2

.bar
  padding: 0 5px 0 0px
  height: $height + $position-height
  display: grid
  grid-template-columns: $height minmax(0,1fr) minmax(auto, 4fr) auto minmax(90px, 1fr)
  grid-template-rows: $position-height $mid-heigth $mid-heigth
  grid-template-areas: "position position position position position" "image track play-control textual-position volume-control" "image album-artist play-control textual-position volume-control"
  font-size: 10px
  grid-gap: 0px 5px
  min-width: 360px

  .album-artist
    @include text-not-wrapped
    margin-bottom: 5px
    grid-area: album-artist
    align-self: start
    opacity: $subtopic-opacity

  .track
    @include text-not-wrapped
    margin-top: 5px
    grid-area: track
    align-self: end
    font-weight: bold

  .volume-control
    grid-area: volume-control
    align-self: center
    justify-self: stretch

  .position
    grid-area: position
    align-self: center
    z-index: 0

  .textual-position
    grid-area: textual-position
    align-self: center

  .image
    grid-area: image

  .play-control
    grid-area: play-control
    align-self: center
    justify-self: center

  ::v-deep
    .position
      .vue-slider-rail
        background-color: $light-main-color

    .vue-slider-dot-tooltip
      z-index: 10
</style>
