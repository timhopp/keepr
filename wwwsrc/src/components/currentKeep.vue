<template>
  <div class="currentKeep">
    <h5>{{currentKeep.name}}</h5>
    <h5>{{currentKeep.description}}</h5>

    <button
      type="button"
      class="btn btn-info"
      data-toggle="modal"
      data-target="#addToVaultModal"
    >Add To Vault</button>
    <button @click="deleteKeep(currentKeep.id)" class="btn btn-info">Delete</button>

    <!-- Keeps Modal -->
    <button
      type="button"
      class="btn btn-success"
      data-toggle="modal"
      data-target="#keepModal"
    >Edit Keep</button>
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
            <form @submit.prevent="editKeep(currentKeep.id)">
              <label for>Title</label>
              <input type="text" placeholder="Title" v-model="editedKeep.Name" required />
              <label for>Article Link</label>
              <input type="text" placeholder="Article" v-model="editedKeep.Description" required />
              <label for>Img</label>
              <input type="text" placeholder="imgUrl" v-model="editedKeep.Img" />
              <label class="form-check-label" for="privateCheck">Private</label>
              <input
                type="checkbox"
                class="form-check-input m-3"
                id="privateCheck"
                v-model="editKeep.IsPrivate"
              />
              <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
              <button type="submit" class="btn btn-primary">Edit Keep</button>
            </form>
          </div>
          <div class="modal-footer"></div>
        </div>
      </div>
    </div>

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
    return {
      editedKeep: {},
    };
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
    deleteKeep(id) {
      this.$store.dispatch("deleteKeep", id);
    },
    editKeep(id) {
      this.$store.dispatch("editKeep", {
        id: this.$route.params.keepId,
        name: this.editedKeep.Name,
        description: this.editedKeep.Description,
        img: this.editedKeep.Img,
        // private: this.editedKeep.IsPrivate,
      });
    },
  },
  components: {},
};
</script>


<style scoped>
</style>