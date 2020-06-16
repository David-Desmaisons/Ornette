<template>
  <div class="play">
    <icon-button icon="mdi-skip-backward" :command="player.Back" fab small />

    <icon-button icon="mdi-play" :command="player.Play" fab :small="false"/>

    <icon-button icon="mdi-skip-forward" :command="player.Next" fab small />
  </div>
</template>

<script>
export default {
  name: "complete-play",
  props: {
    player: {
      required: true,
      type: Object
    }
  }
};
</script>
