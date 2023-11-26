<script setup>
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import ProductComponent from "../components/ProductComponent.vue"
import { ProductModel } from '../models/ProductModel';
import FilterComponent from '../components/FilterComponent.vue';
import FilterValue from "../models/FilterValue";
import FilterValueArray from "../models/FilterValueArray";
import Slider from '@vueform/slider'
</script> 
<template>
    <div class="container mb-5">
        <div class="row">
            <div class="col-12 mt-5 mb-4 filter_list">
                <FilterComponent describe-text="hello" :show="true" v-model="categoryFilterValues">
                </FilterComponent>
                <div class="filter">
                    <button :class="'btn' + (categoryFilter.active ? ' active' : '')" @click="showFilter('categoryFilter')">
                        <FontAwesomeIcon icon="list"></FontAwesomeIcon>
                        <span> {{ categoryFilter.describeText }}</span>
                    </button>
                    <div class="filter_dropdown smooth_shadow" v-if="categoryFilter.show">
                        <div class="filter_dropdown_body">
                            <h5 class="filter_title">Categories</h5>
                            <input type="text" placeholder="Search" class="form-control mb-2">
                            <ul>
                                <li><input type="checkbox"> XS</li>
                                <li><input type="checkbox"> M</li>
                                <li><input type="checkbox"> L</li>
                                <li><input type="checkbox"> XL</li>
                                <li><input type="checkbox"> XXL</li>
                                <li><input type="checkbox"> XL</li>
                                <li><input type="checkbox"> XXL</li>
                                <li><input type="checkbox"> XL</li>
                                <li><input type="checkbox"> XXL</li>
                            </ul>
                        </div>
                        <div class="filter_dropdown_footer">
                            <button class="btn btn-secondary filter_dropdown_clear">Clear</button>
                            <button class="btn btn-primary filter_dropdown_apply">Apply</button>
                        </div>
                    </div>
                </div>
                <div class="filter">
                    <button :class="'btn' + (sizeFilter.active ? ' active' : '')" @click="showFilter('sizeFilter')">
                        <FontAwesomeIcon icon="up-right-and-down-left-from-center"></FontAwesomeIcon>
                        <span> {{ sizeFilter.describeText }}</span>
                    </button>
                    <div class="filter_dropdown smooth_shadow" v-if="sizeFilter.show">
                        <div class="filter_dropdown_body">
                            <h5 class="filter_title">Categories</h5>
                            <input type="text" placeholder="Search" class="form-control mb-2">
                            <ul>
                                <li><input type="checkbox"> XS</li>
                                <li><input type="checkbox"> M</li>
                                <li><input type="checkbox"> L</li>
                                <li><input type="checkbox"> XL</li>
                                <li><input type="checkbox"> XXL</li>
                                <li><input type="checkbox"> XL</li>
                                <li><input type="checkbox"> XXL</li>
                                <li><input type="checkbox"> XL</li>
                                <li><input type="checkbox"> XXL</li>
                            </ul>
                        </div>
                        <div class="filter_dropdown_footer">
                            <button class="btn btn-secondary filter_dropdown_clear">Clear</button>
                            <button class="btn btn-primary filter_dropdown_apply">Apply</button>
                        </div>
                    </div>
                </div>
                <div class="filter">
                    <button :class="'btn' + (priceFilter.active ? ' active' : '')" @click="showFilter('priceFilter')">
                        <FontAwesomeIcon icon="hand-holding-dollar"></FontAwesomeIcon>
                        <span> {{ priceFilter.describeText }}</span>
                    </button>
                    <div class="filter_dropdown smooth_shadow" v-if="priceFilter.show">
                        <div class="filter_dropdown_body">
                            <h5 class="filter_title">Price Range</h5>
                            <Slider class="mt-5 me-2 ms-2" v-model="priceFilter.value" />
                        </div>
                        <div class="filter_dropdown_footer">
                            <button class="btn btn-secondary filter_dropdown_clear">Clear</button>
                            <button class="btn btn-primary filter_dropdown_apply">Apply</button>
                        </div>
                    </div>
                </div>
                <div class="filter">
                    <button :class="'btn' + (sortFilter.active ? ' active' : '')" @click="showFilter('sortFilter')">
                        <FontAwesomeIcon icon="shuffle"></FontAwesomeIcon>
                        <span> {{ sortFilter.describeText }}</span>
                    </button>
                    <div class="filter_dropdown smooth_shadow" v-if="sortFilter.show">
                        <div class="filter_dropdown_body">
                            <h5 class="filter_title">Categories</h5>
                            <input type="text" placeholder="Search" class="form-control mb-2">
                            <ul>
                                <li><input type="checkbox"> XS</li>
                                <li><input type="checkbox"> M</li>
                                <li><input type="checkbox"> L</li>
                                <li><input type="checkbox"> XL</li>
                                <li><input type="checkbox"> XXL</li>
                                <li><input type="checkbox"> XL</li>
                                <li><input type="checkbox"> XXL</li>
                                <li><input type="checkbox"> XL</li>
                                <li><input type="checkbox"> XXL</li>
                            </ul>
                        </div>
                        <div class="filter_dropdown_footer">
                            <button class="btn btn-secondary filter_dropdown_clear">Clear</button>
                            <button class="btn btn-primary filter_dropdown_apply">Apply</button>
                        </div>
                    </div>
                </div>
            </div>
            <ProductComponent v-for="pr in products" :value="pr" class="col-sm-6 col-md-4 col-lg-3 mb-5"></ProductComponent>
        </div>
    </div>
