<script setup>
    import { IdentityPageLayout } from '@/components/identityPageLayout';
    import { VProgressCircular } from 'vuetify/components';
    import axios from 'axios';
    import TextBox from '@/components/TextBox.vue';
</script>
<template>
    <IdentityPageLayout img="/registerimage.svg">
        <div class="h4 font_roboto_mono text-center">Şifremi unuttum</div>
        <span class="col-12 text-danger white-space-pre-line text-center">{{ errors.modelOnly }}</span>
        <div class="col-md-7 form-group">
            <TextBox v-model="email" placeholder="Email adresinizi giriniz" :error-message="errors.email" />
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
import { useToast } from 'vue-toastification';
    export default {
        data() {
            return {
                isSuccess: false,
                email: "",
                toast:useToast(),
                errors:{},
            }
        },
        methods:{
            async sendRequest(){
              var response= await axios.postForm("Identity/ForgotPasswordRequest",{email:this.email});
              if (response.isSuccess)
                    this.toast.success("Eposta adresinize gelen bağlantıya tıklayınız");
                else
                    this.errors = response.data;
            }
        }
    }
</script>