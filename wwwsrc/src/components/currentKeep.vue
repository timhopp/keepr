<template>
  <div class="currentKeep">
    <div class="row justify-content-center">
      <div class="col-lg-5 col-md-8 col-sm-9 mt-5 p-4 m-4 bg-warning rounded shadow-sm">
        <div class="row justify-content-center">
          <h1 class="text-primary text-center">{{currentKeep.name}}</h1>
        </div>
        <div class="row justify-content-center">
          <img :src="currentKeep.img" class="img-fluid" alt="Img Not Available" />
        </div>
        <div class="row justify-content-center">
          <h5 class="text-primary text-center">{{currentKeep.description}}</h5>
        </div>

        <div class="row justify-content-center">
          <button
            type="button"
            class="btn btn-info m-3"
            data-toggle="modal"
            data-target="#addToVaultModal"
          >Add To Vault</button>

          <!-- Keeps Modal -->
          <button
            v-if="currentKeep.userId == user.sub"
            type="button"
            class="btn btn-success m-3"
            data-toggle="modal"
            data-target="#keepModal"
          >Edit Keep</button>
          <div class="modal fade" id="keepModal" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
              <div class="modal-content">
                <div class="modal-header bg-warning">
                  <h5 class="modal-title" id="exampleModalLabel">Edit Your Keep</h5>
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">
                  <form @submit.prevent="editKeep(currentKeep.id),clearSearch()">
                    <div class="row ml-4 mt-1">
                      <label for>Title</label>
                    </div>
                    <div class="row justify-content-center">
                      <input
                        type="text"
                        class="col-11"
                        placeholder="Title"
                        v-model="editedKeep.Name"
                      />
                    </div>
                    <div class="row ml-4 mt-1">
                      <label for>Description</label>
                    </div>
                    <div class="row justify-content-center">
                      <input
                        type="text"
                        class="col-11"
                        placeholder="Description"
                        v-model="editedKeep.Description"
                      />
                    </div>
                    <div class="row ml-4 mt-1">
                      <label for>Img</label>
                    </div>
                    <div class="row justify-content-center mt-1">
                      <input
                        type="text"
                        class="col-11"
                        placeholder="imgUrl"
                        v-model="editedKeep.Img"
                      />
                    </div>
                    <div class="row justify-content-center mt-3">
                      <label class="form-check-label col-4" for="privateCheck">Private</label>
                      <input
                        type="checkbox"
                        class="form-check-input col-8 ml-3"
                        id="privateCheck"
                        v-model="editedKeep.IsPrivate"
                      />
                    </div>
                    <div class="row justify-content-center">
                      <button
                        id="closeModal"
                        type="submit"
                        class="btn btn-primary mt-3 mr-3"
                      >Edit Keep</button>
                      <button
                        type="button"
                        @submit="clearSearch()"
                        class="btn btn-secondary mt-3 ml-3"
                        data-dismiss="modal"
                      >Close</button>
                    </div>
                  </form>
                </div>
                <div class="modal-footer bg-warning"></div>
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
                <div class="modal-header bg-warning">
                  <h5 class="modal-title" id="exampleModalLabel">Choose A Vault</h5>
                </div>
                <div class="modal-body">
                  <div v-for="vault in vaults" :key="vault.id">
                    <button
                      @click="addToVault(vault.id, currentKeep.id)"
                      class="btn btn-success m-2"
                    >{{vault.name}}</button>
                  </div>
                </div>
                <div class="modal-footer bg-warning">
                  <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
              </div>
            </div>
          </div>
          <button
            v-if="currentKeep.userId == user.sub"
            @click="deleteKeep(currentKeep.id)"
            class="btn btn-danger m-3"
          >Delete</button>
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
      editedKeep: {
        IsPrivate: false,
      },
    };
  },
  mounted() {
    this.$store.dispatch("getCurrentKeep", this.$route.params.keepId);
    this.$store.dispatch("getMyVaults");
    this.$store.dispatch("setUser", this.$auth.user);
    $("#keepModal").on("hidden.bs.modal", () => {
      this.clearSearch();
    });
    $("#closeModal").on("click", () => {
      $("#keepModal").modal("hide");
    });
  },
  computed: {
    currentKeep() {
      return this.$store.state.currentKeep;
    },
    vaults() {
      return this.$store.state.vaults;
    },
    user() {
      return this.$store.state.user;
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
      this.$router.push({ name: "home", path: "/" });
      this.$store.dispatch("getPublicKeeps");
    },
    editKeep(id) {
      this.$store.dispatch("editKeep", {
        id: this.$route.params.keepId,
        name: this.editedKeep.Name,
        description: this.editedKeep.Description,
        img: this.editedKeep.Img,
        isprivate: this.editedKeep.IsPrivate,
      });
    },
    clearSearch() {
      this.editedKeep = {};
    },
  },
  components: {},
};
</script>


<style scoped>
/* @media (min-width: 55em) {
  img {
    max-height: 500px;
    max-width: 250px;
  }
}
@media (min-width: 65em) {
  img {
    max-height: 500px;
    max-width: 300px;
  }
}
@media (min-width: 80em) {
  img {
    max-height: 500px;
    max-width: 350px;
  }
}
@media (min-width: 90em) {
  img {
    max-height: 500px;
    max-width: 550px;
  }
} */
img {
  max-height: 600px;
}

/* .container {
  max-width: 31vw;
} */
</style>