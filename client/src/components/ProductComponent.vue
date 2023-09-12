<script setup>
import { ProductModel } from '../models/ProductModel';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';

defineProps({
    model:{
        type:ProductModel,
        required:true
    }
})
</script>
<template>
 <div class="d-flex flex-wrap text-center">
                <img :src="model.img" alt="" class="col-12 bs_img">
                <span class="col-12 mt-2 bs_title">{{ model.title }}</span>
                <span class="col-6 bs_price">${{ model.price }}</span>
                <span class="col-6 bs_star">{{ model.star }} <FontAwesomeIcon icon="star"></FontAwesomeIcon></span>
            </div>
</template>
<style>
.bs_title {
  color: var(--third-color);
  text-align: center;
  font-size: 21px;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
}

.bs_price {
  color: var(--third-color);
  font-family: var(--first-font);
  font-size: 19px;
  font-style: normal;
  font-weight: 500;
  line-height: normal;
  border-right: 1px solid black;
  height: fit-content;
}

.bs_star {
  color: var(--third-color);
  font-size: 19px;
  font-style: normal;
  font-weight: 500;
  line-height: normal;
  height: fit-content;
}
.bs_img{
  height: fit-content;
}</style>