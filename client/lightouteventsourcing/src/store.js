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
            router.push('/Game');
        }
    },

    actions: {
        //TODO remove hard coded params
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
                        commit('createGame', 'test');
                    }
                }).catch((e) => {
                    console.log(e);
                });
        },
    },
    getters: {
    }
});