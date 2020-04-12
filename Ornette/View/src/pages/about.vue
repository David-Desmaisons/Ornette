<template>
  <main>
    <v-content>
      <v-flex xs12 sm3 offset-sm4>
        <v-card>
          <v-card-title primary-title class="about">
            <div>
              <h3 class="headline mb-0 blue--text">
                {{ viewModel.Information.Name }} {{ $t("Resource.MadeBy") }}
                {{ viewModel.Information.MadeBy }}
              </h3>
              <h3 class="headline mb-1 yellow--text">
                v {{ viewModel.Information.Version }}
              </h3>
            </div>
          </v-card-title>
        </v-card>
      </v-flex>
    </v-content>
  </main>
</template>

<script>
const props = {
  viewModel: Object
};

export default {
  props,
  computed: {
    completeDescription() {
      return this.viewModel.Descriptions.join(".<br>") + ".";
    }
  }
};
</script>

<style>
.v-card__title.about {
  word-break: normal;
}
</style>
