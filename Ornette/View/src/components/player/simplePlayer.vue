<template>
  <div class="simple-player">

    <icon-button
      icon="mdi-play"
      :command="player.Play"
    />

    <icon-button
      icon="mdi-pause"
      :command="player.Pause"
    />

    <icon-button
      icon="mdi-stop"
      :command="player.Stop"
    />

    <p class="description">{{displayName}}</p>
    <p>{{player.Position |timeSpan }}</p>

  </div>
</template>

<script>
import Vue from "vue";
import iconButton from "../iconButton";

export default {
  props: {
    player: {
      required: true,
      type: Object
    }
  },
  computed: {
    displayName() {
      const { CurrentTrack } = this.player;
      if (CurrentTrack === null) {
        return "";
      }
      const {
        MetaData: { Duration, Name, TrackNumber }
      } = CurrentTrack;
      const filter = Vue.filter("timeSpan");
      return `${TrackNumber}-${Name}-${filter(Duration)}`;
    }
  },
  components: {
    iconButton
  }
};
</script>

<style scoped=true>
.simple-player {
  width: 100px;
  height: 50px;
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  grid-template-rows: 1fr 1fr;
}

.simple-player div {
  align-self: center;
  place-self: center;
}

.simple-player .description {
  grid-column: 1 / span 3;
  align-self: center;
  place-self: center;
}

.simple-player p {
  font-size: 10px;
}
</style>