<template>
  <div
    class="volume-slider"
    :class="{ muted, vertical, retracted, asButton }"
    @wheel.prevent="wheel"
    @mouseover.prevent="onHover(true)"
    @mouseleave.prevent="onHover(false)"
  >
    <v-btn class="volume" icon small @click="mute">
      <v-icon small :color="color">
        {{ icon }}
      </v-icon>
    </v-btn>

    <VueSlider
      v-if="!retracted"
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
    },
    asButton: {
      type: Boolean,
      default: true
    },
    expandable: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      tooltip: "active",
      quarter: 0,
      show: null
    };
  },
  created() {
    const show = !this.expandable;
    this.show = show;
    this.quarter = show ? 4 : 0;
  },
  methods: {
    format(value) {
      return `${value}%`;
    },
    onHover(value) {
      if (!this.expandable) {
        return;
      }
      this.show = value;
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
    size() {
      return `${(100 * this.quarter) / 4}%`;
    },
    height() {
      return this.vertical ? this.size : 4;
    },
    width() {
      return this.vertical ? 4 : this.size;
    },
    retracted() {
      return this.quarter === 0;
    }
  },
  watch: {
    retracted: {
      immediate: true,
      handler(value) {
        this.$emit("retractChange", value);
      }
    },
    show(value) {
      const { _interval } = this;
      if (_interval) {
        clearInterval(_interval);
      }
      this._interval = setInterval(() => {
        const newValue = this.quarter + (value ? 1 : -1);
        const quarter = Math.min(4, Math.max(newValue, 0));
        this.quarter = quarter;
        if (quarter === 0 || quarter === 4) {
          clearInterval(this._interval);
          this._interval = null;
        }
      }, 20);
    },
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
  box-shadow: none
  opacity: 0.92
  padding-top: 0px
  padding-right: 5px
  transition: all 0.5s ease-in
  &.asButton
    border-radius: 4px
    border-style: solid
    border-width: thin
    border-color: rgba(255, 255, 255, 0.12)
    &:hover
      background-color: rgba(255, 255, 255, 0.18)


  ::v-deep
    button.v-btn.volume
      &:not(.v-btn--text):not(.v-btn--outlined):before
        opacity: 0

  &:not(.vertical)
    height: 30px
    ::v-deep
      button.v-btn.volume
        border-radius: 4px 0px 0px 4px

  .slider

    margin-left: 2px
    margin-bottom: 0px

  &.retracted
    max-height: 28px

  &.muted
    ::v-deep
      .slider .vue-slider-process
          background-color: map-get($grey, 'base')

  &.vertical
    flex-direction: column-reverse
    padding-right: 0px
    padding-top: 5px
    width: 30px

    ::v-deep
      button.v-btn.volume
        border-radius: 0px 0px 4px 4px

    .slider
      margin-bottom: 1px
      margin-left: 0px
</style>
