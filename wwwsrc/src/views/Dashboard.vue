<template>
  <div class="dashboard">
    <h1>Your Profile</h1>
  </div>
</template>

<script>
export default {
  mounted() {
    this.$store.dispatch("getMyKeeps");
  },
  computed: {},
};
</script>

<style></style>
