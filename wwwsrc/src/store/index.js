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
    privateKeeps: [],
  },
  mutations: {
    setUserInfo(state, userInfo) {
      state.user = userInfo;
    },
    clearUserInfo(state) {
      state.user = {};
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
  },
});
