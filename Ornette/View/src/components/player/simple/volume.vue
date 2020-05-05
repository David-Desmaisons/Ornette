<template>
  <v-slider
    class="volume-slider"
    :class="{ muted }"
    :value="volumeValue"
    :color="color"
    :max="100"
    :height="30"
    :prepend-icon="icon"
    @click:prepend="mute"
    @change="change"
    @wheel.prevent.native="wheel"
  >
  </v-slider>
</template>
<script>
export default {
  props: {
    value: {
      type: Number,
      required: true
    },
    wheelIncrement:{
      type: Number,
      default: 5
    }
  },
  data() {
    return {
      muted: false,
      lastVolume: this.value
    };
  },
  computed: {
    icon() {
      return this.value === 0
        ? "volume_off"
        : this.value >= 50
        ? "volume_up"
        : "volume_down";
    },
    volumeValue() {
      return this.muted ? this.lastVolume : this.value;
    },
    color() {
      return this.muted ? "grey" : undefined;
    }
  },
  methods: {
    mute() {
      this.muted = !this.muted;
      const newValue = this.muted ? 0 : this.lastVolume;
      this.lastVolume = this.value;
      this.$emit("input", newValue);
    },
    change(evt) {
      this.updateValue(evt);
    },
    wheel(evt) {
      const change = evt.deltaY < 0 ? this.wheelIncrement : -this.wheelIncrement;
      const value = this.muted ? this.lastVolume : this.value;
      this.updateValue(value + change);
    },
    updateValue(value) {
      const normaLizedValue = Math.max(0, Math.min(100, value));
      this.muted = false;
      this.$emit("input", normaLizedValue);
    }
  }
};
</script>
<style lang="sass" scoped>
@import '~vuetify/src/styles/styles.sass'

.volume-slider
  ::v-deep .v-icon.v-icon
    font-size: 16px
  &.muted
    ::v-deep .v-icon.v-icon
      color: map-get($grey, 'base')
</style>
