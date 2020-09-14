<template>
  <div class="dashboard">
    <h1 class="text-primary text-center">Your Vaults</h1>
    <div class="row justify-content-center">
      <vault v-for="aVault in vaults" :vault="aVault" :key="aVault.id"></vault>
    </div>
    <h1 class="text-primary text-center">Your Keeps</h1>
    <div class="row justify-content-center">
      <div class="card-columns">
        <keep v-for="currentKeep in myKeeps" :keep="currentKeep" :key="currentKeep.id"></keep>
      </div>
    </div>
  </div>
</template>

<script>
import keep from "../components/keep";
import vault from "../components/vault";
export default {
  props: ["keep"],
  mounted() {
    this.$store.dispatch("getMyKeeps");
    this.$store.dispatch("getMyVaults");
    this.$store.dispatch("clearVaultKeeps");
  },
  computed: {
    vaults() {
      return this.$store.state.vaults;
    },
    //How do I want to do this?
    myKeeps() {
      return this.$store.state.myKeeps;
    },
  },
  components: {
    keep,
    vault,
  },
};
</script>

<style>
.dashboard {
  background-image: linear-gradient(To Top, #f4f1def1, #f4f1def5),
    url("https://storage.ning.com/topology/rest/1.0/file/get/2769063122?profile=original");
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
