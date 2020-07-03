<template>
  <transition name="component-fade" mode="out-in">
    <slot />
  </transition>
</template>
<script>
export default {
  name: "fade-transition"
};
</script>
<style lang="sass" scoped>
.component-fade-enter-active, .component-fade-leave-active
  transition: opacity .2s ease

.component-fade-enter, .component-fade-leave-to
  opacity: 0
</style>
