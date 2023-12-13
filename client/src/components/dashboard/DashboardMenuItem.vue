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
    <li v-if="!value.isHr" class="dashboard_menu_item">
        <button v-if="value.childItems.length>0" class="btn dashboard_submenu_collapser" @click="collapseSubmenu">
            <FontAwesomeIcon icon="angle-down" />
        </button>
        <RouterLink :to="value.link">
            <FontAwesomeIcon :icon="value.icon" />{{ value.text }}
        </RouterLink>
        <ul class="dashboard_submenu"  v-if="value.childItems.length>0">
            <DashboardMenuItem v-for="item in value.childItems" :value="item" />
        </ul>
    </li>
    <li v-else class="dashboard_menu_item">
        <hr>
    </li>
</template>
<style>
.dashboard_menu_item{
    display: flex;
    flex-direction: row-reverse;
    flex-wrap: wrap;
    justify-content: start;
}
.dashboard_menu_item>hr{
    width: 100%;
}
.dashboard_menu_item svg{
    margin-right: 5px;
    width: 30px;
    font-size: 17px;
}
.dashboard_menu_item a{
    padding: 10px 5px;
    font-size: 15px;
    display: block;
    color: #484848;
    transition: 0.3s;
    width: 100%;
}
.dashboard_menu_item a:where(:hover/*, .router-link-active*/) {
    color: var(--second-color);
}
.dashboard_submenu_collapser {
    width: 20%;
}

.dashboard_submenu_collapser+a {
    width: 80%;
}

.dashboard_submenu {
    list-style: none;
    padding-left: 18px;
    width: 100%;
    height: 0px;
    overflow: hidden;
    transition: 0.3s;
}

.dashboard_submenu.active {
    border-left: 1px solid color-mix(in srgb, var(--first-color) 30%, transparent);
    height: max-content;
    box-shadow: -5px 0 10px 2px rgba(72, 72, 72, .1);
    margin: 10px 0;
}


</style>
<script>
export default {
    methods: {
        collapseSubmenu(e) {
            var listNode = e.currentTarget.nextElementSibling.nextElementSibling;
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