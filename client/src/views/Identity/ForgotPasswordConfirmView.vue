<script setup>
    import { IdentityPageLayout } from '@/components/identityPageLayout';
    import { VProgressCircular } from 'vuetify/components';
    import axios from 'axios';
    import TextBox from '@/components/TextBox.vue';
    import { useToast } from 'vue-toastification';
</script>
<template>
    <IdentityPageLayout img="/registerimage.svg">
        <span class="col-12 text-danger white-space-pre-line text-center">{{ errors.modelOnly }}</span>
        <div class="col-md-7">
            <TextBox v-model="newPassword" placeholder="Yeni şifre" :error-message="errors.newPassword" />
        </div>
        <div class="col-md-7">
            <TextBox v-model="newPasswordConfirm" placeholder="Yeni şifre tekrar" :error-message="errors.newPasswordConfirm" />
        </div>
        <div class="col-md-7 p-2">
            <button class="btn btn-outline-primary p-3 w-100" @click="sendRequest" :disabled="isLoading">
                <template v-if="isLoading">
                    <VProgressCircular indeterminate />
                </template>
                <template v-else>Gönder</template></button>
        </div>
    </IdentityPageLayout>
</template>
<script>
    export default {
        data() {
            return {
                newPassword: "",
                newPasswordConfirm: "",
                errors: {},
                toast: useToast(),
            }
        },
        methods: {
            async sendRequest() {
                var response = await axios.postForm("Identity/ForgotPasswordConfirm", {
                    newPassword: this.newPassword,
                    newPasswordConfirm: this.newPasswordConfirm,
                    token: this.$route.params.token,
                    userId: this.$route.params.userId
                });
                if (response.isSuccess)
                    this.toast.success("Şifre değiştirme işleminiz başarılı. Yeni şifrenizle giriş yapabilirsiniz");
                else
                    this.errors = response.data;
            }
        }
    }
</script>