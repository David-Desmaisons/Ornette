<template>
  <div class="complete-position">
    <div class="time">{{ player.PositionInSeconds | formatTime("--:--") }}</div>

    <position class="position" :player="player" />

    <div class="time">{{ remaining | formatTime("--:--") }}</div>
  </div>
</template>
<script>
import position from "./basicPosition";
import { totalSeconds } from "@/filter/time";

export default {
  name: "complete-position",
  props: {
    player: {
      required: true,
      type: Object
    }
  },
  components: {
    position
  },
  computed: {
    totalInSeconds() {
      const {
        player: { CurrentTrack }
      } = this;
      return totalSeconds(CurrentTrack, null);
    },
    remaining() {
      const {
        player: { PositionInSeconds },
        totalInSeconds
      } = this;
      if (totalInSeconds === null || PositionInSeconds === null) {
        return null;
      }
      return totalInSeconds - PositionInSeconds;
    }
  }
};
</script>
<style lang="sass" scoped>
.complete-position
  display: grid
  grid-template-columns: 30px 1fr 30px
  gap: 10px

  *
    align-self: center
    text-align: center
</style>
