<script setup>
import { RecursiveItemValue } from './RecursiveItemValue';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
const modelValue = defineModel({ type: RecursiveItemValue,required:true });
</script>
<template>

    <li class="with-alt-menu" v-if="modelValue.childrens!=null && modelValue.childrens.length>0">
        <RouterLink :to="modelValue.link">{{ modelValue.name }}</RouterLink>
        <button class="alt-menu-opener" @click="modelValue.showChildrens=!modelValue.showChildrens">
            <FontAwesomeIcon icon="chevron-down" :class="{'rotate-180':modelValue.showChildrens}" />
        </button>
        <ul v-if="modelValue.showChildrens">
            <RecursiveItem v-for="(item,index) in modelValue.childrens" :key="item.id" v-model="modelValue.childrens[index]"></RecursiveItem>
        </ul>
    </li>
    <li v-else><RouterLink :to="modelValue.link">{{ modelValue.name }}</RouterLink></li>
</template>
<style scoped>
.with-alt-menu {
    display: flex;
    flex-wrap: wrap;

}

.with-alt-menu>a:nth-child(1) {
    width: 90%;
}

.filter_category li a {
    display: block;
    color: #7f7f7f;
    line-height: 31px;
}

.with-alt-menu>a:nth-child(2) {
    width: 10%;
    text-align: center;
}

.with-alt-menu>ul {
    width: 100%;
    margin-left: 10px;
    list-style: none;
    padding: 0;
}
.alt-menu-opener>*{
    transition: 0.3s;
}
</style>