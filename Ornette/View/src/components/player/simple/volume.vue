<template>
  <div
    class="volume-slider"
    :class="{ muted, vertical }"
    @wheel.prevent="wheel"
  >
    <v-btn icon small @click="mute">
      <v-icon small :color="color">
        {{ icon }}
      </v-icon>
    </v-btn>

    <VueSlider
      class="slider"
      :value="volumeValue"
      :max="100"
      :duration="0.1"
      :dotSize="10"
      :tooltipFormatter="format"
      :height="height"
      :width="width"
      :direction="direction"
      :tooltip="tooltip"
      :useKeyboard="true"
      @change="change"
    />
  </div>
</template>
<script>
import mixin from "@/components/player/mixins/volume";
import VueSlider from "vue-slider-component";

export default {
  mixins: [mixin],
  components: {
    VueSlider
  },
  props: {
    vertical: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      tooltip: "active"
    };
  },
  methods: {
    format(value) {
      return `${value}%`;
    }
  },
  computed: {
    icon() {
      return this.value === 0
        ? "volume_off"
        : this.value >= 50
        ? "volume_up"
        : "volume_down";
    },
    color() {
      return this.muted ? "grey" : undefined;
    },
    direction() {
      return this.vertical ? "btt" : undefined;
    },
    height() {
      return this.vertical ? "100%" : 4;
    },
    width() {
      return this.vertical ? 4 : "100%";
    }
  },
  watch: {
    value(newValue) {
      if (newValue === 0 && this.muted) {
        return;
      }
      this.muted = false;
      this.tooltip = "always";
      const { _timeOut } = this;
      if (_timeOut) {
        clearTimeout(_timeOut);
      }
      this._timeOut = setTimeout(() => {
        this.tooltip = "active";
      }, 400);
    }
  }
};
</script>
<style lang="sass" scoped>
.volume-slider
  display: flex
  flex-direction: row
  align-items: center
  border-radius: 4px
  border-style: solid
  border-width: thin
  box-shadow: none
  opacity: 0.8
  padding-right: 8px
  border-color: rgba(255, 255, 255, 0.12)

  &.muted
    ::v-deep
      .slider .vue-slider-process
        background-color: map-get($grey, 'base')
  &.vertical
    flex-direction: column-reverse
    padding-right: 0px
    padding-top: 8px
</style>
