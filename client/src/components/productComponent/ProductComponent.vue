<script setup>
import { ProductComponentValue } from './';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { RouterLink } from 'vue-router';
import { router_names } from '@/router';

defineProps({
  value: {
    type: ProductComponentValue,
    required: true
  }
})
</script>
<template>
  <RouterLink :to="{name:router_names.product,params:{id:value.id}}" class="d-flex flex-wrap text-center">
    <img :src="value.image" alt="" class="col-12 bs_img">
    <span class="col-12 mt-2 bs_title">{{ value.name }}</span>
    <span class="col-6 bs_price">${{ value.price }}</span>
    <span class="col-6 bs_star">{{ value.star }} <FontAwesomeIcon icon="star"></FontAwesomeIcon></span>
  </RouterLink>
</template>
<style scoped>
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

.bs_img {
  height: fit-content;
  aspect-ratio: 12/16;
  object-fit: cover;
}
</style>