<template>
  <div id="app">
    <navbar />
    <transition name="slide-fade" mode="out-in">
      <router-view />
    </transition>
  </div>
</template>

<script>
import Navbar from "@/components/navbar";
import { onAuth } from "@bcwdev/auth0-vue";
export default {
  name: "App",
  async beforeCreate() {
    await onAuth();
    this.$store.dispatch("setBearer", this.$auth.bearer);
  },
  components: {
    Navbar,
  },
};
</script>

<style lang="scss">
@import "./assets/_variables.scss";
@import "bootstrap";
@import "./assets/_overrides.scss";
body {
  background-image: linear-gradient(To Top, #f4f1def1, #f4f1def5),
    url("https://storage.ning.com/topology/rest/1.0/file/get/2769063122?profile=original");
  overflow-x: hidden;
}

.slide-fade-enter-active {
  transition: all 0.4s ease;
}
.slide-fade-leave-active {
  transition: all 0.2s cubic-bezier(1, 0.5, 0.8, 1);
}
.slide-fade-enter,
.slide-fade-leave-to {
  background-image: linear-gradient(to bottom left, #f4f1de, #f4f1de);
  opacity: 0;
}
</style>
