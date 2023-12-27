<script setup>
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import Carousel from "../components/Carousel.vue";
import { ProductComponentValue, ProductComponent } from '@/components/productComponent';
import { QuantityCounterComponent } from "@/components/quantityCounterComponent/";
import { useCartStore } from "@/stores/CartStore"
import { useLoginStore } from '@/stores/LoginStore';
import axios from 'axios';
</script>
<template>
    <div class="theme_bg_3 mb-5">
        <div class="container-lg pt-5 pb-5">
            <div class="row pt-5 pb-5">
                <div class="col-xl-8">
                    <h3 class="mb-4 text_theme">Your Cart</h3>
                    <div class="cart_products">
                        <div v-for="(item,key) in cartStore.items" :key="key" class="cart_product">
                            <img :src="item.productImage" alt="">
                            <h5 class="cart_product_title text_theme">{{ item.productName }}</h5>
                            <div class="cart_product_price">
                                <div><strong>Price</strong></div>
                                <div>{{ item.productPrice }}$</div>
                            </div>
                            <QuantityCounterComponent v-model="item.quantity" @increaseCartCount="increaseCartCount(item)" @decreaseCartCount="decreaseCartCount(item)" :loading="loading" />
                            <div class="cart_product_total">
                                <div><strong>Total</strong></div>
                                <div>{{ item.productPrice * item.quantity }}$</div>
                            </div>
                            <button class="btn btn-primary" @click="deleteCartItem(item)">
                                <FontAwesomeIcon icon="trash" />
                            </button>
                        </div>
                    </div>
                </div>
                <div class="col-xl-4 ps-4">
                    <div class="bg-white smooth_shadow cart_amount_section p-4">
                        <h4 class="text_theme">Summary</h4>
                        <ul class="cart_summary_list pt-2">
                            <li>
                                <div>SubTotal({{cartStore.items.length}} items)</div>
                                <div>{{cartStore.items.length >0 ? cartStore.items.map(x=>x.quantity*x.productPrice).reduce((a,b)=>a+b) :0}}$</div>
                            </li>
                            <li>
                                <div>Cargo</div>
                                <div>10$</div>
                            </li>
                            <li>
                                <div>Discount</div>
                                <div>-8$</div>
                            </li>
                            <li>
                                <div>Balance</div>
                                <div>{{cartStore.items.length >0 ? cartStore.items.map(x=>x.quantity*x.productPrice).reduce((a,b)=>a+b) :0}}$</div>
                            </li>
                        </ul>
                        <button class="btn btn-primary">Checkout</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="row">
            <h1 class="text-center text_theme">Best Sellers</h1>
            <div class="divider_line"></div>
            <div class="col-12 mt-5 mb-5">
                <Carousel :loop="true" :responsive="{ 0: { items: 1, nav: false }, 768: { items: 3 }, 992: { items: 4 } }">
                    <ProductComponent v-for="pr in products" :value="pr" class="m-3"></ProductComponent>
                </Carousel>
            </div>
        </div>
    </div>
</template>
<style>
.cart_product {
    display: grid;
    grid-template-columns: 150px 200px 110px 150px 80px 50px;
    width: 100%;
    align-items: start;
    justify-content: space-between;
    margin-bottom: 20px;
}

.cart_summary_list {
    padding: 0;
    list-style: none;
}

.cart_summary_list>li {
    display: flex;
    justify-content: space-between;
    padding: 8px;
}

.cart_summary_list>li:last-child {
    border-top: 1px solid;
    margin-top: 10px;
    padding-top: 15px;
}

.cart_amount_section>.btn {
    padding: 15px;
    width: 100%;
}

@media (max-width:768px) {
    .cart_product {
        grid-template-columns: 60px 110px 70px 130px 70px 50px;
    }

    .cart_product>h5 {
        font-size: 1rem;
    }
}

@media (max-width:576px) {
    .cart_product {
        grid-template-columns: 100px auto;
        justify-content: initial;
        grid-column-gap: 15px;
        grid-row-gap: 10px;
    }

    .cart_product>img {
        grid-row: 1/span 3;
    }

    .cart_product>*:not(img, button) {
        grid-column: 2;
    }

    .cart_product>button {
        grid-row: 4;
    }
}
</style>
<script>
export default {
    data() {
        return {
            productCartCount: 1,
            cartStore: useCartStore(),
            loginStore:useLoginStore(),
            loading:false,
            products: [
                new ProductComponentValue({ image: ["http://img.sarowa36.com.tr/woman1.png"], name: "Regular Fit Long Sleeve Top", price: "38.99", star: "5.0" }),
                new ProductComponentValue({ image: ["http://img.sarowa36.com.tr/woman2.png"], name: "Black Crop Tailored Jacket", price: "62.99", star: "4.3" }),
                new ProductComponentValue({ image: ["http://img.sarowa36.com.tr/woman3.png"], name: "Textured Sunset Shirt", price: "49.99", star: "5.0" })],
        }
    },
    methods: {
        increaseCartCount(item) {
            this.loading=true;
            var path="";
            if(this.loginStore.isLogged)
            path="User/ShoppingCart/Write";
            else
            path="Anonym/ShoppingCart/Write";
            axios.postForm(path,{productId:item.productId,quantity:item.quantity+1}).then(x=>{
                if(x.status==200)
                this.cartStore.loadCart();
                this.loading=false;
            })
            
        },
        decreaseCartCount(item) {
            this.loading=true;
            var path="";
            if(this.loginStore.isLogged)
            path="User/ShoppingCart/Write";
            else
            path="Anonym/ShoppingCart/Write";
            axios.postForm(path,{productId:item.productId,quantity:item.quantity-1}).then(x=>{
                if(x.status==200)
                this.cartStore.loadCart();
                this.loading=false;
            })
        },
        deleteCartItem(item){
            this.loading=true;
            var path="";
            if(this.loginStore.isLogged)
            path="User/ShoppingCart/Write";
            else
            path="Anonym/ShoppingCart/Write";
            axios.postForm(path,{productId:item.productId,quantity:0}).then(x=>{
                if(x.status==200)
                this.cartStore.loadCart();
                this.loading=false;
            })
        }
    }
}
</script>