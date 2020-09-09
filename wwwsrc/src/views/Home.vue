<template>
  <div class="home pt-3">
    <h1 class="row justify-content-center text-light">Welcome To Keeper</h1>

    <!-- Keeps Modal -->
    <div class="row justify-content-center">
      <button
        type="button"
        class="btn btn-success m-3"
        data-toggle="modal"
        data-target="#keepModal"
      >Create Keep</button>
      <div class="modal fade" id="keepModal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title" id="exampleModalLabel">Create A Keep</h5>
              <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <div class="modal-body">
              <form @submit.prevent="createKeep(newKeep)">
                <div class="row ml-4 mt-1">
                  <label for>Title</label>
                </div>
                <div class="row justify-content-center">
                  <input
                    type="text"
                    class="col-11"
                    placeholder="Title"
                    v-model="newKeep.Name"
                    required
                  />
                </div>
                <div class="row ml-4 mt-1">
                  <label for>Article Link</label>
                </div>
                <div class="row justify-content-center">
                  <input
                    type="text"
                    class="col-11"
                    placeholder="Article"
                    v-model="newKeep.Description"
                    required
                  />
                </div>
                <div class="row ml-4 mt-1">
                  <label for>Img</label>
                </div>
                <div class="row justify-content-center">
                  <input type="text" class="col-11" placeholder="imgUrl" v-model="newKeep.Img" />
                </div>
                <div class="row mt-1 ml-5 justify-content-center">
                  <label class="form-check-label col-6" for="privateCheck">Private</label>
                  <input
                    type="checkbox"
                    class="form-check-input col-6"
                    id="privateCheck"
                    v-model="newKeep.IsPrivate"
                  />
                </div>
                <div class="row justify-content-center mt-3">
                  <button type="submit" class="btn btn-primary m-2">Create Keep</button>
                  <button type="button" class="btn btn-secondary m-2" data-dismiss="modal">Close</button>
                </div>
              </form>
            </div>
            <div class="modal-footer"></div>
          </div>
        </div>
      </div>
      <!-- Vaults Modal -->
      <button
        type="button"
        class="btn btn-success m-3"
        data-toggle="modal"
        data-target="#vaultModal"
      >Create Vault</button>
      <div class="modal fade" id="vaultModal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title" id="exampleModalLabel">Create A Vault</h5>
              <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <div class="modal-body">
              <form @submit.prevent="createVault(newVault)">
                <div>
                  <div class="row ml-4">
                    <label for>Name</label>
                  </div>
                  <div class="row justify-content-center">
                    <input
                      type="text"
                      class="col-11"
                      placeholder="Name"
                      v-model="newVault.Name"
                      required
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
                      v-model="newVault.Description"
                      required
                    />
                  </div>
                  <div class="row justify-content-center mt-2">
                    <button type="submit" class="btn btn-primary m-2">Create Vault</button>
                    <button type="button" class="btn btn-secondary m-2" data-dismiss="modal">Close</button>
                  </div>
                </div>
              </form>
            </div>
            <div class="modal-footer"></div>
          </div>
        </div>
      </div>
    </div>
    <div class="row justify-content-center">
      <keep v-for="keepItem in publicKeeps" :keep="keepItem" :key="keepItem.id"></keep>
    </div>
  </div>
</template>

<script>
import keep from "../components/keep";
export default {
  name: "home",
  data() {
    return {
      newKeep: {},
      newVault: {},
    };
  },
  props: ["keep"],
  computed: {
    user() {
      return this.$store.state.user;
    },
    publicKeeps() {
      return this.$store.state.publicKeeps;
    },
  },
  mounted() {
    this.$store.dispatch("getPublicKeeps");
    this.$store.dispatch("clearVaultKeeps");
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    },
    createKeep(newKeep) {
      this.$store.dispatch("createKeep", this.newKeep);
    },
    createVault(newVault) {
      this.$store.dispatch("createVault", this.newVault);
    },
  },
  components: {
    keep,
  },
};
</script>
