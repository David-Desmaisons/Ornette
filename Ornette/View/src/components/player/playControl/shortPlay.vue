<template>
  <div class="play">
    <icon-button icon="mdi-skip-backward" :command="player.Back" fab small />

    <icon-button
      :icon="isPlaying ? 'mdi-pause' : 'mdi-play'"
      :command="isPlaying ? player.Pause : player.Play"
      fab
      :small="false"
    />

    <icon-button icon="mdi-skip-forward" :command="player.Next" fab small />
  </div>
</template>

<script>
export default {
  name: "short-play",
  props: {
    player: {
      required: true,
      type: Object
    }
  },
  computed: {
    isPlaying() {
      return this.player.State.intValue === 4;
    }
  }
};
</script>
