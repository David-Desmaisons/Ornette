import mixin from "@/components/player/mixins/volume";

const simpleMixin = {
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
    }
  }
};

const mixins = [mixin, simpleMixin]

export {
  mixins
}