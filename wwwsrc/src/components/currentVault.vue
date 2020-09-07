<template>
  <div class="currentVault">
    You hit the vault
    <div class="bg-warning m-5 p-2">
      {{vault.name}}
      {{vault.description}}
    </div>
    <keep v-for="akeep in keeps" :keep="akeep" :key="akeep.id"></keep>
  </div>
</template>


<script>
import keep from "./keep";
export default {
  name: "currentVault",
  data() {
    return {};
  },
  mounted() {
    this.$store.dispatch("getCurrentVault", this.$route.params.vaultId);
    this.$store.dispatch("getVaultKeeps", this.$route.params.vaultId);
  },
  computed: {
    vault() {
      return this.$store.state.currentVault;
    },
    keeps() {
      return this.$store.state.currentVaultKeeps;
    },
  },
  methods: {},
  components: {
    keep,
  },
};
</script>


<style scoped>
</style>