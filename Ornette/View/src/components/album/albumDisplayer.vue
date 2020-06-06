<template>
  <v-card class="album-display" v-if="album" :class="{ horizontal }">
    <img
      class="main main-image"
      v-if="imageUri"
      :src="imageUri"
      :style="size | imageStyle"
    />
    <slot v-else class="main" name="no-art" :album="album" :size="size"> </slot>

    <span class="album-title">{{ album.Name }}</span>

    <span class="album-artist">{{ album.Artists | join }}</span>
  </v-card>
</template>
<script>
import { albumImage } from "@/filter/track";
export default {
  name: "albumDisplayer",
  props: {
    album: {
      type: Object
    },
    size: {
      type: String,
      default: "150px"
    },
    horizontal: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      imageIndex: 0
    };
  },
  computed: {
    imageUri() {
      const { album, imageIndex } = this;
      return albumImage(album, imageIndex);
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
  padding: 4px

  &.horizontal
    grid-template-columns: 1fr auto
    grid-template-rows: 1fr 1fr
    grid-template-areas: "image artist" "image title"

    span.album-artist
      align-self: start

    span.album-title
      align-self: end

  .main
    grid-area: image

    &.main-image
      text-align: center
      display: block
      margin-left: auto
      margin-right: auto
      border-radius: 2%
      width: auto
      height: auto

  span
    text-align: center

    &.album-title
      font-weight: bold
      font-size: 16px
      grid-area: artist

    &.album-artist
      font-style: italic
      font-size: 14px
      grid-area: title
</style>
