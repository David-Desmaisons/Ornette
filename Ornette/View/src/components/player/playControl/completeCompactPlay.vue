<template>
  <div class="compact">
    <toggleButton
      class="mode"
      icon="mdi-shuffle-variant"
      v-model="player.RandomPlay"
    />

    <icon-button icon="mdi-skip-backward" :command="player.Back" fab small />

    <icon-button
      :icon="isPlaying ? 'mdi-pause' : 'mdi-play'"
      :command="isPlaying ? player.Pause : player.Play"
      fab
      :small="false"
    />

    <icon-button icon="mdi-skip-forward" :command="player.Next" fab small />

    <toggleButton class="mode" icon="mdi-repeat" v-model="player.AutoReplay" />
  </div>
</template>

<script>
import toggleButton from "@/components/button/toggleButton";
export default {
  name: "complete-compact-play",
  components: {
    toggleButton
  },
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
  },
  methods: {
    toggleRandom() {
      const { player } = this;
      this.player.RandomPlay = !player.RandomPlay;
    },
    toggleRepeat() {
      const { player } = this;
      player.AutoReplay = !player.AutoReplay;
    }
  }
};
</script>
<style lang="sass" scoped>
.compact
  width: fit-content
  height: fit-content

  .mode
    opacity: $subtopic-opacity
</style>
