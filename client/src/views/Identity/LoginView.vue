<script setup>
    import { RouterLink } from 'vue-router';
    import TextBox from '@/components/TextBox.vue';
    import { useLoginStore } from '@/stores/LoginStore';
    import { useCartStore } from "@/stores/CartStore"
    import axios from "axios"
    import { router, router_names } from '@/router';
    import { IdentityPageLayout } from "@/components/identityPageLayout"
    import { VProgressCircular } from 'vuetify/components';
</script>
<template>
    <identity-page-layout img="/LoginImage.svg">
        <h4 class="col-12 text-center font_roboto_mono text_theme">Login</h4>
        <span class="col-12 text-danger white-space-pre-line text-center">{{ errors.modelOnly }}</span>
        <div class="col-md-7 form-group">
            <TextBox placeholder="Email" v-model="model.email" :errorMessage="errors.email" />
        </div>
        <div class="col-md-7 form-group">
            <TextBox placeholder="Password" type="password" v-model="model.password" :errorMessage="errors.password" />
        </div>
        <div class="col-md-7 form-group d-flex gap-2">
            <button class="btn btn-outline-primary w-50 p-3" @click="sendRequest" :disabled="isLoading">
                    <template v-if="isLoading">
                        <VProgressCircular indeterminate />
                    </template>
                    <template v-else>Sign In</template></button>
            <RouterLink :to="{ name: router_names.register }" class="btn btn-outline-primary w-50 p-3">Register</RouterLink>
        </div>
        <div class="col-md-7 text-center">
            <RouterLink :to="{name:router_names.forgot_password_request}" class="fs-5 font_roboto_mono text_theme">Forgot Password?</RouterLink>
        </div>
    </identity-page-layout>
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
                cartStore: useCartStore(),
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