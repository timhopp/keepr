import Vue from "vue";
import Router from "vue-router";
// @ts-ignore
import Home from "./views/Home.vue";
// @ts-ignore
import Dashboard from "./views/Dashboard.vue";
import currentKeep from "./components/currentKeep.vue";
import currentVault from "./components/currentVault.vue";
import { authGuard } from "@bcwdev/auth0-vue";

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: "/",
      name: "home",
      component: Home,
    },
    {
      path: "/dashboard",
      name: "dashboard",
      component: Dashboard,
      beforeEnter: authGuard,
    },
    {
      path: "/:keepId",
      name: "currentKeep",
      component: currentKeep,
      beforeEnter: authGuard,
    },
    {
      path: "/:vaultId",
      name: "currentVault",
      component: currentVault,
      beforeEnter: authGuard,
    },
  ],
});
