<template>
  <div>

    <icon-button icon="mdi-play"  :command="player.Play"></icon-button>
    <icon-button icon="mdi-pause" :command="player.Pause"></icon-button>
    <icon-button icon="mdi-stop"  :command="player.Stop"></icon-button>
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
  }
};
</script>

<style>
</style>