<script setup>
import { FilterBoxLayout } from '..';
import TextBox from '@/components/TextBox.vue';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { ref,watch } from 'vue';
const props=defineProps({defaultMin:Number,defaultMax:Number})
const minVal=ref(NaN);
const maxVal=ref(NaN);
if(props.defaultMin){
    minVal.value=props.defaultMin
}
if(props.defaultMax){
    maxVal.value=props.defaultMax
}
watch(()=>props.defaultMin,(newVal,oldVal)=>{
    minVal.value=newVal;
})
watch(()=>props.defaultMax,(newVal,oldVal)=>{
    maxVal.value=newVal;
})
defineEmits(["onSearch"])

</script>
<template>
<FilterBoxLayout>
    <div class="price_filter">
    <TextBox class="price_filter_field font-sm" placeholder="Min. price" type="number" v-model="minVal" />
    <TextBox class="price_filter_field font-sm" placeholder="Max. price" type="number" v-model="maxVal" />
    <button class="btn btn-primary" @click="$emit('onSearch',minVal,maxVal)"><FontAwesomeIcon icon="magnifying-glass"></FontAwesomeIcon></button>
</div>
</FilterBoxLayout>
</template>
<style scoped>
.price_filter{
    display: flex;
    align-items: center;
    justify-content: center;
}
.price_filter>*:not(:last-child){
    padding-right: 10px;
}
.price_filter_field{
width: 40%;
} 
.price_filter>.btn{
width: 20%;
}
</style>