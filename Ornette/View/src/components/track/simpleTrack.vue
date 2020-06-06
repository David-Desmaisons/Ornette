<template>
  <div class="track-item" >
    <div class="position">{{ track.MetaData.TrackNumber.Position }}</div>
    <div class="name">{{ track.MetaData.Name }}</div>
    <div class="duration">{{ track.MetaData.Duration | timeSpan }}</div>
  </div>
</template>
<script>
export default {
  name: "track-simple",
  props: {
    track: {
      required: true,
      type: Object
    }
  }
};
</script>
<style lang="sass" scoped>
.track-item
  display: flex
  justify-content: space-between
  flex-direction: row
  font-size: 12px

  div
    &.position
      width: 20px

    &.name
      flex-grow: 1

    &.duration
      margin-left: 10px
</style>
