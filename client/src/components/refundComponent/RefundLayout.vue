<script setup>
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { dateToDateTimeString } from "@/helpers/";
import { useCitiesAndDistrictsStore } from "@/stores/CitiesAndDistrictsStore";
import { OrderRefundValue, OrderRefundStatusDescriber, OrderRefundStatus } from ".";
import { OrderItemComponent } from "../orderItemComponent/";
</script>
<template>
    <div :class="{ 'order_outer': true, 'active': show }">
        <div class="order_mini" @click="toggleOrder()">
            <div class="order_mini_images">
                <img v-for="imagelink in value.refundItems.map(x => x.productImage)" v-bind:key="imagelink" :src="imagelink"
                    alt="">
            </div>
            <div class="order_mini_meta">
                <span>Refund number: <strong class="text_theme">{{ value.id }}</strong></span>
                <span>{{ dateToDateTimeString(value.createDate) }}</span>
            </div>
            <div class="order_mini_status">
                <FontAwesomeIcon icon="check-circle" />
                <span>{{ OrderRefundStatusDescriber(value.orderRefundStatus) }}</span>
            </div>
            <div class="order_mini_amount text_theme">
                {{ value.totalRefundAmount }} $
            </div>
            <div class="order_mini_toggle_icon">
                <FontAwesomeIcon :icon="show ? 'chevron-up' : 'chevron-down'" />
            </div>
        </div>
        <Transition name="nested">
            <div v-if="show" class="order_detail_outer">
                <div class="order_detail_inner">
                    <OrderItemComponent v-model="value.refundItems"></OrderItemComponent>
                    <div class="order_action_buttons">
                        <slot name="actionButtons"></slot>
                    </div>
                    <div class="row">
                        <div class="col-12 mt-4">
                            <div>
                                Status: {{ OrderRefundStatusDescriber(value.orderRefundStatus) }}
                            </div>
                            <div class="text-alert" v-if="value.cargoCode">
                                Please ship the product with this shipping code: {{ value.cargoCode }}
                            </div>
                            <div>
                                Refund Amount: {{ value.totalRefundAmount }} $
                            </div>
                            <div v-if="value.userMessage">
                                Your Message: {{ value.userMessage }}
                            </div>
                            <div class="text-danger" v-if="value.sellerResponse">
                                Seller Response: {{ value.sellerResponse }}
                            </div>
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
    },
    props: {
        modelValue: {
            type: OrderRefundValue,
            req: true
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
    align-items: center;
}

.order_action_buttons>.btn>svg {
    margin-right: 10px;
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