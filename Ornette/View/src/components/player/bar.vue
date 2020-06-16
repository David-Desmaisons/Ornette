<template>
  <v-card class="bar">
    <cover :album="album" size="54px" />

    <span v-if="album" class="album-artist"
      >{{ album.Artists | join }} - {{ album.Name }}</span
    >

    <span v-if="player.CurrentTrack" class="track">{{ metaData.Name }}</span>

    <position class="position" :player="player" />

    <volume class="volume-control" v-model="player.Volume" :asButton="false" />
  </v-card>
</template>
<script>
import cover from "../cover/cover";
import volume from "./basic/volume";
import position from "./position/completePosition";
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
    position
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
  grid-template-columns: $height minmax($height, 1fr) 1fr 3fr 1fr minmax(100px, 1fr)
  grid-template-rows: $mid-heigth $mid-heigth
  grid-template-areas: "image album-artist play-control position repeat-control volume-control" "image track play-control position repeat-control volume-control"
  font-size: 10px
  grid-gap: 0px 5px

  .album-artist
    grid-area: album-artist
    font-weight: bold
    align-self: end

  .track
    grid-area: track
    align-self: start

  .volume-control
    grid-area: volume-control
    align-self: center

  .position
    grid-area: position
    align-self: center

  .image
    grid-area: image

  ::v-deep
    span.album-title
      font-size: 10px
</style>
