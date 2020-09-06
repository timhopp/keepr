<template>
  <div class="dashboard">
    <h1>Your Profile</h1>
    <h3>Your Vaults</h3>
    <div v-for="vault in vaults" :key="vault.id">{{vault.name}} {{vault.description}}</div>
    <h3>Your Keeps</h3>
    <div v-for="keep in myKeeps" :key="keep.id">{{keep.title}}</div>
  </div>
</template>

<script>
export default {
  mounted() {
    this.$store.dispatch("getMyKeeps");
    this.$store.dispatch("getMyVaults");
  },
  computed: {
    vaults() {
      return this.$store.state.vaults;
    },
    //How do I want to do this?
    myKeeps() {
      return this.$store.state.myKeeps;
    },
  },
};
</script>

<style></style>
