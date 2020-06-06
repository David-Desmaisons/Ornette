<template>
  <v-card class="track-list">
    <v-list dense>
      <v-list-item-group v-model="index" color="primary">
        <v-list-item v-for="track in tracks" :key="track.Path">
          <slot name="track" :track="track">
            <trackDisplay :track="track" />
          </slot>
        </v-list-item>
      </v-list-item-group>
    </v-list>
  </v-card>
</template>
<script>
import trackDisplay from "./simpleTrack";
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
  components: {
    trackDisplay
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
  margin: 2px
  height: fit-content

  ::v-deep.track-item
    flex-grow: 1
</style>
