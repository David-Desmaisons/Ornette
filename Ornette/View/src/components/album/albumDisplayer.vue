<template>
  <div>
    <img v-if="currentImageUri" :src="currentImageUri" />
    <slot v-else name="no-art" />
  </div>
</template>
<script>
export default {
  name: "albumDisplayer",
  props: {
    album: {
      required: true,
      type: Object
    }
  },
  data() {
    return {
      imageIndex: 0
    };
  },
  computed: {
    currentImageUri() {
      const {
        album: { Images },
        imageIndex
      } = this;
      return Images.length > 0 ? Images[imageIndex].Uri : null;
    }
  }
};
</script>
<style lang="sass" scoped></style>
