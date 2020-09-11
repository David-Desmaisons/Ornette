<template>
  <v-app-bar id="top-menu" :clipped-left="true" absolute app>
    <v-app-bar-nav-icon @click.stop="toggleMenu" />

    <v-toolbar-title v-text="title" />

    <v-spacer />

    <icon-button :x-small="true" :command="window.Minimize" icon="remove" />

    <icon-button :command="window.Normalize" :icon="middleIcon" />

    <icon-button :command="window.Close" icon="close" />
  </v-app-bar>
</template>

<script>
const props = {
  value: {
    type: Boolean,
    required: true
  },
  title: {
    type: String,
    required: false
  },
  window: {
    type: Object,
    required: true
  }
};

export default {
  props,

  computed: {
    middleIcon() {
      return this.window.State.displayName == "Normal"
        ? "mdi-window-maximize"
        : "mdi-window-restore";
    }
  },

  methods: {
    toggleMenu() {
      this.$emit("input", !this.value);
    }
  }
};
</script>

<style>
#top-menu > div {
  -webkit-app-region: drag;
}

#top-menu > div > button {
  -webkit-app-region: no-drag;
}
</style>
