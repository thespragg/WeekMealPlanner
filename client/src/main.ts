import { createApp } from 'vue'
import App from './App.vue'
import "./index.css"
import { createPinia } from 'pinia'

import { library } from "@fortawesome/fontawesome-svg-core";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { faPencil } from "@fortawesome/free-solid-svg-icons";

library.add(faPencil);

const pinia = createPinia();
createApp(App)
    .component("font-awesome-icon", FontAwesomeIcon)
    .use(pinia)
    .mount('#app')
