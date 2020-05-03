<template>
  <v-btn icon :disabled="!canExecute" @click.stop="execute" v-bind="attrs">
    <v-icon v-bind="attrs">{{ icon }}</v-icon>
  </v-btn>
</template>

<script>
import mixin from "neutronium-vue-simple-command-mixin";

export default {
  mixins: [mixin],
  props: {
    icon: {
      type: String,
      required: true
    },
    small: {
      type: Boolean,
      default: true
    }
  },
  computed: {
    attrs() {
      const { small, $attrs } = this;
      return { small, ...$attrs };
    }
  },
  inheritAttrs: false
};
</script>

<style></style>
