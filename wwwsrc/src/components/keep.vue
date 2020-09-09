<template>
  <div>
    <div
      v-if="this.$store.state.currentVaultKeeps.length > 0"
      class="bug bg-warning m-3 p-2 rounded"
    >
      <div @click="viewKeep(keep.id)">
        <h3>{{keep.name}}</h3>
        <h5>{{keep.description}}</h5>
        <img :src="keep.img" alt="No Image" class="img" />
        <div class="row">
          <div class="col-3">{{keep.views}}</div>
          <div class="col-3">{{keep.shares}}</div>
          <div class="col-3">{{keep.keeps}}</div>
        </div>
      </div>
      <button @click="deleteVaultKeep(keep.vaultKeepId)" class="btn btn-info m-2">Remove From Vault</button>
    </div>
    <div v-else class="bug bg-warning m-3 p-2 rounded" @click="viewKeep(keep.id)">
      <h3>{{keep.name}}</h3>
      <img :src="keep.img" alt="No Image" class="img" />
      <h5>{{keep.description}}</h5>
      <div class="row justify-content-center">
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
      console.log(id);
    },
    deleteVaultKeep(id) {
      this.$store.dispatch("deleteVaultKeep", {
        id: id,
        vaultId: this.$route.params.vaultId,
      });
    },
  },
  components: {},
};
</script>


<style scoped>
img {
  width: 400px;
}
</style>