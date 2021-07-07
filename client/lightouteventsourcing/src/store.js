import axios from 'axios';
import Vue from 'vue';
import Vuex from 'vuex';
import { apiRestHost } from "@/main.js";
import router from './router'

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        gameName: "",
        boardSize: null,
        lightsOn: null,
        isActive: false
    },
    mutations: {
        createGame(state, data) {
            state.gameName = data.gameName;
            state.boardSize = parseInt(data.boardSize);
            state.isActive = true;
            router.push('/Game');
        },
        toggleLight(state) {
            console.log(state);
        },
        update(state, data) {
            state.lightsOn = data.lightsOn.on;
            state.isActive = data.isActive;
            state.boardSize = parseInt(data.boardSize);
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
        getBoardSize: state => {
            return state.boardSize;
        },
        getGameName: state => {
            return state.gameName;
        },
        getLightsOn: state => {
            return state.lightsOn;
        },
        getIsActive: state => {
            return state.isActive;
        }
    }
});