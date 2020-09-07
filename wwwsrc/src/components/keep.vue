<template>
  <div>
    <div class="bug col-4 bg-warning m-3 p-2 rounded" @click="viewKeep(keep.id)">
      <h5>{{keep.title}}</h5>
      <!-- <img class="img" src="{{keep.img}}" /> -->
      <h5>{{keep.article}}</h5>
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
      console.log(id);
    },
  },
  components: {},
};
</script>


<style scoped>
</style>