<script setup>
import {DashboardMenuItemValue} from "./"
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
defineProps({
    value:{
        type:DashboardMenuItemValue,
        required:true
    }
})
</script>
<template>
    <li v-if="!value.isHr">
        <button v-if="value.childItems.length>0" class="btn dashboard_submenu_collapser" @click="collapseSubmenu">
            <FontAwesomeIcon icon="angle-down" />
        </button>
        <RouterLink to="/">
            <FontAwesomeIcon :icon="value.icon" />{{ value.text }}
        </RouterLink>
        <ul class="dashboard_submenu"  v-if="value.childItems.length>0">
            <DashboardMenuItem v-for="item in value.childItems" :value="item" />
        </ul>
    </li>
    <li v-else>
        <hr>
    </li>
</template>
<script>
export default {
    methods: {
        collapseSubmenu(e) {
            var listNode = e.currentTarget.nextElementSibling.nextElementSibling;
            this.active = true;
            if (listNode.classList.contains("active")) {
                listNode.classList.remove("active")
                listNode.style.height = 0 + "px";
            }
            else {
                var totalHeight = 0;
                [...listNode.children].forEach(x =>totalHeight+=x.offsetHeight)
                listNode.style.height = totalHeight + "px"
                listNode.classList.add("active")
            }
        }
    }
}
</script>