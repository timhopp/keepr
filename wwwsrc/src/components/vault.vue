<template>
  <div>
    <div class="bug col-4 bg-warning m-3 p-2 rounded" @click="viewVault(vault.id)">
      <h5>{{vault.name}}</h5>
      <h5>{{vault.description}}</h5>
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