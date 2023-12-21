import 'bootstrap/dist/css/bootstrap.min.css'
import 'select2/dist/css/select2.min.css'
import 'jquery-ui/dist/themes/ui-darkness/theme.css'
import 'jquery-ui/dist/themes/ui-darkness/jquery-ui.min.css'
import './assets/main.css'
import 'vue-toastification/dist/index.css'
import '@mdi/font/css/materialdesignicons.css'

// Import the CSS or use your own!

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import { library } from '@fortawesome/fontawesome-svg-core'
import { fas } from '@fortawesome/free-solid-svg-icons'
import { far } from '@fortawesome/free-regular-svg-icons'
import { fab } from '@fortawesome/free-brands-svg-icons'

// Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import {VDataTableServer,VBtn,VCard,VImg,VRating} from 'vuetify/components'
import * as directives from 'vuetify/directives'

import { default as Toast, useToast } from 'vue-toastification'
import { Tabs, Tab } from 'vue3-tabs-component'
import jQuery from 'jquery'
import axios from 'axios'
import select2 from 'select2/dist/js/select2.full.min.js'
import VueScreen from 'vue-screen'
import App from './App.vue'
import router from './router'
;(async () => {
  window.jQuery = window.$ = jQuery

  const app = createApp(App)

  axios.defaults.baseURL = location.origin + '/api/'
  axios.defaults.validateStatus = (status) => status >= 200 && status <= 600
  axios.interceptors.response.use(function (response,val1) {
    var toast = useToast()
    if (response.status >= 500)
      toast.error(
        'A server-based error occurred. The error report has been logged and will be resolved as soon as possible.'
      )
    else if (response.status >= 400)
      toast.warning('You have made a false request. Check your request again.')
    return response
  }, undefined)

  await import('owl.carousel')
  await import('jquery-ui/dist/jquery-ui')
  await import('bootstrap/dist/js/bootstrap.min.js')

  select2()
  library.add(far)
  library.add(fas)
  library.add(fab)
  
  if (import.meta.env.DEV) app.config.compilerOptions.comments = true

  app.component('tabs', Tabs)
  app.component('tab', Tab)

  app.use(createPinia())

  app.use(Toast)

  app.use(VueScreen, 'bootstrap')

  const vuetify = createVuetify({
    components:{VDataTableServer,VBtn,VCard,VImg,VRating},
    directives,
  })
  app.use(vuetify);
  
  app.use(router)

  app.mount('#app')
})()
