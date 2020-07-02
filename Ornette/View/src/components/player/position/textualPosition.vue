<template>
  <div class="complete-position" v-if="player.CurrentTrack">
    <span class="time">{{
      player.PositionInSeconds | formatTime("--:--")
    }}</span>
    <span class="separator">/</span>
    <span class="time">{{
      player.CurrentTrack.MetaData.Duration | timeSpan
    }}</span>
  </div>
</template>
<script>
export default {
  name: "complete-position",
  props: {
    player: {
      required: true,
      type: Object
    }
  }
};
</script>
<style lang="sass" scoped>
.complete-position
  opacity: $subtopic-opacity
  display: flex

  *
    margin-right: 4px
    align-self: center

  .separator
    font-size: larger
</style>
