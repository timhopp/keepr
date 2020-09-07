<template>
  <div class="currentKeep">
    <h5>Hit</h5>
    <h5>{{currentKeep.title}}</h5>

    <button
      type="button"
      class="btn btn-info"
      data-toggle="modal"
      data-target="#addToVaultModal"
    >Add To Vault</button>

    <!-- Modal -->
    <div
      class="modal fade"
      id="addToVaultModal"
      tabindex="-1"
      role="dialog"
      aria-labelledby="exampleModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Choose A Vault</h5>
          </div>
          <div class="modal-body">
            <div v-for="vault in vaults" :key="vault.id">
              <button
                @click="addToVault(vault.id, currentKeep.id)"
                class="btn btn-info"
              >{{vault.name}}</button>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  name: "currentKeep",
  data() {
    return {};
  },
  mounted() {
    this.$store.dispatch("getCurrentKeep", this.$route.params.keepId);
    this.$store.dispatch("getMyVaults");
  },
  computed: {
    currentKeep() {
      return this.$store.state.currentKeep;
    },
    vaults() {
      return this.$store.state.vaults;
    },
  },
  methods: {
    addToVault(vault, keep) {
      this.$store.dispatch("addToVault", {
        vaultId: vault,
        keepId: keep,
      });
    },
  },
  components: {},
};
</script>


<style scoped>
</style>