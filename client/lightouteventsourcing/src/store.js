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
        createGame(state, data) {
            state.gameName = data.gameName;
            state.boardSize = data.boardSize;
            state.isActive = true;
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
        CreateGame({ commit }, data) {
            axios.post(apiRestHost + "/game/CreateGame",
            {
                'Content-Type': 'application/json'
            },
            {
                params: {
                    gameName: data.gameName, 
                    boardSize: data.boardSize
                }
            }).
                then((response) => {
                    if (response.status == 200) {
                        commit('createGame', data);
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
                    gameName: this.state.gameName, 
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