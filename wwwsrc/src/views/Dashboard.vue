<template>
  <div class="dashboard">
    <h1>Your Profile</h1>
    <h3>Your Vaults</h3>
    <vault v-for="aVault in vaults" :vault="aVault" :key="aVault.id"></vault>
    <h3>Your Keeps</h3>
    <keep v-for="currentKeep in myKeeps" :keep="currentKeep" :key="currentKeep.id"></keep>
  </div>
</template>

<script>
import keep from "../components/keep";
import vault from "../components/vault";
export default {
  props: ["keep"],
  mounted() {
    this.$store.dispatch("getMyKeeps");
    this.$store.dispatch("getMyVaults");
    this.$store.dispatch("clearVaultKeeps");
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
  components: {
    keep,
    vault,
  },
};
</script>

<style></style>