</template>
<script>
export default {
    data() {
        return {
            products: [
                new ProductModel({ img: ["http://img.sarowa36.com.tr/woman1.png"], title: "Regular Fit Long Sleeve Top", price: "38.99", star: "5.0" }),
                new ProductModel({ img: ["http://img.sarowa36.com.tr/woman2.png"], title: "Black Crop Tailored Jacket", price: "62.99", star: "4.3" }),
                new ProductModel({ img: ["http://img.sarowa36.com.tr/woman3.png"], title: "Textured Sunset Shirt", price: "49.99", star: "5.0" }),
                new ProductModel({ img: ["http://img.sarowa36.com.tr/woman3.png"], title: "Textured Sunset Shirt", price: "49.99", star: "5.0" })
            ],
            categoryFilterValues:new FilterValueArray(new FilterValue({value:"XS"}),new FilterValue({value:"XL"})),
            categoryFilter: {
                active: false,
                value: [],
                show: false,
                describeText: "Categories"
            },
            sizeFilter: {
                active: false,
                value: [],
                show: false,
                describeText: "Size"
            },
            priceFilter: {
                active: false,
                value: [0, 100],
                show: false,
                describeText: "Price"
            },
            sortFilter: {
                active: false,
                value: [],
                show: false,
                describeText: "Sort"
            },

        }
    },
    mounted() {
        console.log(this.categoryFilterValues.GetSelectedValues())
    },
    methods: {
        showFilter(prop) {
            this[prop].show = !this[prop].show;
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

.filter>button {
    display: flex;
    gap: 13px;
    align-items: center;
    color: var(--first-color);
    border: 1px solid var(--second-color);
}

.filter>button:where(:hover, .active) {
    background-color: var(--sixth-color);
}

.filter_dropdown {
    border-radius: 6px;
    width: 370px;
    margin-top: 15px;
    position: absolute;
    background-color: white;
}

.filter_dropdown_body {
    padding: 20px 22px;
}

.filter_dropdown_body ul {
    list-style-type: none;
    padding: 0;
    margin: 0;
    max-height: 155px;
    overflow: scroll;
}

.filter_dropdown_footer {
    background: #EAEAEA;
    display: flex;
    padding: 20px 22px;
    justify-content: space-between;
}

@media (max-width:768px) {
    .filter_list {
        flex-direction: column;
    }

    .filter>.btn {
        width: 100%;
    }

    .filter_dropdown {
        width: 516px;
    }
}

@media (max-width:576px) {
    .filter_dropdown {
        width: calc(100vw - 24px);
    }
}</style>