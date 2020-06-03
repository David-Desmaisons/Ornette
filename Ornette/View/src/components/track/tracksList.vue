<template>
  <ul class="track-list">
    <li
      v-for="track in tracks"
      :key="track.Path"
      :class="{selected: track===currentTrack}"
      @click="click(track)"
    >
      <div class="position">{{ track.MetaData.TrackNumber.Position }}</div>
      <div class="name"> {{ track.MetaData.Name }}</div>
      <div class="duration">{{track.MetaData.Duration  | timeSpan }}</div>

    </li>
  </ul>
</template>
<script>
export default {
  name: "tracks-list",
  props: {
    tracks: {
      required: true,
      type: Array
    },
    currentTrack: {
      required: false,
      type: Object
    }
  },
  methods: {
    click(track) {
      this.$emit("click", track);
    }
  }
};
</script>
<style lang="sass" scoped>
.track-list
  font-size: 12px

  li
    display: flex
    flex-direction: row
    &.selected
      background-color: red

    div
      &.position
        width: 20px

      &.name
        flex-grow: 1
      
      &.duration
        width: 30px
        margin-left: 10px
</style>
