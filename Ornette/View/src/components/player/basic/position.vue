<template>
  <VueSlider
    class="slider player"
    :disabled="disabled"
    v-model="player.PositionInSeconds"
    :max="player.CurrentTrack | totalSeconds(100)"
    :duration="0.1"
    :dotSize="dotSize"
    :height="4"
    :tooltipFormatter="formatTime"
    tooltip="focus"
    width="calc(100% - 2px)"
    lazy
  />
</template>

<script>
import VueSlider from "vue-slider-component";
import { formatTime } from "@/filter/time";

export default {
  props: {
    player: {
      required: true,
      type: Object
    },
    dot: {
      type: Boolean,
      default: false
    }
  },
  components: {
    VueSlider
  },
  methods: {
    formatTime
  },
  computed: {
    disabled() {
      return this.player.CurrentTrack === null;
    },
    dotSize() {
      return this.dot && !this.disabled ? 10 : 0;
    }
  }
};
</script>

<style lang="sass">
$themeColor: orange
@import '~vue-slider-component/lib/theme/default.scss'
</style>
