<template>
  <div v-if="album" class="album-display" :class="{ horizontal }">
    <img
      class="main main-image"
      v-if="currentImageUri"
      :src="currentImageUri"
      :style="style"
    />
    <slot class="main" v-else name="no-art" />

    <span class="album-title">{{ album.Name }}</span>

    <span class="album-artist">{{ album.Artists | join }}</span>
  </div>
</template>
<script>
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
    style() {
      const { size } = this;
      return {
        "max-width": size,
        "max-height": size
      };
    },
    currentImageUri() {
      const {
        album: { Images },
        imageIndex
      } = this;
      return Images.length > 0 ? Images[imageIndex].Uri : null;
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

  &.horizontal
    grid-template-columns: 1fr auto
    grid-template-rows: 1fr 1fr
    grid-template-areas: "image artist" "image title"
    align-self: center

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
