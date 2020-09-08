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
    currentKeep: [],
    myKeeps: [],
    vaults: [],
    currentVault: [],
    currentVaultKeeps: [],
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
    setCurrentKeep(state, currentKeep) {
      state.currentKeep = currentKeep;
    },
    setVaults(state, vaults) {
      state.vaults = vaults;
    },
    setCurrentVault(state, currentVault) {
      state.currentVault = currentVault;
    },
    setCurrentVaultKeeps(state, currentVaultKeeps) {
      state.currentVaultKeeps = currentVaultKeeps;
    },
    clearVaultKeeps(state) {
      state.currentVaultKeeps = [];
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
        this.dispatch("getPublicKeeps");
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
    async addToVault({ commit }, data) {
      try {
        let res = await api.post("vaultkeeps", data);
        console.log(res);
      } catch (error) {
        console.error(error);
      }
    },
    async deleteVault({ commit }, vaultId) {
      try {
        api.delete("vaults/" + vaultId);
        console.log("vault deleted");
      } catch (error) {
        console.error(error);
      }
    },
    async deleteKeep({ commit }, id) {
      try {
        api.delete("keeps/" + id);
        console.log("delete");
      } catch (error) {
        console.error(error);
      }
    },
    async editKeep({ dispatch }, editedKeep) {
      try {
        let res = await api.put("keeps/" + editedKeep.id, editedKeep);
        this.dispatch("getCurrentKeep", editedKeep.id);
      } catch (error) {
        console.error(error);
      }
    },
    async editVault({ dispatch }, editedVault) {
      try {
        let res = await api.put("vaults/" + editedVault.id, editedVault);
        this.dispatch("getCurrentVault", editedVault.id);
      } catch (error) {
        console.error(error);
      }
    },

    async deleteVaultKeep({ state, dispatch }, vaultkeepId) {
      try {
        await api.delete("vaultkeeps/" + vaultkeepId);
      } catch (error) {
        console.error(error);
      }
    },
    getPublicKeeps({ commit, dispatch }) {
      api.get("keeps").then((res) => commit("setPublicKeeps", res.data));
    },
    getMyKeeps({ commit }) {
      api.get("keeps/user").then((res) => commit("setMyKeeps", res.data));
    },
    getMyVaults({ commit }) {
      api.get("vaults").then((res) => commit("setVaults", res.data));
    },
    getCurrentKeep({ commit }, id) {
      api.get("keeps/" + id).then((res) => commit("setCurrentKeep", res.data));
    },
    getCurrentVault({ commit }, id) {
      api
        .get("vaults/" + id)
        .then((res) => commit("setCurrentVault", res.data));
    },
    getVaultKeeps({ commit }, id) {
      api
        .get("vaults/" + id + "/keeps")
        .then((res) => commit("setCurrentVaultKeeps", res.data));
    },
    clearVaultKeeps({ commit }) {
      commit("clearVaultKeeps");
    },
    viewKeepCount({ state, commit }, id) {
      let foundKeep = state.publicKeeps.find((keep) => keep.id == id);
      commit("increaseKeepCount", foundKeep);
    },
  },
});
