<template>
  <div class="home pt-3">
    <!-- Keeps Modal -->
    <div class="row justify-content-center">
      <button
        type="button"
        class="btn btn-success m-3"
        data-toggle="modal"
        data-target="#keepModal"
      >Create Keep</button>
      <div class="modal fade justify-content-center" id="keepModal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
          <div class="modal-content">
            <div class="modal-header bg-info">
              <h5 class="modal-title" id="exampleModalLabel">Create A Keep</h5>
              <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <div class="modal-body">
              <form @submit.prevent="createKeep(newKeep), clearKeepCreate()">
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
                  <label for>Description</label>
                </div>
                <div class="row justify-content-center">
                  <input
                    type="text"
                    class="col-11"
                    placeholder="Descriptions"
                    v-model="newKeep.Description"
                    required
                  />
                </div>
                <div class="row ml-4 mt-1">
                  <label for>Img</label>
                </div>
                <div class="row justify-content-center">
                  <input
                    type="text"
                    class="col-11"
                    placeholder="imgUrl"
                    v-model="newKeep.Img"
                    required
                  />
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
                  <button id="closeKeepModal" type="submit" class="btn btn-primary m-2">Create Keep</button>
                  <button
                    @click="clearKeepCreate()"
                    type="button"
                    class="btn btn-secondary m-2"
                    data-dismiss="modal"
                  >Close</button>
                </div>
              </form>
            </div>
            <div class="modal-footer bg-info"></div>
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
            <div class="modal-header bg-info">
              <h5 class="modal-title" id="exampleModalLabel">Create A Vault</h5>
              <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <div class="modal-body">
              <form @submit.prevent="createVault(newVault), clearVaultCreate">
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
                    <button
                      id="closeVaultModal"
                      type="submit"
                      class="btn btn-primary m-2"
                    >Create Vault</button>
                    <button
                      @click="clearVaultCreate()"
                      type="button"
                      class="btn btn-secondary m-2"
                      data-dismiss="modal"
                    >Close</button>
                  </div>
                </div>
              </form>
            </div>
            <div class="modal-footer bg-info"></div>
          </div>
        </div>
      </div>
    </div>
    <div class="row justify-content-center">
      <div class="card-columns">
        <keep v-for="keepItem in publicKeeps" :keep="keepItem" :key="keepItem.id"></keep>
      </div>
    </div>
    <div id="fad"></div>
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
    $("#keepModal").on("hidden.bs.modal", () => {
      this.clearKeepCreate();
    });
    $("#closeKeepModal").on("click", () => {
      $("#keepModal").modal("hide");
    });
    $("#vaultModal").on("hidden.bs.modal", () => {
      this.clearVaultCreate();
    });
    $("#closeVaultModal").on("click", () => {
      $("#vaultModal").modal("hide");
    });
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
    clearVaultCreate() {
      this.newVault = {};
    },
    clearKeepCreate() {
      this.newKeep = {};
    },
    // resizeMasonryItem(item) {
    //   /* Get the grid object, its row-gap, and the size of its implicit rows */
    //   let grid = document.getElementsByClassName("masonry")[0],
    //     rowGap = parseInt(
    //       window.getComputedStyle(grid).getPropertyValue("grid-row-gap")
    //     ),
    //     rowHeight = parseInt(
    //       window.getComputedStyle(grid).getPropertyValue("grid-auto-rows")
    //     );
    //   let rowSpan = Math.ceil(
    //     (item.querySelector(".card-content").getBoundingClientRect().height +
    //       rowGap) /
    //       (rowHeight + rowGap)
    //   );

    //   /* Set the spanning as calculated above (S) */
    //   item.style.gridRowEnd = "span " + rowSpan;
    // },
    // resizeAllMasonryItems() {
    //   // Get all item class objects in one list
    //   let allItems = document.getElementsByClassName("card");

    //   /*
    //    * Loop through the above list and execute the spanning function to
    //    * each list-item (i.e. each masonry item)
    //    */
    //   for (let i = 0; i < allItems.length; i++) {
    //     this.resizeMasonryItem(allItems[i]);
    //   }
    // },
  },
  components: {
    keep,
  },
};
</script>

<style scoped>
.home {
  background-image: linear-gradient(To Top, #f4f1def1, #f4f1def5),
    url("https://storage.ning.com/topology/rest/1.0/file/get/2769063122?profile=original");
}
/* #fad {
  position: fixed !important;
  height: 100vh;
  width: 100vw;
  background-color: #f4f1de8f;
} */

.btn-success:hover {
  transition: All 0.2s linear;
  background-color: #f5be5e !important;
}

@media (min-width: 1em) {
  .card-columns {
    column-count: 1;
  }
}

@media (min-width: 45em) {
  .card-columns {
    column-count: 2;
  }
}

@media (min-width: 67em) {
  .card-columns {
    column-count: 3;
  }
}

@media (min-width: 90em) {
  .card-columns {
    column-count: 4;
  }
}
@media (min-width: 105em) {
  .card-columns {
    column-count: 5;
  }
}
</style>
