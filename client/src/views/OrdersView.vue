<script setup>
import DashboardLayout from "@/components/DashboardLayout.vue"
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import  OrderModel  from "../models/OrderModel";
import OrderStatus from "../enums/OrderStatus";
import { ProductModel } from "../models/ProductModel";
import {createRandomNumber,dateToDateTimeString} from "@/helpers/";
import FilterComponent from "../components/FilterComponent.vue";
import FilterValueArray from "../models/FilterValueArray";
import FilterValue from "../models/FilterValue";
import FilterType from "../enums/FilterType";
</script>
<template>
    <DashboardLayout>
        <div class="row pt-5">
            <div class="col-12 mb-3">
                <FilterComponent icon="calendar-days" describeText="Date" v-model="dateFilterValues" :filterType="FilterType.checkboxListOnlyOneSelectable" :showSearchInput="false" />
            </div>
            <div v-for="(item,index) in orders" :class="{ 'order_outer': true, 'active': currentOrderId==item.id }">
                <div class="order_mini" @click="toggleOrder(item.id)">
                    <div class="order_mini_images">
                        <img v-for="imagelink in item.products.map(x=>x.img[0])" :src="imagelink" alt="">
                    </div>
                    <div class="order_mini_meta">
                        <span>Order number: <strong>{{ item.id }}</strong></span>
                        <span>{{ dateToDateTimeString(item.date) }}</span>
                    </div>
                    <div class="order_mini_status">
                        <FontAwesomeIcon icon="check-circle" />
                        <span>{{ item.orderStatus }}</span>
                    </div>
                    <div class="order_mini_amount">
                        {{ item.totalAmount }} $
                    </div>
                    <div class="order_mini_toggle_icon">
                        <FontAwesomeIcon :icon="currentOrderId==item.id ? 'chevron-up':'chevron-down' " />
                    </div>
                </div>
                <Transition name="nested">
                    <div v-if="currentOrderId==item.id " class="order_detail_outer">
                        <div class="order_detail_inner">
                            <div class="order_products">
                                <div v-for="order_product in item.products" class="order_product">
                                    <div class="order_product_card_image">
                                        <a class="order_product_image_outer" href="#"><img :src="order_product.img[0]" alt=""></a>
                                    </div>
                                    <div class="order_product_card_content">
                                        <a href="#">
                                            <h6>{{ order_product.title }}</h6>
                                        </a>
                                        <div class="text-success">{{order_product.price}} $</div>
                                    </div>
                                </div>
                            </div>
                            <div class="order_action_buttons">
                                <a href="#" class="btn btn-primary">
                                    <FontAwesomeIcon icon="people-carry-box" />Cargo Tracking
                                </a>
                                <a href="#" class="btn btn-primary">
                                    <FontAwesomeIcon icon="ban" />Complaint Or Refund
                                </a>
                                <a href="#" class="btn btn-primary">
                                    <FontAwesomeIcon icon="file-invoice" />Show Bill
                                </a>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="order_delivery_status_box">
                                        <FontAwesomeIcon icon="check-circle" />
                                        <div class="order_delivery_meta">
                                            <h6>{{item.orderStatus}}</h6>
                                            <span class="text-success">Delivered on: <strong>{{ item.date.toLocaleString() }}</strong></span>
                                        </div>
                                    </div>
                                    <div class="order_delivery_detail_box mt-3">
                                        <h5>Delivery Details</h5>
                                        <strong>{{item.addressName}}</strong>
                                        <span>{{ item.addressDetail }}</span>
                                        <span>{{ item.addressCity }}, {{ item.addressZip }} {{ item.addressCountry }}</span>
                                        <strong>{{ item.targetPerson }} - {{item.targetPhone}}</strong>
                                    </div>
                                </div>
                                <div class="col-md-6 mt-4">
                                    <h5>Payment Information</h5>
                                    <ul class="order_payment_detail_list">
                                        <li><span>Cargo: </span><span>{{ item.cargoAmount }}</span></li>
                                        <li><span>Amount: </span><span>$ {{ item.amount }}</span></li>
                                        <li><span>Discount: </span><span>$ {{ item.discount }}</span></li>
                                        <li><span>Total Amount: </span><span>$ {{ item.totalAmount }}</span></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </Transition>
            </div>
        </div>
    </DashboardLayout>
