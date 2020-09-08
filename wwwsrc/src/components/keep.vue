<template>
  <div>
    <div
      v-if="this.$store.state.currentVaultKeeps.length > 0"
      class="bug col-4 bg-warning m-3 p-2 rounded"
    >
      <button @click="deleteVaultKeep(keep.vaultKeepId)" class="btn btn-info">Remove From Vault</button>
      <div @click="viewKeep(keep.id)">
        <h5>{{keep.name}}</h5>
        <!-- <img class="img" src="{{keep.img}}" /> -->
        <h5>{{keep.description}}</h5>
        <div class="row">
          <div class="col-3">{{keep.views}}</div>
          <div class="col-3">{{keep.shares}}</div>
          <div class="col-3">{{keep.keeps}}</div>
        </div>
      </div>
    </div>
    <div v-else class="bug col-4 bg-warning m-3 p-2 rounded" @click="viewKeep(keep.id)">
      <h5>{{keep.name}}</h5>
      <!-- <img class="img" src="{{keep.img}}" /> -->
      <h5>{{keep.description}}</h5>
      <div class="row">
        <div class="col-3">{{keep.views}}</div>
        <div class="col-3">{{keep.shares}}</div>
        <div class="col-3">{{keep.keeps}}</div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  name: "keep",
  props: ["keep"],
  data() {
    return {};
  },
  computed: {},
  methods: {
    viewKeep(id) {
      this.$router.push({ name: "currentKeep", params: { keepId: id } });
      this.$store.dispatch("keepViewCount", id);
      console.log(id);
    },
    deleteVaultKeep(id) {
      this.$store.dispatch("deleteVaultKeep", id);
    },
  },
  components: {},
};
</script>


<style scoped>
</style>