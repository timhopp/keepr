<template>
  <div>
    <div class="bug bg-success pl-5 pr-5 p-2 m-2 rounded" @click="viewVault(vault.id)">
      <h1 class="text-center">{{vault.name}}</h1>
      <h3 class="text-center">{{vault.description}}</h3>
    </div>
  </div>
</template>


<script>
export default {
  name: "vault",
  props: ["vault"],
  data() {
    return {};
  },
  computed: {
    vaultKeeps() {
      return this.$store.state.currentVaultKeeps;
    },
  },
  methods: {
    viewVault(id) {
      this.$router.push({ name: "currentVault", params: { vaultId: id } });
      console.log(id);
    },
  },
  components: {},
};
</script>


<style scoped>
</style>