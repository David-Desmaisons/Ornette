<template>
  <v-slider
    class="volume-slider"
    :class="{ muted }"
    :value="volumeValue"
    :color="color"
    :max="100"
    :hide-details="true"
    :prepend-icon="icon"
    @click:prepend="mute"
    @change="change"
    @wheel.prevent.native="wheel"
  >
  </v-slider>
</template>
<script>
import { mixins } from "./mixins/volume";

export default {
  mixins
};
</script>
<style lang="sass" scoped>
@import '~vuetify/src/styles/styles.sass'

.volume-slider
  ::v-deep
    .v-icon.v-icon
      font-size: 16px
    .v-input__slot
      margin-bottom: 0
  &.muted
    ::v-deep .v-icon.v-icon
      color: map-get($grey, 'base')
</style>
