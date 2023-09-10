import './assets/main.css'
import 'bootstrap/dist/css/bootstrap.min.css';
import "select2/dist/css/select2.min.css";
import "jquery-ui/dist/themes/ui-darkness/theme.css"
import "jquery-ui/dist/themes/ui-darkness/jquery-ui.min.css"

import { createApp } from 'vue'
import { createPinia } from 'pinia'
import { library } from "@fortawesome/fontawesome-svg-core"
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { fas } from "@fortawesome/free-solid-svg-icons"
import { far } from '@fortawesome/free-regular-svg-icons'
import { fab } from "@fortawesome/free-brands-svg-icons"
import jQuery from 'jquery'
import axios from 'axios'
import select2 from 'select2/dist/js/select2.full.min.js'
import VueScreen from "vue-screen"
import App from './App.vue'
import router from './router'

(async () => {
    const app = createApp(App)

    axios.defaults.baseURL=location.origin+"/api/";
    
    window.jQuery = window.$ = jQuery;
    await import("jquery-ui/dist/jquery-ui");
    select2()
    library.add(far)
    library.add(fas)
    library.add(fab)
    app.component("font-awasome-icon", library)

    app.use(createPinia())
    app.use(VueScreen, "bootstrap")

    app.use(router)

    app.mount('#app')
})()