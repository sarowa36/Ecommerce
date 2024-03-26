<script setup lang="ts">
    import {VProgressCircular} from "vuetify/components"
    import type { ImgHTMLAttributes } from "vue";
    defineProps({
        img:String
        
    })
    
    
</script>
<template>
    <div class="container mt-5 mb-5 pt-1 pb-1">
        <div class="row mt-5 mb-5 justify-content-center">
            <div class="col-lg-6 p-0 identity_image_section">
                <img :src="img" alt="">
            </div>
            <div class="col-lg-6 p-0 theme_bg_3 position-relative">
                <img class="identity_form_mobile_background" :src="img" alt="">
                
                <div class="row identity_form_inner h-100 p-4 justify-content-center align-content-center gap-4">
                    <slot></slot>
                </div>
            </div>
        </div>
    </div>
</template>
<style scoped>
.identity_form_mobile_background {
    display: none;
}

.identity_image_section>img {
    height: 100%;
    object-fit: cover;
}

@media (max-width:992px) {
    .identity_form_mobile_background {
        display: initial;
        position: absolute;
        filter: blur(2px);
        height: 100%;
        width: 100%;
        opacity: 0.4;
    }

    .identity_image_section {
        display: none;
    }

    .identity_form_inner {
        background-color: rgb(255, 255, 255, .6);
        height: 100%;
        position: relative;
    }

}
</style>