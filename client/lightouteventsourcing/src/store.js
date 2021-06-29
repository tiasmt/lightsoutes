import axios from 'axios';
import Vue from 'vue';
import Vuex from 'vuex';
import { apiRestHost } from "@/main.js";
import router from './router'

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        gameName: ""
    },
    mutations: {
        createGame(state, gameName) {
            state.gameName = gameName;
            state.isAdmin = true;
            state.inProgress = false;
            router.push('/Categories');
        }
    },

    actions: {
        CreateGame({ commit }) {
            axios.post(apiRestHost + "/game/CreateGame",
            {
                'Content-Type': 'application/json'
            },
            {
                params: {
                    gameName: 'test',
                    boardSize: 2
                }
            }).
                then((response) => {
                    if (response.status == 200) {
                        commit('createGame', response.data);
                    }
                }).catch((e) => {
                    console.log(e);
                });
        },
    },
    getters: {
    }
});