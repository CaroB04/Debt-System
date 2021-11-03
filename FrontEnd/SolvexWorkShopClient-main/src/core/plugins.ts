import Vue from 'vue'

//Buefy
import Buefy from 'buefy'
import 'buefy/dist/buefy.css'

Vue.use(Buefy)


//VeeValidate
import es from 'vee-validate/dist/locale/es'
import VeeValidate, { Validator } from 'vee-validate';
// Install the Plugin.
Vue.use(VeeValidate);
// Localize takes the locale object as the second argument (optional) and merges it.
Validator.localize('es', es);


import VueTheMask from "vue-the-mask";
Vue.use(VueTheMask);