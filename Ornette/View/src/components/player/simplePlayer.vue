<template>
  <div class="simple-player">
    <p class="time">{{ player.PositionInSeconds | formatTime }}</p>

    <marquee class="track-name">{{ player.CurrentTrack | track }}</marquee>

    <v-slider
      v-if="player.CurrentTrack"
      v-model="player.PositionInSeconds"
      :max="player.CurrentTrack | totalSeconds"
      class="slider player"
      :dense="true"
    />

    <v-btn-toggle class="control">
      <icon-button icon="mdi-skip-backward" :command="player.Back" />

      <icon-button icon="mdi-play" :command="player.Play" />

      <icon-button icon="mdi-pause" :command="player.Pause" />

      <icon-button icon="mdi-stop" :command="player.Stop" />

      <icon-button icon="mdi-skip-forward" :command="player.Next" />
    </v-btn-toggle>

    <v-btn-toggle
      class="control-minor"
      multiple
      :value="toogleValue"
      @change="changeToogle"
    >
      <v-btn small icon>
        <v-icon>mdi-shuffle-variant</v-icon>
      </v-btn>

      <v-btn small icon>
        <v-icon>mdi-repeat</v-icon>
      </v-btn>
    </v-btn-toggle>
  </div>
</template>

<script>
import iconButton from "../iconButton";

export default {
  props: {
    player: {
      required: true,
      type: Object
    }
  },
  components: {
    iconButton
  },
  computed: {
    toogleValue() {
      const res = [];
      if (this.player.AutoReplay) res.push(1);
      if (this.player.RandomPlay) res.push(0);
      return res;
    }
  },
  methods: {
    changeToogle(evt) {
      this.player.AutoReplay = evt.includes(1);
      this.player.RandomPlay = evt.includes(0);
    }
  }
};
</script>

<style scoped="true">
.simple-player {
  width: 200px;
  height: 50px;
  display: grid;
  grid-template-columns: 2fr 3fr 2fr;
  grid-template-rows: 25px 20px 1fr;
  grid-column-gap: 5px;
}

.simple-player div {
  align-self: center;
  place-self: center;
}

.simple-player .time {
  grid-column: 1 / span 1;
  grid-row-start: 1;
  margin-bottom: 0;
  font-size: 20px;
  text-align: center;
  align-self: center;
  place-self: center;
}

.simple-player {
  font-size: 10px;
}

.simple-player .slider {
  height: 30px;
  width: 100%;
  grid-column: 1 / span 3;
  grid-row-start: 2;
}

.simple-player .track-name {
  width: 100%;
  grid-column: 2 / span 2;
  grid-row-start: 1;
  align-self: center;
  place-self: center;
}

.simple-player .control {
  grid-column: 2 / span 2;
  grid-row-start: 3;
  align-self: center;
  place-self: flex-end;
}

.simple-player .control-minor {
  grid-row-start: 3;
  grid-column: 1 / span 1;
}
</style>