</template>
<script>
export default {
    data() {
        return {
            active: false,
            currentOrderId:null,
            orders: [
                new OrderModel({
                    id: createRandomNumber(),
                    orderStatus: OrderStatus.deliverySuccess,
                    date: new Date(),
                    products: [new ProductModel({ img: ["http://img.sarowa36.com.tr/woman1.png"], title: "Regular Fit Long Sleeve Top", price: "38.99", star: "5.0" }),
                    new ProductModel({ img: ["http://img.sarowa36.com.tr/woman2.png"], title: "Black Crop Tailored Jacket", price: "62.99", star: "4.3" }),
                    new ProductModel({ img: ["http://img.sarowa36.com.tr/woman3.png"], title: "Textured Sunset Shirt", price: "49.99", star: "5.0" })],
                    addressName: "Home",
                    addressDetail: "5632 Grove Street Apartment #20",
                    addressCity: "New York",
                    addressZip: "10014",
                    addressCountry: "USA",
                    targetPerson: "Cemal Mustafaoğlu",
                    targetPhone: "905*******58",
                    cargoAmount: 0,
                    amount: 539.11,
                    discount: 50,
                    totalAmount: 489.11
                }),
                new OrderModel({
                    id: createRandomNumber(),
                    orderStatus: OrderStatus.deliverySuccess,
                    date: new Date(),
                    products: [new ProductModel({ img: ["http://img.sarowa36.com.tr/woman1.png"], title: "Regular Fit Long Sleeve Top", price: "38.99", star: "5.0" }),
                    new ProductModel({ img: ["http://img.sarowa36.com.tr/woman2.png"], title: "Black Crop Tailored Jacket", price: "62.99", star: "4.3" }),
                    new ProductModel({ img: ["http://img.sarowa36.com.tr/woman3.png"], title: "Textured Sunset Shirt", price: "49.99", star: "5.0" })],
                    addressName: "Home",
                    addressDetail: "5632 Grove Street Apartment #20",
                    addressCity: "New York",
                    addressZip: "10014",
                    addressCountry: "USA",
                    targetPerson: "Cemal Mustafaoğlu",
                    targetPhone: "905*******58",
                    cargoAmount: 0,
                    amount: 539.11,
                    discount: 50,
                    totalAmount: 489.11
                })],
            dateFilterValues:new FilterValueArray(
                new FilterValue({text:"Last 30 Day", value:"30"}),
                new FilterValue({text:"Last 3 Months",value:"90"}),
                new FilterValue({text:"Last 6 Months", value:"180"}),
                new FilterValue({text:"Last 1 Year", value:"365"}),
                new FilterValue({text:"All", value:"-1"}),
                )
        }
    },
    methods: {
        toggleOrder(orderId) {
            this.currentOrderId= this.currentOrderId==orderId ? 0: orderId;
        }
    }
}
</script>
<style scoped>
.order_outer {
    border: 1px solid color-mix(in srgb, var(--first-color) 25%, transparent);
    width: 99%;
    margin: 0 auto 20px;
    transition: 0.3s;
    cursor: pointer;
}

.order_mini {
    display: flex;
    justify-content: space-between;
    padding: 15px;
    align-items: center;
    border-radius: 10px;
    font-size: 15px;
    flex-wrap: wrap;
}

.order_mini_images {
    display: flex;
}

.order_mini_images>img {
    aspect-ratio: 1;
    width: 50px;
    object-fit: cover;
    border: 3px solid #f5f5f5;
    border-radius: 50%;
}

.order_mini_images>img:not(:first-child) {
    margin-left: -20px;
}

.order_mini_meta {
    display: flex;
    flex-direction: column;
}

.order_mini_meta>span {
    color: black;
}

.order_mini_status>svg {
    margin-right: 15px;
}

.order_mini_toggle_icon>svg {
    background-color: #f5f5f5;
    padding: 10px;
    border-radius: 50%;
}

.order_detail_inner {
    padding-bottom: 20px;
}

.order_detail_inner>* {
    margin: 20px;
}

.order_product_card_content {
    display: flex;
    flex-direction: column;
    justify-content: center;
}

.order_product {
    display: flex;
    width: 100%;
    align-items: center;
    padding: 15px 30px;
    gap: 15px;
}

.order_product_card_image {
    width: 80px;
}

.order_products {
    border: 1px solid color-mix(in srgb, var(--first-color) 25%, transparent);
}

.order_outer.active {
    box-shadow: 0 8px 32px rgba(72, 72, 72, .16);
    border-color: transparent;
}

.order_action_buttons {
    display: flex;
    flex-wrap: wrap;
    gap: 15px;
}

.order_action_buttons>a>svg {
    margin-right: 10px;
}

.order_delivery_status_box {
    display: flex;
    align-items: center;
    padding: 10px;
    background-color: color-mix(in srgb, var(--fifth-color) 45%, transparent);
    font-size: 14px;
}

.order_delivery_status_box>svg {
    font-size: 30px;
    margin-right: 10px;
}

.order_delivery_detail_box {
    display: flex;
    flex-direction: column;
    gap: 8px;
}

.order_payment_detail_list {
    list-style: none;
    padding-left: 5px;
}

.order_payment_detail_list>li {
    display: flex;
    justify-content: space-between;
    padding: 8px;
}

.order_payment_detail_list>li>span:first-child {
    opacity: 80%;
}

.order_payment_detail_list>li:last-child {
    border-top: 1px solid color-mix(in srgb, var(--first-color) 50%, transparent);
}
</style>
<style  scoped>
@media (max-width:992px) {
.order_mini_status{
width: 100%;
order: 99;
margin-top: 10px;
}  
.order_action_buttons>*{
    width: 100%;
}
}
@media (max-width:576px) {
    .order_mini_meta{
    display: none;
}    
}
</style>