import axios from 'axios';
import Vue from 'vue';
import Vuex from 'vuex';
import { apiRestHost } from "@/main.js";
import router from './router'

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        gameName: "",
        boardSize: 0,
        lightsOn: null,
        isActive: false
    },
    mutations: {
        createGame(state, gameName) {
            state.gameName = gameName;
            router.push('/Game');
        },
        toggleLight(state) {
            // addEventCounter? 
            console.log(state);
        },
        update(state, data) {
            state.boardSize = data.boardSize;
            state.lightsOn = data.lightsOn.on;
            state.isActive = data.isActive;
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
        Update({ commit }, data) {
            commit('update', data);
            console.log('updated');
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