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
        },
        toggleLight(state) {
            // addEventCounter? 
            console.log(state);
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
        ToggleLight({ commit }, data) {
            axios.post(apiRestHost + "/game/ToggleLight",
            {
                'Content-Type': 'application/json'
            },
            {
                params: {
                    gameName: 'test', 
                    x: data.x,
                    y: data.y
                }
            }).
                then((response) => {
                    if (response.status == 200) {
                        commit('toggleLight');
                    }
                }).catch((e) => {
                    console.log(e);
                });
        },
    },
    getters: {
    }
});