<script setup>
import { RouterLink } from 'vue-router';
import TextBox from '../components/TextBox.vue';
import { useLoginStore } from './../stores/LoginStore';
</script>
<template>
    <div class="container mt-5 mb-5 pt-1 pb-1">
        <div class="row mt-5 mb-5 justify-content-center">
            <div class="col-lg-5 p-0 identity_image_section">
                <img src="@/assets/LoginImage.svg" alt="">
            </div>
            <div class="col-lg-6 p-0 theme_bg_3 position-relative">
                <img class="identity_form_mobile_background" src="@/assets/LoginImage.svg" alt="">
                <div class="row identity_form_inner h-100 p-4 justify-content-center align-content-center gap-4">
                    <h4 class="col-12 text-center font_roboto_mono">Login</h4>
                    <div class="col-md-7 form-group">
                        <TextBox placeholder="Email" />
                    </div>
                    <div class="col-md-7 form-group">
                        <TextBox placeholder="Password" />
                    </div>
                    <div class="col-md-7 form-group identity_form_buttons">
                        <button class="btn btn-primary" @click="login">Sign In</button><RouterLink to="/Register" class="btn btn-primary">Register</RouterLink>
                    </div>
                    <div class="col-md-7 text-center">
                        <RouterLink to="" class="fs-5 font_roboto_mono">Forgot Password?</RouterLink>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    data(){
        return {
            loginStore:useLoginStore()
        };
    },
methods:{
    login(){
        this.loginStore.SignIn();
    }
}
}
</script>