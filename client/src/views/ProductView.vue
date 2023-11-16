<script setup>
import Carousel from "../components/Carousel.vue";
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { Tabs, Tab } from 'vue3-tabs-component';
import {ProductModel} from "../models/ProductModel";
import ProductComponent from "../components/ProductComponent.vue";
</script> 
<template>
    <div class="container">
        <div class="row justify-content-center theme_bg_3 mt-5 pt-4 pb-4">
            <div class="col-5">
                <Carousel :items="1" :nav="false">
                    <img src="http://img.sarowa36.com.tr/woman1.png" alt="">
                    <img src="http://img.sarowa36.com.tr/woman2.png" alt="">
                    <img src="http://img.sarowa36.com.tr/woman3.png" alt="">
                </Carousel>
            </div>
            <div class="col-6 product_description">
                <h3>Piece of beauty</h3>
                <h4>1800,00 TL</h4>
                <div class="product_reviews_section">
                    <div class="d-flex align-items-center">
                        <span class="h4 m-0 me-2">3.6</span>
                        <div class="product_reviews_stars">
                            <img src="/src/assets/rating_disabled_star.svg" alt="">
                            <img src="/src/assets/rating_star.svg" class="active" alt="">
                        </div>
                    </div>
                    <div class="product_reviews_count">
                        16 Review
                    </div>
                </div>
                <div class="product_cart_section">
                    <div class="product_cart_counter">
                        <button class="btn btn-outline-dark" @click="decreaseCartCount">
                            <FontAwesomeIcon icon="minus" />
                        </button>
                        <input type="text" class="form-control" :value="productCartCount" disabled>
                        <button class="btn btn-outline-dark" @click="increaseCartCount">
                            <FontAwesomeIcon icon="plus" />
                        </button>
                    </div>
                    <button class="btn btn-secondary">
                        <FontAwesomeIcon icon="cart-shopping" /> Add Cart
                    </button>
                </div>
                <div class="product_boxs_section">
                    <div>
                        <FontAwesomeIcon icon="truck" /> Fast Delivery
                    </div>
                    <div>
                        <FontAwesomeIcon icon="shield-halved" /> 14 Day Return Guarantee
                    </div>
                    <div>
                        <FontAwesomeIcon icon="credit-card" /> Security Payment
                    </div>
                </div>
                <hr>
                <div class="product_buttons_section">
                    <button class="btn">
                        <FontAwesomeIcon :icon="['far', 'heart']" /> Like
                    </button>
                    <button class="btn">
                        <FontAwesomeIcon :icon="['far', 'bookmark']" /> Save
                    </button>
                    <button class="btn">
                        <FontAwesomeIcon icon="share" /> Share
                    </button>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <Tabs>
                    <Tab name="Details">
                        <h3>Lorem ipsum</h3>
                        Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the
                        industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type
                        and scrambled it to make a type specimen book. It has survived not only five centuries, but also the
                        leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s
                        with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop
                        publishing software like Aldus PageMaker including versions of Lorem Ipsum.
                        <ul>
                            <li>Lorem</li>
                            <li>Lorem</li>
                            <li>Lorem</li>
                            <li>Lorem</li>
                            <li>Lorem</li>
                            <li>Lorem</li>
                        </ul>
                        <ul class="odd_even_list">
                            <li><span>Lorem</span><span>ipsum</span></li>
                            <li><span>Lorem</span><span>ipsum</span></li>
                            <li><span>Lorem</span><span>ipsum</span></li>
                            <li><span>Lorem</span><span>ipsum</span></li>
                            <li><span>Lorem</span><span>ipsum</span></li>
                            <li><span>Lorem</span><span>ipsum</span></li>
                        </ul>
                    </Tab>
                    <Tab name="Comments">
                        <div class="comment_outer">
                            <div class="comment_picture">
                                <img src="@/assets/e.webp" alt="">
                            </div>
                            <div class="comment_inner">
                                <div class="comment_meta_outer">
                                    <div class="product_reviews_stars">
                                        <img src="/src/assets/rating_disabled_star.svg" alt="">
                                        <img src="/src/assets/rating_star.svg" class="active" alt="">
                                    </div>
                                    <div class="comment_meta_inner">
                                        12 October, Saturday 2023 | D***** Z*****
                                    </div>
                                </div>
                                <div class="comment_text theme_bg_3">
                                    Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum. 
                                </div>
                                <div class="comment_like_dislike">
                                    <span>Is this comment useful?</span>
                                    <button class="btn"><FontAwesomeIcon :icon="['far','thumbs-up']" /> 3</button>
                                    <button class="btn"><FontAwesomeIcon :icon="['far','thumbs-down']" /> 2</button>
                                </div>
                            </div>
                        </div>
                        <div class="comment_outer">
                            <div class="comment_picture">
                                <img src="@/assets/e.webp" alt="">
                            </div>
                            <div class="comment_inner">
                                <div class="comment_meta_outer">
                                    <div class="product_reviews_stars">
                                        <img src="/src/assets/rating_disabled_star.svg" alt="">
                                        <img src="/src/assets/rating_star.svg" class="active" alt="">
                                    </div>
                                    <div class="comment_meta_inner">
                                        12 October, Saturday 2023 | D***** Z*****
                                    </div>
                                </div>
                                <div class="comment_text theme_bg_3">
                                    Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum. 
                                </div>
                                <div class="comment_like_dislike">
                                    <span>Is this comment useful?</span>
                                    <button class="btn"><FontAwesomeIcon :icon="['far','thumbs-up']" /> 3</button>
                                    <button class="btn"><FontAwesomeIcon :icon="['far','thumbs-down']" /> 2</button>
                                </div>
                            </div>
                        </div>
                    </Tab>
                    <Tab name="Questions">

                    </Tab>
                </Tabs>
            </div>
            <div class="col-12">
                <Carousel :loop="true">
                    <ProductComponent v-for="pr in products" :value="pr" class="m-3"></ProductComponent>
                </Carousel>
            </div>
        </div>
    </div>
