<template>
  <v-btn-toggle>
    <icon-button icon="mdi-skip-backward" :command="player.Back" />

    <icon-button icon="mdi-play" :command="player.Play" />

    <icon-button icon="mdi-pause" :command="player.Pause" />

    <icon-button icon="mdi-stop" :command="player.Stop" />

    <icon-button icon="mdi-skip-forward" :command="player.Next" />
  </v-btn-toggle>
</template>

<script>
export default {
  name: "complete-play",
  props: {
    player: {
      required: true,
      type: Object
    }
  }
};
</script>
