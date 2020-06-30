<template>
  <v-btn
    small
    icon
    class="mode-button"
    :input-value="value"
    activeClass="active-button"
    @click="toggle"
  >
    <v-icon x-small>{{ icon }}</v-icon>
  </v-btn>
</template>
<script>
export default {
  name: "toggle-button",
  props: {
    icon: {
      type: String,
      required: true
    },
    value: {
      type: Boolean,
      required: true
    }
  },
  methods: {
    toggle() {
      this.$emit("input", !this.value);
    }
  }
};
</script>
<style lang="sass" scoped>
button.mode-button
  &.active-button.v-btn--active
    background-color: transparent
    color: $main-color
    opacity: 1

  &:not(:hover).v-btn--active.v-btn:before
    background-color: transparent
</style>
