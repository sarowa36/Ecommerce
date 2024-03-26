<script setup>
    import { RouterLink } from 'vue-router';
    import TextBox from '@/components/TextBox.vue';
    import axios from "axios"
    import { useLoginStore } from '@/stores/LoginStore';
    import { router, router_names } from '@/router';
    import { IdentityPageLayout } from "@/components/identityPageLayout"
    import { VProgressCircular } from 'vuetify/components';
</script>
<template>
    <identity-page-layout img="/registerimage.svg">

        <template v-if="registerIsSuccess">
            <div class="font_roboto_mono">Kayıt işleminiz başarılı. Lütfen eposta adresinize gönderilen linke tıklayarak
                hesabınızı onaylayınız.</div>
        </template>
        <template v-else>
            <h4 class="col-12 text-center font_roboto_mono text_theme">Register</h4>
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
                <TextBox placeholder="Phone Number" v-model="model.phoneNumber" :errorMessage="errors.phoneNumber" />
            </div>
            <div class="col-12">
                <TextBox placeholder="Password" type="password" v-model="model.password"
                    :errorMessage="errors.password" />
            </div>
            <div class="col-12">
                <TextBox placeholder="Password Confirm" type="password" v-model="model.passwordConfirm"
                    :errorMessage="errors.passwordConfirm" />
            </div>
            <div class="col-md-7 p-2">
                <button class="btn btn-outline-primary p-3 w-100" @click="sendRequest" :disabled="isLoading">
                    <template v-if="isLoading">
                        <VProgressCircular indeterminate />
                    </template>
                    <template v-else>Register</template></button>
            </div>
            <div class="col-md-12 text-center">
                <RouterLink :to="{ name: router_names.login }" class="fs-5 font_roboto_mono text_theme">
                    Do You Have Account Already?
                </RouterLink>
            </div>
        </template>
    </identity-page-layout>
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
                registerIsSuccess: false,
                loginStore: useLoginStore()
            }
        },
        methods: {
            async sendRequest() {
                var response = (await axios.postForm("Identity/Register", this.model));
                this.errors = {};
                if (response.isSuccess) {
                    this.registerIsSuccess = true;
                }
                else
                    this.errors = response.data;
            }
        }
    }
</script>