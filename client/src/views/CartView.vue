<script setup>
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import Carousel from "../components/Carousel.vue";
import {ProductComponentValue,ProductComponent} from '@/components/productComponent';

</script>
<template>
    <div class="theme_bg_3 mb-5">
        <div class="container-lg pt-5 pb-5">
            <div class="row pt-5 pb-5">
                <div class="col-xl-8">
                    <h3 class="mb-4 text_theme">Your Cart</h3>
                    <div class="cart_products">
                        <div class="cart_product">
                            <img src="http://img.sarowa36.com.tr/woman1.png" alt="">
                            <h5 class="cart_product_title text_theme">Piece Of Beauty</h5>
                            <div class="cart_product_price">
                                <div><strong>Price</strong></div>
                                <div>10$</div>
                            </div>
                            <div class="cart_counter">
                                <button class="btn btn-outline-dark" @click="decreaseCartCount">
                                    <FontAwesomeIcon icon="minus" />
                                </button>
                                <input type="text" class="form-control border-dark" :value="productCartCount" disabled>
                                <button class="btn btn-outline-dark" @click="increaseCartCount">
                                    <FontAwesomeIcon icon="plus" />
                                </button>
                            </div>
                            <div class="cart_product_total">
                                <div><strong>Total</strong></div>
                                <div>800$</div>
                            </div>
                            <button class="btn btn-primary">
                                <FontAwesomeIcon icon="trash" />
                            </button>
                        </div>
                        <div class="cart_product">
                            <img src="http://img.sarowa36.com.tr/woman1.png" alt="">
                            <h5 class="cart_product_title">Piece Of Beauty</h5>
                            <div class="cart_product_price">
                                <div><strong>Price</strong></div>
                                <div>10$</div>
                            </div>
                            <div class="cart_counter">
                                <button class="btn btn-outline-dark" @click="decreaseCartCount">
                                    <FontAwesomeIcon icon="minus" />
                                </button>
                                <input type="text" class="form-control border-dark" :value="productCartCount" disabled>
                                <button class="btn btn-outline-dark" @click="increaseCartCount">
                                    <FontAwesomeIcon icon="plus" />
                                </button>
                            </div>
                            <div class="cart_product_total">
                                <div><strong>Total</strong></div>
                                <div>800$</div>
                            </div>
                            <button class="btn btn-primary">
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
                                <div>SubTotal(1 items)</div>
                                <div>80$</div>
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
                                <div>82$</div>
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
            products: [
                new ProductComponentValue({ img: ["http://img.sarowa36.com.tr/woman1.png"], title: "Regular Fit Long Sleeve Top", price: "38.99", star: "5.0" }),
                new ProductComponentValue({ img: ["http://img.sarowa36.com.tr/woman2.png"], title: "Black Crop Tailored Jacket", price: "62.99", star: "4.3" }),
                new ProductComponentValue({ img: ["http://img.sarowa36.com.tr/woman3.png"], title: "Textured Sunset Shirt", price: "49.99", star: "5.0" })],
        }
    },
    methods: {
        increaseCartCount() {
            this.productCartCount++;
        },
        decreaseCartCount() {
            if (this.productCartCount > 1)
                this.productCartCount--;
        }
    }
}
</script>