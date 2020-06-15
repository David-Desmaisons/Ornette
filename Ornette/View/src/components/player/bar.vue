<template>
  <v-card class="bar">
    <cover :album="album" size="50px" />

    <span v-if="album" class="album-artist"
      >{{ album.Artists | join }} - {{ album.Name }}</span
    >

    <span v-if="player.CurrentTrack" class="track">{{ metaData.Name }}</span>

    <position dot class="position" :player="player" />

    <volume class="volume-control" v-model="player.Volume" :asButton="false" />
  </v-card>
</template>
<script>
import cover from "../cover/cover";
import volume from "../player/basic/volume";

import { album } from "@/filter/track";

export default {
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
    volume
  }
};
</script>
<style lang="sass" scoped>
$height: 50px
$mid-heigth: $height/2
.bar
  padding: 0 5px 0 0px
  height: $height
  display: grid
  grid-template-columns: $height minmax($height, auto) repeat(3, 2fr) minmax(100px, 1fr)
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

  .image
    grid-area: image

  ::v-deep
    span.album-title
      font-size: 10px
</style>
