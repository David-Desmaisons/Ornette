<template>
  <VueSlider
    class="slider player"
    :disabled="disabled"
    v-model="player.PositionInSeconds"
    :key="key"
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
  name: "position",
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
    },
    key() {
      const {
        player: { CurrentTrack }
      } = this;
      if (CurrentTrack === null) {
        return "";
      }
      const {
        MetaData: {
          Name,
          TrackNumber: { Position }
        }
      } = CurrentTrack;
      return `${Name}-${Position}`;
    }
  }
};
</script>
