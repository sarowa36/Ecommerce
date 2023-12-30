<script setup>
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
defineEmits(["increaseCartCount", "decreaseCartCount"])
defineProps({
    modelValue: {
        type: Number,
        default: 1
    }
})
</script>
<template>
    <div class="cart_counter">
        <div class="cart_counter_inner">
        <button :class="{ 'btn btn-outline-dark': true, 'active': modelValue == 1 }" :disabled="modelValue == 1"
            @click="$emit('decreaseCartCount')">
            <FontAwesomeIcon icon="minus" />
        </button>
        <input type="text" class="form-control border-dark" :value="modelValue" disabled>
        <button class="btn btn-outline-dark" @click="$emit('increaseCartCount')">
            <FontAwesomeIcon icon="plus" />
        </button>
        <div v-if="isLoading" class="cart_counter_overlay">
            <FontAwesomeIcon icon="circle-notch" />
        </div>
    </div>
       
    </div>
</template>
<style>
.cart_counter_inner{
    position: relative;
    display: flex;
    width:fit-content;
}
.cart_counter_inner>button {
  aspect-ratio: 1;
  border-radius: 0;
}
.cart_counter_inner>button:first-child{
  border-right: 0;
}
.cart_counter_inner>button:last-child{
  border-left: 0;
}
.cart_counter_inner>input {
  width: 46px;
  text-align: center;
  outline-color: unset;
  border-radius: 0;
  border-left: 0;
  border-right: 0;
}
.cart_counter_overlay{
    background-color: color-mix(in srgb, var(--third-color) 80%, transparent);
    position: absolute;
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100%;
    width:100%;
}
.cart_counter_overlay > svg{
    color: white;
    font-size: 25px;
    animation: rotating 2s linear infinite;
}
@keyframes rotating {
  from {
    transform: rotate(0deg);
  }
  to {
    transform: rotate(360deg);
  }
}
</style>
