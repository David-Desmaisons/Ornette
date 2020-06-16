<template>
  <v-btn
    :color="color"
    :disabled="!canExecute"
    @click.stop="execute"
    v-bind="$attrs"
  >
    {{ text }}
  </v-btn>
</template>

<script>
import mixin from "neutronium-vue-simple-command-mixin";

export default {
  mixins: [mixin],
  props: {
    text: {
      type: String,
      required: true
    },
    color: {
      type: String,
      default: "primary"
    }
  },
  inheritAttrs: false
};
</script>

<style></style>
