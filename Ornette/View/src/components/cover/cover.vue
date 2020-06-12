<template>
  <div class="cover" :style="size | style">
    <img
      class="main main-image"
      v-if="imageUri"
      :src="imageUri"
      :style="size | imageStyle"
    />
    <slot v-else name="no-art" :album="album" :size="size" :class="'main'">
      <noCover class="main no-art" :style="size | style" />
    </slot>
  </div>
</template>
<script>
import noCover from "./noCover";
import { albumImage } from "@/filter/track";
export default {
  name: "cover",
  props: {
    album: {
      type: Object,
      required: true
    },
    size: {
      type: String,
      required: true
    }
  },
  data() {
    return {
      imageIndex: 0
    };
  },
  components: {
    noCover
  },
  watch: {
    imageUri: {
      handler(value) {
        this.$emit("imageSource", value);
      },
      immediate: true
    }
  },
  computed: {
    imageUri() {
      const { album, imageIndex } = this;
      return albumImage(album, imageIndex);
    }
  }
};
</script>
<style lang="sass" scoped>
.cover
  width: fit-content
  height: fit-content

  .main-image
    text-align: center
    display: block
    margin-left: auto
    margin-right: auto
    border-radius: 2%
    width: auto
    height: auto
</style>
