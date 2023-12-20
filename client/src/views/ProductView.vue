<script setup>
import Carousel from "../components/Carousel.vue";
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { Tabs, Tab } from 'vue3-tabs-component';
import { ProductComponent, ProductComponentValue } from "@/components/productComponent";
</script> 
<template>
    <div class="container mb-5">
        <div class="row justify-content-center theme_bg_3 mt-5 pt-4 pb-4">
            <div class="col-md-8 col-lg-5">
                <Carousel :items="1" :nav="false">
                    <img src="http://img.sarowa36.com.tr/woman1.png" alt="">
                    <img src="http://img.sarowa36.com.tr/woman2.png" alt="">
                    <img src="http://img.sarowa36.com.tr/woman3.png" alt="">
                </Carousel>
            </div>
            <div class="col-lg-6 product_description">
                <h3 class="text_theme">Piece of beauty</h3>
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
                    <div class="cart_counter">
                        <button class="btn btn-outline-dark" @click="decreaseCartCount">
                            <FontAwesomeIcon icon="minus" />
                        </button>
                        <input type="text" class="form-control border-dark" :value="productCartCount" disabled>
                        <button class="btn btn-outline-dark" @click="increaseCartCount">
                            <FontAwesomeIcon icon="plus" />
                        </button>
                    </div>
                    <button class="btn btn-primary">
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
                    <div class="save_btn_outer">
                        <button :class="'btn show_list_dropdown' + (showListDropdown ? ' active' : '')"
                            @click="toggleShowListDropdown">
                            <FontAwesomeIcon :icon="['far', 'bookmark']" /> Save
                        </button>
                        <div v-if="showListDropdown" class="save_btn_dropdown">
                            <h6>Add To List</h6>
                            <ul>
                                <li><button class="btn">Lorem Ipsum
                                        <FontAwesomeIcon :icon="['far', 'bookmark']" />
                                    </button></li>
                                <li><button class="btn">Lorem Ipsum
                                        <FontAwesomeIcon :icon="['fas', 'bookmark']" />
                                    </button></li>
                                <li><button class="btn">Lorem Ipsum
                                        <FontAwesomeIcon :icon="['far', 'bookmark']" />
                                    </button></li>
                            </ul>
                        </div>
                    </div>
                    <div class="share_btn_outer">
                        <button :class="'btn' + (showShareDropdown ? ' active' : '')" @click="toggleShowShareDropdown">
                            <FontAwesomeIcon icon="share" /> Share
                        </button>
                        <div v-if="showShareDropdown" class="share_btn_dropdown">
                            <a href="#">
                                <FontAwesomeIcon icon="link" />
                            </a>
                            <a href="#">
                                <FontAwesomeIcon :icon="['fab', 'whatsapp']" />
                            </a>
                            <a href="#">
                                <FontAwesomeIcon :icon="['fab', 'instagram']" />
                            </a>
                        </div>
                    </div>
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
                                    <div>
                                        12 October, Saturday 2023 | D***** Z*****
                                    </div>
                                </div>
                                <div class="comment_text theme_bg_3">
                                    Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum
                                    has been the industry's standard dummy text ever since the 1500s, when an unknown
                                    printer took a galley of type and scrambled it to make a type specimen book. It has
                                    survived not only five centuries, but also the leap into electronic typesetting,
                                    remaining essentially unchanged. It was popularised in the 1960s with the release of
                                    Letraset sheets containing Lorem Ipsum passages, and more recently with desktop
                                    publishing software like Aldus PageMaker including versions of Lorem Ipsum.
                                </div>
                                <div class="comment_like_dislike">
                                    <span>Is this comment useful?</span>
                                    <button class="btn">
                                        <FontAwesomeIcon :icon="['far', 'thumbs-up']" /> 3
                                    </button>
                                    <button class="btn">
                                        <FontAwesomeIcon :icon="['far', 'thumbs-down']" /> 2
                                    </button>
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
                                    <div>
                                        12 October, Saturday 2023 | D***** Z*****
                                    </div>
                                </div>
                                <div class="comment_text theme_bg_3">
                                    Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum
                                    has been the industry's standard dummy text ever since the 1500s, when an unknown
                                    printer took a galley of type and scrambled it to make a type specimen book. It has
                                    survived not only five centuries, but also the leap into electronic typesetting,
                                    remaining essentially unchanged. It was popularised in the 1960s with the release of
                                    Letraset sheets containing Lorem Ipsum passages, and more recently with desktop
                                    publishing software like Aldus PageMaker including versions of Lorem Ipsum.
                                </div>
                                <div class="comment_like_dislike">
                                    <span>Is this comment useful?</span>
                                    <button class="btn">
                                        <FontAwesomeIcon :icon="['far', 'thumbs-up']" /> 3
                                    </button>
                                    <button class="btn">
                                        <FontAwesomeIcon :icon="['far', 'thumbs-down']" /> 2
                                    </button>
                                </div>
                            </div>
                        </div>
                    </Tab>
                    <Tab name="Questions">
                        <div class="question">
                            <h5>Question</h5>
                            <div class="question_text theme_bg_3">
                                Lorem ipsum dolor sit amet consectetur adipisicing elit. Necessitatibus doloremque iusto
                                perspiciatis, illum possimus vel?
                            </div>
                            <div class="question_meta">
                                12 September 2023 | A**** Z****
                            </div>
                            <h5>Answer</h5>
                            <div class="question_answer">
                                Lorem ipsum dolor sit amet consectetur adipisicing elit. Magni, corrupti!
                            </div>
                            <div>
                                12 September 2023
                            </div>
                        </div>
                        <div class="question">
                            <h5>Question</h5>
                            <div class="question_text theme_bg_3">
                                Lorem ipsum dolor sit amet consectetur adipisicing elit. Necessitatibus doloremque iusto
                                perspiciatis, illum possimus vel?
                            </div>
                            <div class="question_meta">
                                12 September 2023 | A**** Z****
                            </div>
                            <h5>Answer</h5>
                            <div class="question_answer">
                                Lorem ipsum dolor sit amet consectetur adipisicing elit. Magni, corrupti!
                            </div>
                            <div>
                                12 September 2023
                            </div>
                        </div>
                    </Tab>
                </Tabs>
            </div>
            <h1 class="text-center">Best Sellers</h1>
            <div class="divider_line mb-3"></div>
            <div class="col-12">
                <Carousel :loop="true" :responsive="{ 0: { items: 1, nav: false }, 992: { items: 3 } }">
                    <ProductComponent v-for="pr in products" :value="pr" class="m-3"></ProductComponent>
                </Carousel>
            </div>
        </div>
    </div>
