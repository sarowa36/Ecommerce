<script setup>
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { dateToDateTimeString } from "@/helpers/";
import { useCitiesAndDistrictsStore } from "@/stores/CitiesAndDistrictsStore";
import { OrderValue, OrderStatusDescriber, OrderStatus } from ".";
import {OrderItemComponent,OrderItemStatus} from "@/components/orderItemComponent/index.ts";

defineProps({
    modelValue: {
        type: OrderValue,
        req: true
    }
})
</script>
<template>
    <div :class="{ 'order_outer': true, 'active': show }">
        <div class="order_mini" @click="toggleOrder()">
            <div class="order_mini_images">
                <img v-for="imagelink in value.orderItems.map(x => x.productImage)" v-bind:key="imagelink" :src="imagelink"
                    alt="">
            </div>
            <div class="order_mini_meta">
                <span>Order number: <strong class="text_theme">{{ value.id }}</strong></span>
                <span>{{ dateToDateTimeString(value.createDate) }}</span>
            </div>
            <div class="order_mini_status">
                <FontAwesomeIcon icon="check-circle" />
                <span>{{ OrderStatusDescriber(value.orderStatus) }}</span>
            </div>
            <div class="order_mini_amount text_theme">
                {{ value.paidPrice }} $
            </div>
            <div class="order_mini_toggle_icon">
                <FontAwesomeIcon :icon="show ? 'chevron-up' : 'chevron-down'" />
            </div>
        </div>
        <Transition name="nested">
            <div v-if="show" class="order_detail_outer">
                <div class="order_detail_inner">
                    <OrderItemComponent v-model="value.orderItems"></OrderItemComponent> 
                    <div class="order_action_buttons">
                        <slot name="actionButtons"></slot>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="order_delivery_status_box">
                                <FontAwesomeIcon icon="check-circle" class="text_theme" />
                                <div class="order_delivery_meta">
                                    <h6>{{ OrderStatusDescriber(value.orderStatus) }}</h6>
                                    <span class="text-success" v-if="value.orderStatus == OrderStatus.Delivered">Delivered
                                        on:
                                        <strong>{{
                                            value.createDate.toLocaleString()
                                        }}</strong></span>
                                </div>
                            </div>
                            <div class="order_delivery_detail_box mt-3">
                                <h5 class="text_theme">Delivery Details</h5>
                                <strong>{{ value.address.name }}</strong>
                                <span>{{ citiesAndDistricts.getCity(value.address.cityId).name }}, {{
                                    citiesAndDistricts.getDistrict(value.address.cityId, value.address.districtId).name }},
                                    {{ value.address.zip }}</span>
                                <span>{{ value.address.detail }}</span>
                                <span v-if="value.cargoCode">Cargo code: {{ value.cargoCode }}</span>
                                <strong>{{ value.targetName }} - {{ value.targetPhone }}</strong>
                            </div>
                        </div>
                        <div class="col-md-6 mt-4">
                            <h5 class="text_theme">Payment Information</h5>
                            <ul class="order_payment_detail_list">
                                <li><span>Cargo: </span><span>{{ value.cargoAmount }}</span></li>
                                <li><span>Amount: </span><span>$ {{ value.totalPrice }}</span></li>
                                <li><span>Discount: </span><span>$ {{ value.discount }}</span></li>
                                <li><span>Total Amount: </span><span>$ {{ value.paidPrice }}</span></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </Transition>
    </div>
</template>
<script>
export default {
    data() {
        return {
            show: false,
            citiesAndDistricts: useCitiesAndDistrictsStore(),
        }
    },
    methods: {
        toggleOrder() {
            this.show = !this.show;
        },
    },
    computed: {
        value: {
            get() {
                return this.modelValue;
            },
            set(val) {
                this.$emit("update:modelValue", val);
            }
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
}

.order_mini {
    display: flex;
    justify-content: space-between;
    padding: 15px;
    align-items: center;
    border-radius: 10px;
    font-size: 15px;
    flex-wrap: wrap;
    cursor: pointer;
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

.order_outer.active {
    box-shadow: 0 8px 32px rgba(72, 72, 72, .16);
    border-color: transparent;
}

.order_action_buttons {
    display: flex;
    flex-wrap: wrap;
    gap: 15px;
    align-items: center;
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

@media (max-width:992px) {
    .order_mini_status {
        width: 100%;
        order: 99;
        margin-top: 10px;
    }

    .order_action_buttons>* {
        width: 100%;
    }
}

@media (max-width:576px) {
    .order_mini_meta {
        display: none;
    }
}
</style>