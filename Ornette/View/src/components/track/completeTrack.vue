<template>
  <div class="track-item">
    <img
      class="main-image"
      :style="style"
      v-if="currentImageUri"
      :src="currentImageUri"
    />
    <div class="artist">{{ track.MetaData.Album.Artists | join }}</div>
    <div class="name">{{ track.MetaData.Name }}</div>
    <div class="duration">{{ track.MetaData.Duration | timeSpan }}</div>
  </div>
</template>
<script>
export default {
  name: "track-complete",
  props: {
    track: {
      required: true,
      type: Object
    },
    size: {
      required: false,
      default: "50px"
    }
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
        track: {
          MetaData: {
            Album: { Images }
          }
        }
      } = this;
      return Images.length > 0 ? Images[0].Uri : null;
    }
  }
};
</script>
<style lang="sass" scoped>
.track-item
  display: grid
  grid-template-columns: min-content 1fr min-content
  grid-template-rows: 1fr 1fr
  grid-template-areas: "image name duration" "image artist duration"
  justify-items: stretch
  justify-content: stretch
  column-gap: 6px
  font-size: 12px
  padding: 6px 0 6px 0

  img.main-image
      text-align: center
      display: block
      border-radius: 2%
      width: auto
      height: auto
      grid-area: image
      justify-self: start

  div
    &.artist
      grid-area: artist
      font-size: 10px
      font-style: italic
      justify-self: start
      align-self: start

    &.name
      grid-area: name
      justify-self: start
      align-self: end

    &.duration
      grid-area: duration
      align-self: center
      justify-self: end
</style>
