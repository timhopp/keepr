<template>
  <div class="currentVault">
    <div class="row justify-content-center">
      <h3 class="text-primary">{{vault.name}}</h3>
    </div>
    <div class="row justify-content-center">
      <h5 class="text-primary">{{vault.description}}</h5>
    </div>

    <div class="row justify-content-center">
      <!-- Keeps Modal -->
      <button
        type="button"
        class="btn btn-success m-3"
        data-toggle="modal"
        data-target="#vaultModal"
      >Edit Vault</button>
      <div class="modal fade" id="vaultModal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
          <div class="modal-content">
            <div class="modal-header bg-info">
              <h5 class="modal-title" id="exampleModalLabel">Edit Your Vault</h5>
              <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <div class="modal-body">
              <form @submit.prevent="editVault(),clearSearch()">
                <div class="row ml-4">
                  <label for>Title</label>
                </div>
                <div class="row justify-content-center">
                  <input
                    type="text"
                    class="col-11"
                    placeholder="Title"
                    v-model="editedVault.Title"
                    required
                  />
                </div>
                <div class="row ml-4">
                  <label for>Description</label>
                </div>
                <div class="row justify-content-center">
                  <input
                    type="text"
                    class="col-11"
                    placeholder="Description"
                    v-model="editedVault.Description"
                    required
                  />
                </div>
                <div class="row justify-content-center mt-2">
                  <button
                    type="button"
                    @click="clearSearch()"
                    class="btn btn-secondary m-2"
                    data-dismiss="modal"
                  >Close</button>
                  <button id="closeModal" type="submit" class="btn btn-primary m-2">Edit Vault</button>
                </div>
              </form>
            </div>
            <div class="modal-footer bg-info"></div>
          </div>
        </div>
      </div>
      <button @click="deleteVault(vault.id)" class="btn text-dark btn-danger m-3">Delete</button>
    </div>
    <div class="row justify-content-center">
      <keep v-for="akeep in keeps" :keep="akeep" :key="akeep.id"></keep>
    </div>
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
    $("#vaultModal").on("hidden.bs.modal", () => {
      this.clearSearch();
    });
    $("#closeModal").on("click", () => {
      $("#vaultModal").modal("hide");
    });
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
      this.$router.push({ name: "dashboard", path: "/dashboard" });
    },
    editVault() {
      this.$store.dispatch("editVault", {
        name: this.editedVault.Title,
        description: this.editedVault.Description,
        id: this.$route.params.vaultId,
      });
    },
    clearSearch() {
      this.editedVault = {};
    },
  },
  components: {
    keep,
  },
};
</script>


<style scoped>
</style>