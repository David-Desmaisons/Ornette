<template>
  <v-list class="track-list" dense>
    <v-list-item-group v-model="index" color="primary">
      <v-list-item class="track-item" v-for="track in tracks" :key="track.Path">
        <div class="position">{{ track.MetaData.TrackNumber.Position }}</div>
        <div class="name">{{ track.MetaData.Name }}</div>
        <div class="duration">{{ track.MetaData.Duration | timeSpan }}</div>
      </v-list-item>
    </v-list-item-group>
  </v-list>
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
  computed: {
    index: {
      set(value) {
        const track = this.tracks[value];
        this.$emit("choosen", track);
      },
      get() {
        return this.tracks.indexOf(this.currentTrack);
      }
    }
  },
  methods: {
    click(track) {
      this.$emit("choosen", track);
    }
  }
};
</script>
<style lang="sass" scoped>
.track-list
  font-size: 12px

  .track-item
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
