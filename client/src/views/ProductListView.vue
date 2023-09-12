<script setup lang="ts">
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { Filter, arraystringConverter } from "../models/Filter";
import  ProductComponent from "../components/ProductComponent.vue"
import { ProductModel } from '../models/ProductModel';
</script> 
<template>
    <div class="container">
        <div class="row">
            <div class="col-12 mt-5 mb-4 filter_list">
                <button :class="'btn'+(priceFilter.isActive() ? ' active':'') " @click="deneme">
                    <FontAwesomeIcon :icon="priceFilter.icon"></FontAwesomeIcon>
                    <span> {{ priceFilter.valueAsString() }}</span>
                    <FontAwesomeIcon :icon="priceFilter.isActive() ? 'xmark-circle':'chevron-down'"></FontAwesomeIcon>
                </button>
                 <button v-for="i in filterList" :class="'btn'+ (i.isActive() ? ' active':'')" >
                    <FontAwesomeIcon :icon="i.icon"></FontAwesomeIcon>
                    <span> {{ i.valueAsString() }}</span>
                    <FontAwesomeIcon :icon="i.isActive() ? 'xmark-circle':'chevron-down'"></FontAwesomeIcon>
                </button>
            </div>
            <ProductComponent v-for="pr in products" :model="new ProductModel(pr)" class="col-3"></ProductComponent>
        </div>
    </div>
</template>
<script lang="ts">
export default {
    data() {
        return {
            products:[
            { img: "http://img.sarowa36.com.tr/woman1.png", title: "Regular Fit Long Sleeve Top", price: "38.99", star: "5.0" },
                { img: "http://img.sarowa36.com.tr/woman2.png", title: "Black Crop Tailored Jacket", price: "62.99", star: "4.3" },
                { img: "http://img.sarowa36.com.tr/woman3.png", title: "Textured Sunset Shirt", price: "49.99", star: "5.0" },
                { img: "http://img.sarowa36.com.tr/woman3.png", title: "Textured Sunset Shirt", price: "49.99", star: "5.0" }
            ],
            filterList: [
            new Filter<string, Array<string>>("Categories","list", arraystringConverter, (val) => val != null && val.length > 0),
            new Filter<string, Array<string>>("Size","up-right-and-down-left-from-center", arraystringConverter, (val) => val != null && val.length > 0),
            new Filter<string, string>("Sort" ,"shuffle",
                (val) => val.value != null && val.value.length > 0 ? val.value : val.filterName,
                (val) => val != null && val.length > 0)],
            priceFilter: new Filter<number, { minPrice?: number, maxPrice?: number }>("Price","hand-holding-dollar", (param) => {
                if (param.value) {
                    let val = param.value;
                    if (val.minPrice && val.maxPrice)
                        return val.minPrice.toString() + "$ - " + val.maxPrice.toString() + "$"
                    else if (val.minPrice)
                        return val.minPrice.toString() + "$+"
                    else if (val.maxPrice)
                        return val.maxPrice.toString() + "$-"
                }
                return param.filterName
            },(val) => val && (val.maxPrice != null && val.maxPrice > 0 || val.minPrice != null && val.minPrice > 0))
        }
    },
    mounted() {
    },
    methods: {
        deneme() {
             this.priceFilter.value={minPrice:100}
              console.log(this.priceFilter)
              console.log(this.priceFilter.isActive())
        }
    }
}
</script>
<style scoped>
.filter_list{
    display: flex;
    gap: 20px;
}
.filter_list>button {
    display: flex;
    gap: 13px;
    align-items: center;
    color: var(--first-color);
    border: 1px solid var(--second-color);

}

.filter_list>button:where(:hover, .active) {
    background-color: var(--sixth-color);
}
</style>