<template>
  <div class="home">
    <h1>Welcome To Keeper</h1>
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
    <div v-for="keep in publicKeeps" :key="keep.id">{{keep.title}} {{keep.article}}</div>
  </div>
</template>

<script>
export default {
  name: "home",
  data() {
    return {
      newKeep: {},
    };
  },
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
  },
};
</script>
