<script setup>
import { OrderItemStatus, OrderItemStatusDescriber,OrderItemArray } from '.';
import { router_names } from '@/router';
const modelValue=defineModel({type:OrderItemArray});
</script>
<template>
    <div class="order_products">
         <div v-for="order_product in modelValue" v-bind:key="order_product.id" class="order_product">
            <div class="order_product_card_image">
                <RouterLink class="order_product_image_outer" :to="{ name: router_names.product, params: { id: order_product.productId } }">
                    <img :src="order_product.productImage" alt="">
                </RouterLink>
            </div>
            <div class="order_product_card_content">
                <RouterLink class="order_product_image_outer"
                    :to="{ name: router_names.product, params: { id: order_product.productId } }">
                    <h6>{{ order_product.productName }}</h6>
                </RouterLink>
                <div class="text-success">{{ order_product.price }}$ x {{ order_product.quantity }} piece </div>
                <div class="order_variation" v-for="varia in order_product.variation" :key="varia.id">{{ varia.name }}: {{ varia.value }}</div>
                <div class="text-danger" v-if="order_product.orderItemStatus != OrderItemStatus.NotOnRefundProccess">{{
                    OrderItemStatusDescriber(order_product.orderItemStatus) }}</div>
            </div>
        </div>  
    </div>
</template>
<style>
.order_variation{
    font-size: 14px;
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
</style>