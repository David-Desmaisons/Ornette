<template>
  <div class="track-item">
    <cover :album="track.MetaData.Album" :size="size" class="main-image" />
    <div class="artist">{{ track.MetaData.Album.Artists | join }}</div>
    <div class="name">{{ track.MetaData.Name }}</div>
    <div class="duration">{{ track.MetaData.Duration | timeSpan }}</div>
  </div>
</template>
<script>
import cover from "../cover/cover";
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
  components: {
    cover
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

  .main-image
    grid-area: image
    align-self: center
    justify-self: start

  div
    &.artist
      grid-area: artist
      font-size: 10px
      opacity: $subtopic-opacity
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
      opacity: $subtopic-opacity
</style>
