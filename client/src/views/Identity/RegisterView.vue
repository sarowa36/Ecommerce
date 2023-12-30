<script setup>
import { RouterLink } from 'vue-router';
import TextBox from '../../components/TextBox.vue';
import axios from "axios"
import { useLoginStore } from './../../stores/LoginStore';
import router from '../../router';
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
                    <h4 class="col-12 text-center font_roboto_mono text_theme">Register {{ loginStore.user.name }}</h4>
                    <span class="col-12 text-danger white-space-pre-line text-center">{{ errors.modelOnly }}</span>
                    <div class="col-12">
                        <TextBox placeholder="Name" v-model="model.name" :errorMessage="errors.name" />
                    </div>
                    <div class="col-12">
                        <TextBox placeholder="Surname" v-model="model.surname" :errorMessage="errors.surname" />
                    </div>
                    <div class="col-12">
                        <TextBox placeholder="Email" v-model="model.email" :errorMessage="errors.email" />
                    </div>
                    <div class="col-12">
                        <TextBox placeholder="Phone Number" v-model="model.phoneNumber"
                            :errorMessage="errors.phoneNumber" />
                    </div>
                    <div class="col-12">
                        <TextBox placeholder="Password" type="password" v-model="model.password" :errorMessage="errors.password" />
                    </div>
                    <div class="col-12">
                        <TextBox placeholder="Password Confirm" type="password" v-model="model.passwordConfirm"
                            :errorMessage="errors.passwordConfirm" />
                    </div>
                    <div class="col-md-7 identity_form_buttons">
                        <button class="btn btn-outline-primary" @click="sendRequest">Register</button>
                    </div>
                    <div class="col-md-12 text-center">
                        <RouterLink to="/Login" class="fs-5 font_roboto_mono text_theme">Do You Have Account Already?</RouterLink>
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
                name: "",
                surname: "",
                email: "",
                phoneNumber: "",
                password: "",
                passwordConfirm: "",
            },
            errors: {},
            loginStore: useLoginStore()
        }
    },
    methods: {
        async sendRequest() {
            var response = (await axios.postForm("Identity/Register", this.model));
            this.errors = {};
             if (response.isSuccess) {
                this.loginStore.loadUser();
                router.push("/")
            }
            else
                this.errors = response.data;
        }
    }
}
</script>