<template>
  <main>
    <v-content>
      <v-container fluid class="main-container">
        <v-layout>
          <simplePlayer :player="viewModel.Player" />
        </v-layout>
      </v-container>
    </v-content>
  </main>
</template>

<script>
import simplePlayer from "../components/player/simple/player";

const props = {
  viewModel: Object
};

export default {
  props,
  components: {
    simplePlayer
  }
};
</script>

<style>
main {
  height: 100%;
}

.main-container {
  height: calc(100% - 50px);
  overflow-y: auto;
}
</style>
