<script setup>
import { VCheckbox } from 'vuetify/components';
import FilterBoxLayout from '../FilterBoxLayout.vue';
import { SelectListValue } from '.';
import { ref,watch } from 'vue';

const props=defineProps({"title":String,"items":[SelectListValue],multiple:Boolean,defaultValue:null})
defineEmits(["onSearch"])
const val= props.multiple? ref([]):ref(NaN)
if(props.defaultValue || typeof(props.defaultValue)=="number"){
    val.value=props.defaultValue;
}
watch(()=>props.defaultValue,(newVal,oldVal)=>{
    val.value=newVal;
})
</script>
<template>
    <FilterBoxLayout>
        <span class="h5">{{ title }}</span>
<v-checkbox v-for="item in items" :key="item.value"
      v-model="val"
      :label="item.name"
      :value="item.value" hide-details
    ></v-checkbox>
    <div class="d-flex w-100">
    <button class="btn btn-primary m-2 w-100 " @click="$emit('onSearch',val)">Apply</button>
</div>
</FilterBoxLayout>
</template>