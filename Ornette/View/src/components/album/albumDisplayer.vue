<template>
  <div v-if="album" class="album-display">
    <img
      class="main main-image"
      v-if="currentImageUri"
      :src="currentImageUri"
    />
    <slot class="main" v-else name="no-art" />

    <span class="album-artist">{{ album.Artists | join }}</span>

    <span class="album-title">{{ album.Name }}</span>
  </div>
</template>
<script>
export default {
  name: "albumDisplayer",
  props: {
    album: {
      type: Object
    }
  },
  data() {
    return {
      imageIndex: 0
    };
  },
  computed: {
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
  grid-template-rows: 1fr auto auto
  grid-template-columns: auto
  width: fit-content
  height: fit-content
  margin: 2px

  .main
    width: 150px

    &.main-image
      text-align: center
      display: block
      margin-left: auto
      margin-right: auto
      border-radius: 2%

  span
    text-align: center

    &.album-artist
      font-weight: bold
      font-size: 16px
      align-self: center

    &.album-title
      font-style: italic
      font-size: 12px
      align-self: center
</style>
