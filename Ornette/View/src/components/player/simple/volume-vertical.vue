<template>
  <v-slider
    class="volume-slider-vertical"
    :class="{ muted }"
    :value="volumeValue"
    :color="color"
    :max="100"
    :hide-details="true"
    :prepend-icon="icon"
    :vertical="true"
    style=" width: 32px;"
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

.volume-slider-vertical
  width: 32px
  ::v-deep
    .v-icon.v-icon
      font-size: 16px
    .v-input__slot
      margin-bottom: 0
    .v-input__prepend-outer
      width: 32px
  &.muted
    ::v-deep .v-icon.v-icon
      color: map-get($grey, 'base')
</style>
