export default {
  props: {
    value: {
      type: Number,
      required: true
    },
    wheelIncrement: {
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
      this.updateValue(evt);
    },
    wheel(evt) {
      const change =
        evt.deltaY < 0 ? this.wheelIncrement : -this.wheelIncrement;
      this.updateValue(this.volumeValue + change);
    },
    updateValue(value) {
      const normalizedValue = Math.max(0, Math.min(100, value));
      this.muted = false;
      this.$emit("input", normalizedValue);
    }
  }
};
