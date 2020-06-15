<template>
  <main>
    <v-content>
      <v-container fluid class="main-container">
        <v-layout>
          <simplePlayer :player="viewModel.Player" />
          <tracksList
            :tracks="tracks"
            :currentTrack="currentTrack"
            @choosen="choosen"
          >
            <template slot="track" slot-scope="{ track }">
              <completeTrack :track="track" />
            </template>
          </tracksList>
          <albumTracks :album="currentAlbum" :tracks="tracks" />
          <albumDisplayer :album="currentAlbum" type="compact" />
          <albumDisplayer :album="currentAlbum" />
          <albumDisplayer :album="currentAlbum" type="horizontal" />
          <tracksList
            :tracks="tracks"
            :currentTrack="currentTrack"
            @choosen="choosen"
          />
        </v-layout>
        <bar :player="viewModel.Player" />
      </v-container>
    </v-content>
  </main>
</template>

<script>
import simplePlayer from "@/components/player/simple";
import bar from "@/components/player/bar";
import albumDisplayer from "@/components/album/albumDisplayer";
import albumTracks from "@/components/album/albumTracks";
import tracksList from "@/components/track/tracksList";
import completeTrack from "@/components/track/completeTrack";

export default {
  name: "demo",
  props: {
    viewModel: Object
  },
  components: {
    albumTracks,
    simplePlayer,
    albumDisplayer,
    tracksList,
    completeTrack,
    bar
  },
  methods: {
    choosen(track) {
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
