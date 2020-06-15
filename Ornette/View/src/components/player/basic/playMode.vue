<template>
  <v-btn-toggle
    class="play-mode"
    multiple
    :value="toogleValue"
    @change="changeToogle"
  >
    <v-btn small icon>
      <v-icon x-small>mdi-shuffle-variant</v-icon>
    </v-btn>

    <v-btn small icon>
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
