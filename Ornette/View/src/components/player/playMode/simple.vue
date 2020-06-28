<template>
  <v-btn-toggle
    multiple
    class="play-mode"
    :class="{ borderless }"
    :borderless="borderless"
    :value="toogleValue"
    @change="changeToogle"
  >
    <v-btn small icon :fab="borderless">
      <v-icon x-small>mdi-shuffle-variant</v-icon>
    </v-btn>

    <v-btn small icon :fab="borderless">
      <v-icon x-small>mdi-repeat</v-icon>
    </v-btn>
  </v-btn-toggle>
</template>

<script>
export default {
  props: {
    player: {
      required: true,
      type: Object
    },
    borderless: {
      required: false,
      default: false
    }
  },
  computed: {
    toogleValue() {
      const res = [];
      if (this.player.AutoReplay) res.push(1);
      if (this.player.RandomPlay) res.push(0);
      return res;
    }
  },
  methods: {
    changeToogle(evt) {
      this.player.AutoReplay = evt.includes(1);
      this.player.RandomPlay = evt.includes(0);
    }
  }
};
</script>
<style lang="sass" scoped>
.play-mode
  &.borderless
    ::v-deep
      button.v-btn.v-btn--active
        .v-icon
          background-color: transparent
          color: $main-color
          opacity: 1

    button.v-btn:before
      background-color: transparent
</style>
