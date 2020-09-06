import Vue from "vue";
import Vuex from "vuex";
import Axios from "axios";
import router from "../router";

Vue.use(Vuex);

let baseUrl = location.host.includes("localhost")
  ? "https://localhost:5001/"
  : "/";

let api = Axios.create({
  baseURL: baseUrl + "api/",
  timeout: 3000,
  withCredentials: true,
});

export default new Vuex.Store({
  state: {
    user: {},
    publicKeeps: [],
    //Do I still need a private keeps?
    privateKeeps: [],
    myKeeps: [],
    vaults: [],
  },
  mutations: {
    setUserInfo(state, userInfo) {
      state.user = userInfo;
    },
    clearUserInfo(state) {
      state.user = {};
    },
    setPublicKeeps(state, publicKeeps) {
      state.publicKeeps = publicKeeps;
    },
    setMyKeeps(state, myKeeps) {
      state.myKeeps = myKeeps;
    },
    setVaults(state, vaults) {
      state.vaults = vaults;
    },
  },
  actions: {
    setBearer({}, bearer) {
      api.defaults.headers.authorization = bearer;
    },
    resetBearer() {
      api.defaults.headers.authorization = "";
    },
    setUser({ commit }, userinfo) {
      commit("setUserInfo", userinfo);
    },
    clearUser({ commit }) {
      commit("clearUserInfo");
    },
    async createKeep({ commit, dispatch }, newKeep) {
      try {
        let res = await api.post("keeps", newKeep);
        console.log(res);
      } catch (error) {
        console.error(error);
      }
    },
    async createVault({ commit }, newVault) {
      try {
        let res = await api.post("vaults", newVault);
        console.log(res);
      } catch (error) {
        console.error(error);
      }
    },
    getPublicKeeps({ commit, dispatch }) {
      api.get("keeps").then((res) => commit("setPublicKeeps", res.data));
      console.log("got keeps");
    },
    getMyKeeps({ commit }) {
      api.get("keeps/user").then((res) => commit("setMyKeeps", res.data));
      console.log("got my keeps");
    },
    getMyVaults({ commit }) {
      api.get("vaults/user").then((res) => commit("setVaults", res.data));
      console.log("got vaults");
    },
  },
});
