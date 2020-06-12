<template>
  <div class="palette" v-if="palette">
    <div class="color-description" v-for="(element, key) in palette" :key="key">
      <div
        class="color"
        :style="{ 'background-color': element.getHex() }"
      ></div>
      <div class="name">{{ key }}</div>
    </div>
  </div>
</template>
<script>
export default {
  props: {
    palette: {
      required: true,
      type: Object
    }
  }
};
</script>
<style lang="sass" scoped>
.palette
  display: flex
  flex-flow: column nowrap

  .color-description
    display: flex
    flex-flow: row nowrap
    font-size: 12px

    .name
      align-self: center
      padding-left: 8px

  .color
    width: 30px
    height: 30px
</style>
