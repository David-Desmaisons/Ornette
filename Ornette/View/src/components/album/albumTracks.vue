<template>
  <v-card class="album-tracks" :style="style">
    <albumDisplayer
      :size="size"
      :album="album"
      type="compact"
      class="main"
      @imageSource="onImageSource"
    />
    <div class="tracks" :style="size | style">
      <div class="track-item" v-for="track in tracks" :key="track.Path">
        <div class="name">{{ track.MetaData.Name }}</div>
        <div class="duration">{{ track.MetaData.Duration | timeSpan }}</div>
      </div>

      <palette :palette="palette" />
    </div>
  </v-card>
</template>
<script>
import albumDisplayer from "./albumDisplayer";
import palette from "../helper/palette";
import * as Vibrant from "node-vibrant";

export default {
  props: {
    album: {
      type: Object,
      required: true
    },
    tracks: {
      type: Array,
      required: true
    },
    size: {
      type: String,
      default: "150px"
    }
  },
  components: {
    albumDisplayer,
    palette
  },
  data() {
    return {
      palette: null
    };
  },
  computed: {
    style() {
      const { palette } = this;
      if (palette === null) {
        return null;
      }
      const { LightVibrant, LightMuted, DarkVibrant } = palette;
      const textColor = LightMuted || LightVibrant;
      return {
        "background-color": DarkVibrant.getHex(),
        color: textColor.getHex()
      };
    }
  },
  methods: {
    async onImageSource(uri) {
      if (!uri) {
        this.palette = null;
        return;
      }
      const palette = await Vibrant.from(uri).getPalette();
      this.palette = palette;
    }
  }
};
</script>
<style lang="sass" scoped>
.album-tracks
  display: grid
  grid-template-columns: min-content min-content
  gap: 8px
  width: fit-content
  height: fit-content
  border-radius: 2%
  transition: background-color 50ms linear, color 50ms linear
  margin: 2px

  .main
    margin: 0px

  .tracks
    padding-left: 8px
    padding-right: 8px
    padding-top: 4px
    display: flex
    flex-flow: row wrap
    overflow: auto

    .track-item
      width: 100%
      display: flex
      justify-content: space-between
      flex-direction: row
      font-size: 12px

      div
        &.name
          flex-grow: 1
          align-self: center

        &.duration
          margin-left: 10px
          align-self: center
</style>
