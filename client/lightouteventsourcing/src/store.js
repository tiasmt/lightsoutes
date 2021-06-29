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
            axios.get(apiRestHost + "/game/CreateGame").
                then((response) => {
                    if (response.status == 200) {
                        commit('createGame', response.data);
                    }
                }).catch((e) => {
                    commit('setError', {
                        error: e.response.data.error
                    });
                });
        },

        Join({ commit }, data) {
            commit('join', data);
        },

        ObserveGame({ commit }, data) {
            commit('observeGame', data);
        },

        SetCategory({ commit }, category) {
            axios.post(apiRestHost + "/game/SetCategory",
                {
                    'Content-Type': 'application/json'
                },
                {
                    params: {
                        gameName: this.state.gameName,
                        gameCategory: category
                    }
                }).
                then((response) => {
                    if (response.status == 200) {
                        commit('setCategory', response.data);
                    }
                }).catch((e) => {
                    commit('setError', {
                        error: e.response.data.error
                    });
                });
        },

        SetSettings({ commit }, data) {
            var body = {
                TimePerQuestion: data.secondsPerQuestion,
                TotalNumberOfQuestions: data.numberOfQuestions,
                NumberOfPlayers: data.numberOfPlayers
            };
            axios.post(apiRestHost + "/game/SetSettings", JSON.stringify(body),
                {
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    params: {
                        gameName: this.state.gameName,
                        gameCategory: this.state.category
                    }
                },
            ).
                then((response) => {
                    if (response.status == 200) {
                        commit('setSettings', body);
                    }
                }).catch((e) => {
                    commit('setError', {
                        error: e.response.data.error
                    });
                });
        },

        CreatePlayer({ commit }, data) {
            data.username ??= 'TestPlayer';
            console.log(data.username);
            axios.post(apiRestHost + "/game/CreatePlayer",
                {
                    'Content-Type': 'application/json'
                },
                {
                    params: {
                        username: data.username,
                        avatar: data.avatar,
                        gameName: this.state.gameName
                    }
                }).
                then((response) => {
                    if (response.status == 200) {
                        data.id = response.data;
                        commit('createPlayer', data);
                    }
                }).catch((e) => {
                    commit('setError', {
                        error: e.response.data.error
                    });
                });
        },

        StartGame({ commit }, data) {
            axios.post(apiRestHost + "/game/StartGame",
                {
                    'Content-Type': 'application/json'
                },
                {
                    params: {
                        gameName: data.gameName,
                    }
                }).
                then((response) => {
                    if (response.status == 200) {
                        commit('startGame', data);
                    }
                }).catch((e) => {
                    commit('setError', {
                        error: e.response.data.error
                    });
                });
        },

        CheckAnswer({ commit }, data) {
            return axios.post(apiRestHost + "/game/CheckAnswer",
                {
                    'Content-Type': 'application/json'
                },
                {
                    params: {
                        gameName: data.gameName,
                        username: data.username,
                        questionId: data.questionId,
                        answerId: data.answerId
                    }
                }).
                then((response) => {
                    if (response.status == 200) {
                        return response.data;
                    }
                }).catch((e) => {
                    commit('setError', {
                        error: e.response.data.error
                    });
                });
        },
        GetQuestion({ commit }) {
            var state = this.state;
            axios.post(apiRestHost + "/game/GetQuestion",
                {
                    'Content-Type': 'application/json'
                },
                {
                    params: {
                        gameName: state.gameName,
                        questionId: state.questionId++,
                    }
                }).
                then((response) => {
                    if (response.status == 200) {
                        console.log("Ok");
                    }
                }).catch((e) => {
                    commit('setError', {
                        error: e.response.data.error
                    });
                });
        },

        PlayerJoined({ commit }, data) {
            commit('playerJoined', data);
        },

        JoinGame({ commit }, data) {
            axios.post(apiRestHost + "/game/JoinGame",
                {
                    'Content-Type': 'application/json'
                },
                {
                    params: {
                        gameName: data
                    }
                }).
                then((response) => {
                    if (response.status == 200) {
                        commit('joinGame', response.data);
                    }
                }).catch((e) => {
                    commit('setError', {
                        error: e.response.data.error
                    });
                });
        },
        GameStarted({ commit }) {
            commit('gameStarted')
        },
        UpdateLeaderboard({ commit }, data) {
            commit('updateLeaderboard', data)
        },
        Stop({ commit }) {
            axios.post(apiRestHost + "/game/StopEverything",
                {
                    'Content-Type': 'application/json'
                }).catch((e) => {
                    commit('setError', {
                        error: e.response.data.error
                    });
                });
        },



    },
    getters: {
    }
});