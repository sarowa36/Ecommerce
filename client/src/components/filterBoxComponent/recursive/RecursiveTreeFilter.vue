<script setup>
import { FilterBoxLayout } from '../';
import { RecursiveItemValue } from './RecursiveItemValue';
import RecursiveItem from './RecursiveItem.vue';
const modelValue=defineModel({type:[],required:true})
const {title}=defineProps(["title"])
</script>
<template>
    <FilterBoxLayout>
        <div class="h5">{{ title }}</div>
        <div class="filter">
            <ul>
              <RecursiveItem v-for="(item,index) in modelValue" v-bind:key="item.id" v-model="modelValue[index]" ></RecursiveItem>
            </ul>
        </div>
    </FilterBoxLayout>
</template>
<style scoped>
.filter>ul{
    list-style-type: none;
    max-height: 150px;
    overflow-y: scroll;
    padding: 0 12px 0 0;
}
</style>