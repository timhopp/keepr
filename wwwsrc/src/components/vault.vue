<template>
  <div>
    <div
      class="bug bg-warning pl-5 pr-5 p-2 m-4 rounded shadow-sm hover pointer"
      @click="viewVault(vault.id)"
    >
      <h1 class="text-primary text-center">{{ vault.name }}</h1>
      <h4 class="text-primary text-center">{{ vault.description }}</h4>
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
.hover {
  transition: all 0.3s ease;
}
.hover:hover {
  box-shadow: 3px 10px #abcebd !important;
  transform: translateY(-3px);
}
.pointer {
  cursor: pointer;
}
</style>
