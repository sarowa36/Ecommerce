<script setup>
import { RouterLink } from 'vue-router';
import TextBox from '@/components/TextBox.vue';
import { useLoginStore } from '@/stores/LoginStore';
import {useCartStore} from "@/stores/CartStore"
import axios from "axios"
import {router,router_names} from '@/router';
</script>
<template>
    <div class="container mt-5 mb-5 pt-1 pb-1">
        <div class="row mt-5 mb-5 justify-content-center">
            <div class="col-lg-5 p-0 identity_image_section">
                <img src="@/assets/img/LoginImage.svg" alt="">
            </div>
            <div class="col-lg-6 p-0 theme_bg_3 position-relative">
                <img class="identity_form_mobile_background" src="@/assets/img/LoginImage.svg" alt="">
                <div class="row identity_form_inner h-100 p-4 justify-content-center align-content-center gap-4">
                    <h4 class="col-12 text-center font_roboto_mono text_theme">Login</h4>
                    <span class="col-12 text-danger white-space-pre-line text-center">{{ errors.modelOnly }}</span>
                    <div class="col-md-7 form-group">
                        <TextBox placeholder="Email" v-model="model.email" :errorMessage="errors.email" />
                    </div>
                    <div class="col-md-7 form-group">
                        <TextBox placeholder="Password" type="password" v-model="model.password"
                            :errorMessage="errors.password" />
                    </div>
                    <div class="col-md-7 form-group identity_form_buttons">
                        <button class="btn btn-outline-primary" @click="sendRequest">Sign In</button>
                        <RouterLink :to="{name:router_names.register}" class="btn btn-outline-primary">Register</RouterLink>
                    </div>
                    <div class="col-md-7 text-center">
                        <RouterLink to="" class="fs-5 font_roboto_mono text_theme">Forgot Password?</RouterLink>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    data() {
        return {
            model: {
                email: "",
                password: ""
            },
            errors: {},
            cartStore:useCartStore(),
            loginStore: useLoginStore()
        };
    },
    methods: {
        async sendRequest() {
            var response = (await axios.postForm("Identity/Login", this.model));
            this.errors = {};
            if (response.isSuccess) {
               await this.loginStore.loadUser();
               await this.cartStore.loadCart();
                router.push("/")
            }
            else
                this.errors = response.data;
        }
    }
}
</script>