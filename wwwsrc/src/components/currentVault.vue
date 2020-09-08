<template>
  <div class="currentVault">
    You hit the vault
    <div class="bg-warning m-5 p-2">
      {{vault.name}}
      {{vault.description}}
      <button
        @click="deleteVault(vault.id)"
        class="btn btn-info"
      >DELETE</button>
    </div>

    <!-- Keeps Modal -->
    <button
      type="button"
      class="btn btn-success"
      data-toggle="modal"
      data-target="#keepModal"
    >Edit Vault</button>
    <div class="modal fade" id="keepModal" tabindex="-1" role="dialog">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Edit Your Keep</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <form @submit.prevent="editVault()">
              <label for>Title</label>
              <input type="text" placeholder="Title" v-model="editedVault.Title" required />
              <label for>Article Link</label>
              <input type="text" placeholder="Article" v-model="editedVault.Description" required />

              <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
              <button type="submit" class="btn btn-primary">Edit Vault</button>
            </form>
          </div>
          <div class="modal-footer"></div>
        </div>
      </div>
    </div>

    <keep v-for="akeep in keeps" :keep="akeep" :key="akeep.id"></keep>
  </div>
</template>


<script>
import keep from "./keep";
export default {
  name: "currentVault",
  data() {
    return {
      editedVault: {},
    };
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
  methods: {
    deleteVault(id) {
      this.$store.dispatch("deleteVault", id);
    },
    editVault() {
      this.$store.dispatch("editVault", {
        name: this.editedVault.Title,
        description: this.editedVault.Description,
        id: this.$route.params.vaultId,
      });
    },
  },
  components: {
    keep,
  },
};
</script>


<style scoped>
</style>