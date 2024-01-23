<script setup>
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { ProductComponent, ProductComponentValue } from '@/components/productComponent';
import axios from 'axios';

</script>

<template>
    <div class="theme_bg">
        <div class="container pt-5 pb-5">
            <div class="row">
                <div class="col-lg-7 introduce_letters">
                    <h1 class="introduce_title">
                        Discover and Find Your Own Fashion!
                    </h1>
                    <span class="introduce_desc">
                        Explore our curated collection of stylish clothing and accessories tailored to your unique taste.
                    </span>
                    <button class="btn btn-primary shadow">
                        Explore Now
                    </button>
                </div>
                <div class="col-5">
                    <img class="introduce_img" src="../assets/greenwoman.png" alt="">
                </div>
            </div>
        </div>
    </div>
    <section class="container mt-5">
        <div class="row pt-5">
            <h2 class="col-12 section_title">Best Selling</h2>
            <span class="col-12 section_desc text_theme">Get in on the trend with our curated selection of best-selling
                styles.</span>
        </div>
        <div class="row pt-3 justify-content-center">
            <ProductComponent v-for="pr in bestSellingProducts.slice(0, 3)" :value="pr" class="col-sm-6 col-md-4 mt-3">
            </ProductComponent>
        </div>
        <div class="row pt-5 justify-content-center">
            <button class="btn btn-outline-primary col-2">See All</button>
        </div>
    </section>
    <section class="container mt-5 theme_bg">
        <div class="row">
            <div class="col-lg-5 position-relative offer_img_outer">
                <div class="ellipse_group">
                    <div></div>
                    <div></div>
                    <div></div>
                    <div></div>
                    <div></div>
                    <div></div>
                    <div></div>
                    <div></div>
                    <div></div>
                    <div></div>
                    <div></div>
                    <div></div>
                    <div></div>
                    <div></div>
                    <div></div>
                    <div></div>
                </div>
                <img src="/src/assets/offerwoman.png" alt="">
            </div>
            <div class="col-lg-6 d-flex flex-column justify-content-evenly offer_details">
                <h3 class="offer_title">Exclusive offer</h3>
                <span class="offer_desc">
                    Unlock the ultimate style upgrade with our exclusive offer Enjoy savings of up to 40% off on our latest
                    New Arrivals
                </span>
                <div class="offer_timer">
                    <div class="offer_day">
                        <h5>06</h5><span>Days</span>
                    </div>
                    <div class="offer_hour">
                        <h5>18</h5><span>Hours</span>
                    </div>
                    <div class="offer_minute">
                        <h5>48</h5><span>Minute</span>
                    </div>
                </div>
                <button class="btn btn-primary shadow">Get Now</button>
            </div>
        </div>
    </section>
    <section class="container mt-4 mb-5">
        <div class="row">
            <h2 class="section_title">Our Products</h2>
            <div class="link_list mt-3 mb-3">
                <a href="#" class="font-xl" @click.prevent>Sale</a>
                <a href="#" class="font-xl" @click.prevent>Hot</a>
                <a href="#" class="font-xl" @click.prevent>New Arrivals</a>
                <a href="#" class="font-xl" @click.prevent>Accessories</a>
            </div>
            <ProductComponent v-for="pr in bestSellingProducts" :value="pr" class="col-sm-6 col-lg-3 mt-3">
            </ProductComponent>
        </div>
    </section>
</template>
<style scoped>
.introduce_title {
    font-family: var(--second-title-font);
    color: var(--first-color);
    font-size: 86px;
    font-weight: 700;
}

.introduce_desc {
    color: var(--second-color);
    font-size: 24px;
    font-style: normal;
    font-weight: 500;
    line-height: 220%;
}

.introduce_letters {
    display: flex;
    flex-direction: column;
    justify-content: center;
}

.btn-primary {
    width: fit-content;
    padding: 15px 35px;
}

.section_desc {
    font-size: 22px;
    font-style: normal;
    font-weight: 500;
    line-height: normal;
    text-align: center;
}


.ellipse_group {
    display: grid;
    grid-template: "s s s s";
    row-gap: 28px;
    width: calc(35px * 4);
    height: fit-content;
    bottom: 35px;
    left: 35px;
    position: absolute;
    z-index: 2;
}

.ellipse_group::before {
    background-color: var(--fifth-color);
    content: '';
    position: absolute;
    height: 100%;
    aspect-ratio: 1;
    transform: translateX(50px) translateY(-30px);
    z-index: -1;
}

.ellipse_group>div {
    padding: 3px;
    width: fit-content;
    clip-path: ellipse();
    background-color: var(--fourth-color);
}

.ellipse_group+img {
    z-index: 2;
    margin-left: 60px;
}

.offer_title {
    color: var(--first-color);
    font-family: var(--first-title-font);
    font-size: 46px;
    font-style: normal;
    font-weight: 700;
    line-height: normal;
}

.offer_desc {
    color: var(--first-color);
    font-family: var(--first-font);
    font-size: 22px;
    font-style: normal;
    font-weight: 500;
    line-height: 167%;
}

.offer_timer {
    display: flex;
    gap: 35px;
    flex-wrap: wrap;
}

.offer_timer>div {
    padding: 16px 29px;
    background-color: var(--white-color);
    border-radius: 3px;
    box-shadow: 0px 7px 30px 0px rgba(0, 0, 0, 0.05);
}

.offer_timer>div>h5 {
    font-size: 32px;
    font-weight: 600;
}

.offer_timer>div>span {
    font-size: 16px;
    font-weight: 500;
}

.offer_img_outer {
    display: flex;
}

.offer_details {
    row-gap: 15px;
    padding: 25px 35px;
}
@media (max-width:992px) {
    .offer_img_outer,
    .introduce_img {
        display: none;
    }
}
@media (max-width: 768px) {
    .introduce_title {
        font-size: 60px;
    }

    .introduce_desc {
        font-size: 20px;
    }
}
</style>
<script>
export default {
    data() {
        return {
            bestSellingProducts: [
                /*new ProductComponentValue({ image: ["http://img.sarowa36.com.tr/woman1.png"], name: "Regular Fit Long Sleeve Top", price: "38.99", star: "5.0" }),
                new ProductComponentValue({ image: ["http://img.sarowa36.com.tr/woman2.png"], name: "Black Crop Tailored Jacket", price: "62.99", star: "4.3" }),
                new ProductComponentValue({ image: ["http://img.sarowa36.com.tr/woman3.png"], name: "Textured Sunset Shirt", price: "49.99", star: "5.0" }),
                new ProductComponentValue({ image: ["http://img.sarowa36.com.tr/woman3.png"], name: "Textured Sunset Shirt", price: "49.99", star: "5.0" })*/
            ]
        }
    },
    mounted(){
        this.loadProducts();
    },
    methods:{
        async loadProducts(){
            this.bestSellingProducts=[];
            var res=await axios.get("Anonym/Product/GetList");
            if(res.isSuccess && res.data.length>0){
                res.data.slice(0,4).forEach(x=>{
                    this.bestSellingProducts.push(new ProductComponentValue(x));
                })
            }
        }
    }
}
</script>