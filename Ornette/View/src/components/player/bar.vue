<template>
  <v-card class="bar">
    <cover v-if="album" :album="album" size="54px" />

    <div v-if="album" class="album-artist">{{ album.Artists | join }}</div>

    <span v-if="player.CurrentTrack" class="track">{{ metaData.Name }}</span>

    <position class="position" :player="player" />

    <playControl class="play-control" :player="player" />

    <playMode class="play-mode" :player="player" :borderless="true" />

    <volume class="volume-control" v-model="player.Volume" :asButton="false" />
  </v-card>
</template>
<script>
import cover from "../cover/cover";
import volume from "./basic/volume";
import position from "./position/completePosition";
import playControl from "./playControl/shortPlay";
import playMode from "./playMode/simple";
import { album } from "@/filter/track";

export default {
  name: "bar-control",
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
    playMode
  }
};
</script>
<style lang="sass" scoped>
$height: 54px
$mid-heigth: $height/2

.bar
  padding: 0 5px 0 0px
  height: $height
  display: grid
  grid-template-columns: $height minmax(0,3*$height) minmax(140px, 2fr)  minmax(170px,8fr) minmax(0,1fr) auto minmax(90px, 2fr)
  grid-template-rows: $mid-heigth $mid-heigth
  grid-template-areas: "image track play-control position . play-mode volume-control" "image album-artist play-control position . play-mode volume-control"
  font-size: 10px
  grid-gap: 0px 5px
  min-width: 420px + $height

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
    margin-right: 10px

  .position
    grid-area: position
    align-self: center

  .image
    grid-area: image

  .play-control
    grid-area: play-control
    align-self: center
    text-align: center

  .play-mode
    grid-area: play-mode
    align-self: center
    overflow: hidden
</style>
