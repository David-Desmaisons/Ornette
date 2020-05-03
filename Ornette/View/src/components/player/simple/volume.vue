<template>
  <v-slider
    class="volume-slider"
    :value="volumeValue"
    @change="change"
    :max="100"
    :height="30"
    :prepend-icon="icon"
    @click:prepend="mute"
  >
  </v-slider>
</template>
<script>
export default {
  props: {
    value: {
      type: Number,
      required: true
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
      this.muted = false;
      this.$emit("input", evt);
    }
  }
};
</script>
<style>
.volume-slider .v-icon.v-icon {
  font-size: 16px;
}
</style>
