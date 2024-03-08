<script setup>
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { ProductComponentValue, ProductComponent } from "@/components/productComponent"
import { FilterComponent, FilterTypeEnum, FilterValue, FilterValueArray } from '@/components/filterComponent';
import Slider from '@vueform/slider'
import axios from 'axios';
</script> 
<template>
    <div class="container mb-5">
        <div class="row">
            <div class="col-12 mt-5 mb-4 filter_list">
                <FilterComponent describe-text="Categories" icon="list" v-model="categoryFilterValues">
                </FilterComponent>
                <FilterComponent describe-text="Sizes" icon="up-right-and-down-left-from-center" v-model="sizeFilterValues">
                </FilterComponent>
                <FilterComponent describe-text="Price" icon="hand-holding-dollar" :filterType="FilterTypeEnum.priceRange"
                    v-model="priceFilterValues">
                </FilterComponent>
                <FilterComponent describe-text="Sort" icon="shuffle" v-model="sortFilterValues">
                </FilterComponent>
            </div>
            <ProductComponent v-for="pr in products" :value="pr" class="col-sm-6 col-md-4 col-lg-3 mb-5"></ProductComponent>
        </div>
    </div>
</template>
<script>
export default {
    data() {
        return {
            products: [ ],
            categoryFilterValues: new FilterValueArray(new FilterValue({ value: "XS" }),new FilterValue({ value: "M" }),new FilterValue({ value: "L" }),new FilterValue({ value: "XL" }),new FilterValue({ value: "XXL" }),new FilterValue({ value: "XL" }),new FilterValue({ value: "XXL" }),new FilterValue({ value: "XL" }),new FilterValue({ value: "XXL" }),),
            sizeFilterValues: new FilterValueArray(new FilterValue({ value: "XS" }),new FilterValue({ value: "M" }),new FilterValue({ value: "L" }),new FilterValue({ value: "XL" }),new FilterValue({ value: "XXL" }),new FilterValue({ value: "XL" }),new FilterValue({ value: "XXL" }),new FilterValue({ value: "XL" }),new FilterValue({ value: "XXL" }),),
            sortFilterValues: new FilterValueArray(new FilterValue({ value: "XS" }),new FilterValue({ value: "M" }),new FilterValue({ value: "L" }),new FilterValue({ value: "XL" }),new FilterValue({ value: "XXL" }),new FilterValue({ value: "XL" }),new FilterValue({ value: "XXL" }),new FilterValue({ value: "XL" }),new FilterValue({ value: "XXL" }),),
            priceFilterValues: new FilterValueArray(new FilterValue({ value: 0 }), new FilterValue({ value: 100 })),
        }
    },
    mounted() {
        this.getProductList();
    },
    methods: {
        showFilter(prop) {
            this[prop].show = !this[prop].show;
        },
        async getProductList(){
            var res= await axios.get("/Anonym/Product/GetList");
            if(res.isSuccess){
                res.data.forEach(element => {
                    this.products.push(new ProductComponentValue(element))
                });
            }
        }
    },
    computed: {
    }
}
</script>
<style src="@vueform/slider/themes/default.css"></style>
<style scoped>
.filter_list {
    display: flex;
    gap: 20px;
}

@media (max-width:768px) {
    .filter_list {
        flex-direction: column;
    }
}
</style>