<script setup>
import { FilterBoxLayout } from '..';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import TextBox from '@/components/TextBox.vue';
import { ref,watch } from 'vue';
const props=defineProps({defaultValue:null})
const val=ref("");
if(props.defaultValue){
    val.value=props.defaultValue;
}
watch(()=>props.defaultValue,(newVal,oldVal)=>{
    val.value=newVal;
})
defineEmits(["onSearch"])
</script>
<template>
<FilterBoxLayout>
    <div class="h5">Search</div>
    <div class="search_filter">
        <TextBox class="search_box" placeholder="Search" v-model="val" />
        <button class="btn btn-primary" @click="$emit('onSearch',val)"><FontAwesomeIcon icon="magnifying-glass"></FontAwesomeIcon></button>
    </div>
</FilterBoxLayout>
</template>
<style scoped>
.search_filter{
    display: flex;
    justify-content: space-between;
    align-items: center;
    max-width: 100%;
}
.search_box{
    width: 75%;
}
.search_filter>button{
    width: 20%;
}
</style>