import 'vuetify/styles'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'jquery-ui/dist/themes/ui-darkness/theme.css'
import 'jquery-ui/dist/themes/ui-darkness/jquery-ui.min.css'
import './assets/main.css'
import 'vue-toastification/dist/index.css'
import '@mdi/font/css/materialdesignicons.css'

// Vuetify
import { createVuetify } from 'vuetify'
import { VDataTableServer, VBtn, VCard, VImg, VRating } from 'vuetify/components'
import * as directives from 'vuetify/directives'

import { createApp } from 'vue'
import { default as Toast } from 'vue-toastification'
import jQuery from 'jquery'
import VueScreen from 'vue-screen'
import App from './App.vue'
import router from './router'
import AxiosConfigSetter from './configs/axios'
import FontAwasomeConfigSetter from './configs/fontawasome'
import piniaConfigSetter from './configs/pinia'
(async () => {
  window.jQuery = window.$ = jQuery

  const app = createApp(App)

  await AxiosConfigSetter(app)
  await FontAwasomeConfigSetter(app)
  await piniaConfigSetter(app)

  await import('owl.carousel')
  await import('jquery-ui/dist/jquery-ui')
  await import('bootstrap/dist/js/bootstrap.min.js')

  if (import.meta.env.DEV) app.config.compilerOptions.comments = true

  app.use(Toast)

  app.use(VueScreen, 'bootstrap')

  const vuetify = createVuetify({
    components: { VDataTableServer, VBtn, VCard, VImg, VRating },
    directives
  })
  app.use(vuetify)

  app.use(router)

  app.directive('OnClickOutsideHandler', {
    mounted(el, binding, vnode) {
      el.clickOutsideEvent = (e) => {
        var eventNode = e.target
        if (!el.contains(eventNode)) {
          binding.value()
        }
      }
      window.addEventListener('click', el.clickOutsideEvent)
    },
    beforeUnmount(el, bindign, vnode) {
      window.removeEventListener('click', el.clickOutsideEvent)
    }
  })

  app.mount('#app')
})()
