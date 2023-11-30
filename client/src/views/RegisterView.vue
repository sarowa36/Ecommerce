<script setup>
import { RouterLink } from 'vue-router';
import TextBox from '../components/TextBox.vue';
</script>
<template>
    <div class="container mt-5 mb-5 pt-1 pb-1">
        <div class="row mt-5 mb-5 justify-content-center">
            <div class="col-lg-6 p-0 identity_image_section">
                <img src="@/assets/registerimage.svg" alt="">
            </div>
            <div class="col-lg-6 p-0 theme_bg_3 position-relative">
                <img class="identity_form_mobile_background" src="@/assets/registerimage.svg" alt="">
                <div class="row identity_form_inner h-100 p-4 justify-content-center align-content-center gap-4">
                    <h4 class="col-12 text-center font_roboto_mono">Register</h4>
                    <div class="col-12 form-group">
                        <TextBox placeholder="Name" />
                    </div>
                    <div class="col-12 form-group">
                        <TextBox placeholder="Surname" />
                    </div>
                    <div class="col-12 form-group">
                        <TextBox placeholder="Email" />
                    </div>
                    <div class="col-12 form-group">
                        <TextBox placeholder="Phone Number" />
                    </div>
                    <div class="col-12 form-group">
                        <TextBox placeholder="Password" />
                    </div>
                    <div class="col-12 form-group">
                        <TextBox placeholder="Password Repeat" />
                    </div>
                    <div class="col-md-7 form-group identity_form_buttons">
                        <button class="btn btn-primary">Register</button>
                    </div>
                    <div class="col-md-12 text-center">
                        <RouterLink to="/Login" class="fs-5 font_roboto_mono">Do You Have Account Already?</RouterLink>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
