<template>
  <v-card class="album-display" v-if="album" :class="type">
    <cover
      :size="size"
      :album="album"
      class="main"
      @imageSource="onImageSource"
    />

    <span v-if="showInfo" class="album-title">{{ album.Name }}</span>

    <span v-if="showInfo" class="album-artist">{{ album.Artists | join }}</span>
  </v-card>
</template>
<script>
import cover from "../cover/cover";
export default {
  name: "albumDisplayer",
  components: {
    cover
  },
  props: {
    album: {
      type: Object
    },
    size: {
      type: String,
      default: "150px"
    },
    type: {
      type: String,
      default: "vertical"
    }
  },
  computed: {
    showInfo() {
      const { type, hasImage } = this;
      return type !== "compact" || !hasImage;
    }
  },
  data() {
    return {
      hasImage: false
    };
  },
  methods: {
    onImageSource(value) {
      this.hasImage = value !== null;
      this.$emit("imageSource", value);
    }
  }
};
</script>
<style lang="sass" scoped>
.album-display
  display: grid
  grid-template-columns: auto
  grid-template-rows: 1fr auto auto
  grid-template-areas: "image" "artist" "title"
  grid-row-gap: 2px
  grid-column-gap: 8px
  width: fit-content
  height: fit-content
  margin: 2px

  &.compact
    grid-template-columns: auto
    grid-template-rows: 50% 50%
    grid-template-areas: "artist" "title"
    grid-gap: 0
    box-shadow: none

    ::v-deep.main
      grid-column: 1
      grid-row: 1 / 3

    span.album-artist
      align-self: start

    span.album-title
      align-self: end

  &.vertical
    padding-bottom: 6px

  &.horizontal
    grid-template-columns: 1fr auto
    grid-template-rows: 1fr 1fr
    grid-template-areas: "image artist" "image title"
    padding-right: 6px

    span.album-artist
      align-self: start

    span.album-title
      align-self: end

  ::v-deep.main
    grid-area: image

  span
    text-align: center

    &.album-title
      font-weight: bold
      font-size: 16px
      grid-area: artist

    &.album-artist
      opacity: $subtopic-opacity
      font-size: 14px
      grid-area: title
</style>
