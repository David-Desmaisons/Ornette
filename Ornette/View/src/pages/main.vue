<template>
  <main>
    <v-content>
      <v-container
        fluid
        class="main-container"
      >
        <v-layout>
          <simplePlayer :player="viewModel.Player" />
          <albumDisplayer
            :album="currentAlbum"
            horizontal
          />
          <tracksList
            :tracks="tracks"
            :currentTrack="currentTrack"
            @click="click"
          />
        </v-layout>
      </v-container>
    </v-content>
  </main>
</template>

<script>
import simplePlayer from "@/components/player/simple/player";
import albumDisplayer from "@/components/album/albumDisplayer";
import tracksList from "@/components/track/tracksList";

const props = {
  viewModel: Object
};

export default {
  props,
  components: {
    simplePlayer,
    albumDisplayer,
    tracksList
  },
  methods: {
    click(track) {
      const {
        viewModel: { Player }
      } = this;
      Player.CurrentTrack = track;
    }
  },
  computed: {
    currentAlbum() {
      const {
        viewModel: { Player }
      } = this;
      return Player.CurrentTrack === null
        ? null
        : Player.CurrentTrack.MetaData.Album;
    },
    currentTrack() {
      const {
        viewModel: { Player }
      } = this;
      return Player.CurrentTrack;
    },
    tracks() {
      const {
        viewModel: { Player }
      } = this;
      return Player.Tracks;
    }
  }
};
</script>

<style>
main {
  height: 100%;
}

.main-container {
  height: calc(100% - 50px);
  overflow-y: auto;
}
</style>
