<script setup>
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';


</script>

<template>
    <div class="theme_bg">
        <div class="container pt-5 pb-5">
            <div class="row">
                <div class="col-7 introduce_letters">
                    <h1 class="introduce_title">
                        Discover and Find Your Own Fashion!
                    </h1>
                    <span class="introduce_desc">
                        Explore our curated collection of stylish clothing and accessories tailored to your unique taste.
                    </span>
                    <button class="btn theme_filled_btn">
                        Explore Now
                    </button>
                </div>
                <div class="col-5">
                    <img src="../assets/greenwoman.png" alt="">
                </div>
            </div>
        </div>
    </div>
    <section class="container mt-5">
        <div class="row pt-5">
            <h2 class="col-12 section_title">Best Selling</h2>
            <span class="col-12 section_desc">Get in on the trend with our curated selection of best-selling styles.</span>
        </div>
        <div class="row pt-3 bs_products">
            <div class="col-4 d-flex flex-wrap text-center" v-for="pr in bestSellingProducts">
                <img :src="pr.img" alt="" class="col-12">
                <span class="col-12 mt-4 bs_title">{{ pr.title }}</span>
                <span class="col-6 bs_price">${{ pr.price }}</span>
                <span class="col-6 bs_star">{{ pr.star }} <FontAwesomeIcon icon="star"></FontAwesomeIcon></span>
            </div>
        </div>
        <div class="row pt-5 justify-content-center">
            <button class="btn theme_btn col-2">See All</button>
        </div>
    </section>
    <section class="container mt-5 theme_bg">
        <div class="row">
            <div class="col-5 d-flex position-relative">
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
            <div class="col-6 d-flex flex-column justify-content-evenly">
                <h3 class="offer_title">Exclusive offer</h3>
                <span class="offer_desc">
                    Unlock the ultimate style upgrade with our exclusive offer Enjoy savings of up to 40% off on our latest New Arrivals
                </span>
                <div class="offer_timer">
                    <div class="offer_day"><h5>06</h5><span>Days</span></div>
                    <div class="offer_hour"><h5>18</h5><span>Hours</span></div>
                    <div class="offer_minute"><h5>48</h5><span>Minute</span></div>
                </div>
                <button class="btn theme_filled_btn">Get Now</button>
            </div>
        </div>
    </section>
    <section class="container mt-4">
        <div class="row">
            <h2 class="section_title">Our Products</h2>
            <div class="product_filters d-flex justify-content-evenly mt-3 mb-5">
                <a href="#">Sale</a>
                <a href="#">Hot</a>
                <a href="#">New Arrivals</a>
                <a href="#">Accessories</a></div>
           
           <div class="col-3 d-flex flex-wrap text-center" v-for="pr in bestSellingProducts">
                <img :src="pr.img" alt="" class="col-12">
                <span class="col-12 mt-4 bs_title">{{ pr.title }}</span>
                <span class="col-6 bs_price">${{ pr.price }}</span>
                <span class="col-6 bs_star">{{ pr.star }} <FontAwesomeIcon icon="star"></FontAwesomeIcon></span>
            </div>
        </div>
    </section>
</template>
<style scoped>

.introduce_title {
    color: #224F34;
    font-family: Rufina;
    font-size: 86px;
    font-style: normal;
    font-weight: 700;
    line-height: normal;
}

.introduce_desc {
    color: #267D49;
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

.theme_filled_btn {
    width: fit-content;
    padding: 15px 35px;
}

.section_title {
    font-family: Roboto Slab;
    font-size: 64px;
    font-style: normal;
    font-weight: 500;
    line-height: normal;
    text-align: center;
}

.section_desc {
    font-size: 22px;
    font-style: normal;
    font-weight: 500;
    line-height: normal;
    text-align: center;
}

.bs_title {
    color: #373737;
    text-align: center;
    font-size: 21px;
    font-style: normal;
    font-weight: 600;
    line-height: normal;
}

.bs_price {
    color: #454545;
    font-family: Poppins;
    font-size: 19px;
    font-style: normal;
    font-weight: 500;
    line-height: normal;
    border-right: 1px solid black;
}

.bs_star {
    color: #454545;
    font-size: 19px;
    font-style: normal;
    font-weight: 500;
    line-height: normal;
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
    background-color: #DFFBEA;
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
    background-color: #328B56;
}

.ellipse_group+img {
    z-index: 2;
    margin-left: 60px;
}
.offer_title{
    color: #224F34;
font-family: Roboto Slab;
font-size: 46px;
font-style: normal;
font-weight: 700;
line-height: normal;
}
.offer_desc{
    color: #224F34;
font-family: Poppins;
font-size: 22px;
font-style: normal;
font-weight: 500;
line-height: 167%;
}
.offer_timer{
    display: flex;
gap: 35px;
}
.offer_timer > div{
    color: #224F34;
    padding: 16px 29px;
background-color: white;
border-radius: 3px;
box-shadow: 0px 7px 30px 0px rgba(0, 0, 0, 0.05);
}
.offer_timer > div>h5{
    font-size: 32px;
    font-weight: 600;
}
.offer_timer > div>span{
font-size: 16px;
font-weight: 500;
}
.product_filters>a{
    color: #454545;
font-family: Poppins;
font-size: 22px;
font-style: normal;
font-weight: 500;
line-height: normal;
text-transform: uppercase;
border-bottom: 1px solid transparent;
}
.product_filters>a:where(.active, :hover){
    color: #224F34;
    border-bottom: 1px solid #224F34;
}</style>
<script>
export default {
    data() {
        return {
            bestSellingProducts: [
                { img: "src/assets/woman1.png", title: "Regular Fit Long Sleeve Top", price: "38.99", star: "5.0" },
                { img: "src/assets/woman2.png", title: "Black Crop Tailored Jacket", price: "62.99", star: "4.3" },
                { img: "src/assets/woman3.png", title: "Textured Sunset Shirt", price: "49.99", star: "5.0" }
            ]
        }
    }
}
</script>