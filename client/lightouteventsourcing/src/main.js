import Vue from 'vue'
import App from './App.vue'
import store from './store'
import router from './router'
import axios from 'axios'


let baseUrl = ''
if (process.env.NODE_ENV === 'production') {
    baseUrl = 'https://localhost:5501';
} else {
    baseUrl = 'https://localhost:5501';
}
export const apiRestHost = baseUrl;

// Setup axios as the Vue default $http library
axios.defaults.baseURL = baseUrl; // same as the Url the server listens to
Vue.prototype.$http = axios

Vue.config.productionTip = false


new Vue({
    router,
    render: h => h(App),
    store
}).$mount('#app')
