import 'bootstrap/dist/css/bootstrap.min.css'
import 'jquery-ui/dist/themes/ui-darkness/theme.css'
import 'jquery-ui/dist/themes/ui-darkness/jquery-ui.min.css'
import './assets/main.css'
import 'vue-toastification/dist/index.css'
import '@mdi/font/css/materialdesignicons.css'

import { createPinia } from 'pinia'
// Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import { VDataTableServer, VBtn, VCard, VImg, VRating } from 'vuetify/components'
import * as directives from 'vuetify/directives'

import { createApp } from 'vue'
import { default as Toast } from 'vue-toastification'
import { Tabs, Tab } from 'vue3-tabs-component'
import jQuery from 'jquery'
import VueScreen from 'vue-screen'
import App from './App.vue'
import router from './router'
import AxiosConfigSetter from './configs/axios'
import FontAwasomeConfigSetter from './configs/fontawasome'
(async () => {
  window.jQuery = window.$ = jQuery

  const app = createApp(App)

  AxiosConfigSetter(app);
  FontAwasomeConfigSetter(app);
  await import('owl.carousel')
  await import('jquery-ui/dist/jquery-ui')
  await import('bootstrap/dist/js/bootstrap.min.js')

  if (import.meta.env.DEV) app.config.compilerOptions.comments = true

  app.component('tabs', Tabs)
  app.component('tab', Tab)

  app.use(createPinia())

  app.use(Toast)

  app.use(VueScreen, 'bootstrap')

  const vuetify = createVuetify({
    components: { VDataTableServer, VBtn, VCard, VImg, VRating },
    directives
  })
  app.use(vuetify)

  app.use(router)

  app.directive("OnClickOutsideHandler",{
    mounted(el, binding, vnode) {
        el.clickOutsideEvent=(e)=>{
            var eventNode = e.target;
            if (!el.contains(eventNode)) {
                binding.value();
            }
        }
        window.addEventListener("click",el.clickOutsideEvent)
    },
    beforeUnmount(el,bindign,vnode){
        window.removeEventListener("click",el.clickOutsideEvent)
    }
})

  app.mount('#app')
})()
