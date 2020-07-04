<template>
  <v-footer class="footer" :fixed="fixed" app height="60">
    <fade-transition>
      <bar class="bar" v-if="showBar" :player="player" />
      <span v-else class="footer-text">&copy; {{ year }} - {{ by }}</span>
    </fade-transition>
  </v-footer>
</template>

<script>
import bar from "@/components/player/compactBar";
export default {
  name: "application-footer",
  props: {
    fixed: {
      type: Boolean,
      default: false
    },
    year: {
      type: Number,
      required: false
    },
    by: {
      type: String,
      required: true
    },
    showBar: {
      type: Boolean,
      required: false
    },
    player: {
      type: Object,
      required: false
    }
  },
  components: {
    bar
  }
};
</script>

<style lang="sass" scoped>
.footer
  padding: 0
  z-index: 10

  .footer-text
    margin: 10px

  .bar
    width: 100%
</style>
