import 'bootstrap/dist/css/bootstrap.min.css';
import "select2/dist/css/select2.min.css";
import "jquery-ui/dist/themes/ui-darkness/theme.css"
import "jquery-ui/dist/themes/ui-darkness/jquery-ui.min.css"
import './assets/main.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import { library } from "@fortawesome/fontawesome-svg-core"
import { fas } from "@fortawesome/free-solid-svg-icons"
import { far } from '@fortawesome/free-regular-svg-icons'
import { fab } from "@fortawesome/free-brands-svg-icons"

import {Tabs, Tab} from 'vue3-tabs-component';
import jQuery from 'jquery'
import axios from 'axios'
import select2 from 'select2/dist/js/select2.full.min.js'
import VueScreen from "vue-screen"
import App from './App.vue'
import router from './router'

(async () => {
    window.jQuery = window.$ = jQuery;

    const app = createApp(App)

    axios.defaults.baseURL=location.origin+"/api/";
    
    await import('owl.carousel');
    await import("jquery-ui/dist/jquery-ui");
    await import("bootstrap/dist/js/bootstrap.min.js")

    select2()
    library.add(far)
    library.add(fas)
    library.add(fab)
    if(import.meta.env.DEV)
    app.config.compilerOptions.comments = true

    app.component("tabs",Tabs);
    app.component("tab",Tab);

    app.use(createPinia())

    app.use(VueScreen, "bootstrap")

    app.use(router)

    app.mount('#app')
})()