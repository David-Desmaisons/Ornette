<template>
  <main>
    <v-content>
      <v-container fluid class="main-container">
        <v-layout>
          <v-flex xs12 sm6 offset-sm3>
            <v-card>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
    </v-content>
  </main>
</template>

<script>
const props = {
  viewModel: Object
};

export default {
  props
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
