<script setup>
    import DashboardLayout from '@/components/dashboard/DashboardLayout.vue';
    import TextBox from '@/components/TextBox.vue';
    import axios from 'axios';
    import { useToast } from 'vue-toastification';
</script>
<template>
    <DashboardLayout>
        <div class="row h-100 justify-content-center align-content-center">
            <h5 class="font_roboto_mono text-center">Yeni Şifre</h5>
            <span class="col-12 text-danger white-space-pre-line text-center">{{ errors.modelOnly }}</span>
            <TextBox class="col-md-7" v-model="formValues.oldPassword" :errorMessage="errors.oldPassword"
                placeholder="Eski şifrenizi giriniz"></TextBox>
            <TextBox class="col-md-7" v-model="formValues.newPassword" :errorMessage="errors.newPassword"
                placeholder="Yeni şifrenizi giriniz"></TextBox>
            <TextBox class="col-md-7" v-model="formValues.newPasswordConfirm" :errorMessage="errors.newPasswordConfirm"
                placeholder="Yeni şifrenizi tekrar giriniz"></TextBox>
            <div class="col-md-7 p-2">
                <button class="btn btn-outline-primary p-3 w-100" @click="sendRequest" :disabled="isLoading">
                    <template v-if="isLoading">
                        <VProgressCircular indeterminate />
                    </template>
                    <template v-else>Gönder</template></button>
            </div>
        </div>
    </DashboardLayout>
</template>
<script>

    export default {
        data() {
            return {
                formValues: {
                    oldPassword: "",
                    newPassword: "",
                    newPasswordConfirm: "",
                },
                errors: {},
                toast: useToast()
            }
        },
        methods: {
            async sendRequest() {
                var response = await axios.postForm("Identity/PasswordChange", this.formValues);
                if (response.isSuccess) {
                    this.toast("İşleminiz Başarılı Lütfen yeniden giriş yapın")
                }
                else {
                    this.errors = response.data;
                }
            }
        }
    }
</script>