</template>
<style scoped>
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

.comment_outer {
    display: flex;
}

.comment_picture {
    width: 20%;
    text-align: center;
}

.comment_picture>img {
    aspect-ratio: 1;
    object-fit: cover;
    width: 50%;
    border-radius: 100%;
}

.comment_inner {
    width: 80%;
}

.comment_meta_outer {
    display: flex;
    column-gap: 25px;
}

.comment_text {
    padding: 15px 35px;
    margin-top: 15px;
    border-radius: 15px;

}

.comment_like_dislike {
    padding: 10px;
}

.comment_like_dislike>button {
    --bs-btn-border-width: 0;
}

.comment_outer {
    border-top: 1px solid var(--sixth-color);
    padding-top: 40px;
    padding-bottom: 24px;
}

.comment_outer:first-child {
    padding-top: 0px;
    border-top: none;
}

.question_text {
    padding: 15px;
    margin-bottom: 10px;
}

.question_meta {
    margin-bottom: 10px;
}

.question_answer {
    background-color: var(--fifth-color);
    padding: 10px;
    margin-bottom: 10px;
}

.question {
    border-top: 1px solid var(--sixth-color);
    padding-top: 24px;
    padding-bottom: 24px;
}

.question:first-child {
    border-top: none;
    padding-top: 0px;
}

.product_buttons_section {
    display: flex;
    width: 100%;
    column-gap: 25px;
}

.save_btn_outer {
    position: relative;
}

.save_btn_dropdown {
    position: absolute;
    background-color: white;
    width: 250px;
    padding: 15px;
    right: calc(-50% - 22px);
    margin-top: 10px;
}

.save_btn_dropdown ul {
    list-style-type: none;
    padding: 0;
    margin-bottom: 0;
}

.save_btn_dropdown ul button {
    width: 100%;
    display: flex;
    justify-content: space-between;
}

.share_btn_outer {
    position: relative;
}

.share_btn_dropdown {
    position: absolute;
    display: flex;
    justify-content: space-between;
    background-color: white;
    padding: 5px;
    column-gap: 5px;
    left: calc(-50% + 20px);
    border-radius: 25px;
    margin-top: 10px;
}

.share_btn_dropdown>a {
    border: 1px solid var(--first-color);
    border-radius: 50%;
    width: 50px;
    height: 50px;
    display: flex;
    align-items: center;
    justify-content: center;
    transition: 0.3s;
    color: var(--first-color);
}

.share_btn_dropdown>a:hover {
    background: black;
    color: white;
    border-color: black;
}

@media (max-width: 768px) {

    .comment_meta_outer,
    .product_boxs_section {
        flex-direction: column;
    }
}
</style>
<script>
export default {
    data: () => {
        return {
            productCartCount: 1,
            products: [
                new ProductComponentValue({ img: ["http://img.sarowa36.com.tr/woman1.png"], title: "Regular Fit Long Sleeve Top", price: "38.99", star: "5.0" }),
                new ProductComponentValue({ img: ["http://img.sarowa36.com.tr/woman2.png"], title: "Black Crop Tailored Jacket", price: "62.99", star: "4.3" }),
                new ProductComponentValue({ img: ["http://img.sarowa36.com.tr/woman3.png"], title: "Textured Sunset Shirt", price: "49.99", star: "5.0" })],
            showListDropdown: false,
            showShareDropdown: false
        }
    },
    mounted() {
        window.addEventListener("click", this.windowClickEvent)
    },
    unmounted() {
        window.removeEventListener("click", this.windowClickEvent)
    },
    methods: {
        increaseCartCount() {
            this.productCartCount++;
        },
        decreaseCartCount() {
            if (this.productCartCount > 1)
                this.productCartCount--;
        },
        toggleShowListDropdown() {
            this.showListDropdown = !this.showListDropdown;
        },
        toggleShowShareDropdown() {
            this.showShareDropdown = !this.showShareDropdown;
        },
        windowClickEvent(e) {
            /*show_share_dropdown  show_list_dropdown*/
            var eventNode = e.target;
            if (!eventNode.closest(".share_btn_outer") && this.showShareDropdown) {
                this.showShareDropdown = false;
            }
            if (!eventNode.closest(".save_btn_outer") && this.showListDropdown) {
                this.showListDropdown = false;
            }
        },
    }
}
</script>