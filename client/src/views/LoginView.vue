<script setup>
import TextBox from '../components/TextBox.vue';
import { useLoginStore } from './../stores/LoginStore';
</script>
<template>
    <div class="container mt-5 mb-5 pt-1 pb-1">
        <div class="row mt-5 mb-5 justify-content-center">
            <div class="col-lg-5 p-0 login_image_section">
                <img src="@/assets/LoginImage.svg" alt="">
            </div>
            <div class="col-lg-6 p-0 theme_bg_3 position-relative">
                <img class="login_form_mobile_background" src="@/assets/LoginImage.svg" alt="">
                <div class="row login_form_inner pt-5 pb-5 pe-4 ps-4 justify-content-center">
                    <div class="col-md-7 mt-3 form-group">
                        <TextBox placeholder="UserName" />
                    </div>
                    <div class="col-md-7 mt-3 form-group">
                        <TextBox placeholder="Password" />
                    </div>
                    <div class="col-md-7 mt-5 form-group login_form_buttons">
                        <button class="btn btn-primary" @click="login">Sign In</button><button class="btn btn-primary">Sign Up</button>
                    </div>
                    <div class="col-md-7 mt-5 text-center">
                        <RouterLink to="" class="forgot_password">Forgot Password?</RouterLink>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<style>
.login_form_buttons {
    display: flex;
    gap: 15px;
}

.login_form_buttons>* {
    flex-grow: 1;
    padding: 14px;
}

.forgot_password {
    font-family: Roboto Mono;
    font-size: 20px;
}

.login_form_mobile_background {
    display: none;
}

.login_image_section>img {
    height: 100%;
    object-fit: cover;
}

@media (max-width:992px) {
    .login_form_mobile_background {
        display: initial;
        position: absolute;
        filter: blur(2px);
        height: 100%;
        width: 100%;
    }

    .login_image_section {
        display: none;
    }

    .login_form_inner {
        background-color: rgb(255, 255, 255, .6);
        height: 100%;
        position: relative;
    }

    .login_form_buttons {
        flex-direction: column;
    }
}
</style>
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