</template>
<style scoped>
.product_cart_counter {
    display: flex;
    margin-right: 5px;
}

.product_cart_counter>button {
    aspect-ratio: 1;
}

.product_cart_counter>input {
    width: 46px;
    text-align: center;
    outline-color: unset;
}

.product_cart_section {
    display: flex;
}

.product_reviews_section {
    display: flex;
    flex-direction: column;
    align-items: end;
}

.product_reviews_stars {
    position: relative;
    height: 20px;
}

.product_reviews_stars img {
    vertical-align: top;
}

.product_reviews_stars img.active {
    position: absolute;
    left: 0;
    width: 75%;
    object-fit: none;
    object-position: left;
    height: 100%;
    top: 0;
}

.product_boxs_section {
    display: flex;
    width: 100%;
    justify-content: space-between;
    margin-top: 25px;
    gap: 15px;
}

.product_boxs_section>div {
    padding: 10px;
    padding-left: 15px;
    border: 1px dashed rgba(0, 0, 0, .5);
    display: flex;
    align-items: center;
    gap: 15px;
}
.comment_outer{
    display: flex;
}
.comment_picture{
    width: 20%;
    text-align: center;
}
.comment_picture > img{
    aspect-ratio:1;
    object-fit: cover;
    width:50%;
    border-radius: 100%;
}
.comment_inner{
    width: 80%;
}
.comment_meta_outer{
    display: flex;
    column-gap: 25px;
}
.comment_text{
padding: 15px 35px;
    margin-top: 15px;
    border-radius: 15px;
    
}
.comment_like_dislike{
    padding: 10px;
}
.comment_like_dislike > button{
    --bs-btn-border-width:0;
}
.comment_outer{
   border-top: 1px solid var(--sixth-color);
  padding-top: 40px;
    padding-bottom: 24px;
}
.comment_outer:first-child{
    border-top: none;
}</style>
<script>
export default {
    data: () => {
        return {
            productCartCount: 1,
            products:[ 
                new ProductModel({ img: "http://img.sarowa36.com.tr/woman1.png", title: "Regular Fit Long Sleeve Top", price: "38.99", star: "5.0" }),
                new ProductModel({ img: "http://img.sarowa36.com.tr/woman2.png", title: "Black Crop Tailored Jacket", price: "62.99", star: "4.3" }),
                new ProductModel({ img: "http://img.sarowa36.com.tr/woman3.png", title: "Textured Sunset Shirt", price: "49.99", star: "5.0" })]
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