<template>
  <div class="home">
    <h1>Welcome To Keeper</h1>

    <!-- Keeps Modal -->
    <button
      type="button"
      class="btn btn-success"
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
              <label for>Title</label>
              <input type="text" placeholder="Title" v-model="newKeep.Title" required />
              <label for>Article Link</label>
              <input type="text" placeholder="Article" v-model="newKeep.Article" required />
              <label for>Img</label>
              <input type="text" placeholder="imgUrl" v-model="newKeep.Img" />
              <label class="form-check-label" for="privateCheck">Private</label>
              <input
                type="checkbox"
                class="form-check-input m-3"
                id="privateCheck"
                v-model="newKeep.IsPrivate"
              />
              <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
              <button type="submit" class="btn btn-primary">Create Keep</button>
            </form>
          </div>
          <div class="modal-footer"></div>
        </div>
      </div>
    </div>
    <!-- Vaults Modal -->
    <button
      type="button"
      class="btn btn-success"
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
              <label for>Name</label>
              <input type="text" placeholder="Name" v-model="newVault.Name" required />
              <label for>Description</label>
              <input type="text" placeholder="Description" v-model="newVault.Description" required />
              <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
              <button type="submit" class="btn btn-primary">Create Vault</button>
            </form>
          </div>
          <div class="modal-footer"></div>
        </div>
      </div>
    </div>
    <keep v-for="keepItem in publicKeeps" :keep="keepItem" :key="keepItem.id"></keep>
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
