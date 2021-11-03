import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'

import './core/plugins';
import './core/components';
import './core/filters';

Vue.config.productionTip = false

new Vue({
  data () {
    return {
      deadlines: [
        { name: "Mensual", value: "Monthly" },
        { name: "Quincenal", value: "Biweekly" },
        { name: "Anual", value: "Annual" },
      ],
    }
  },
  router,
  store,
  render: h => h(App)
}).$mount('#app');
