<script setup>
import { IdentityPageLayout } from '@/components/identityPageLayout';
import { VProgressCircular } from 'vuetify/components';
import axios from 'axios';
</script>
<template>
    <IdentityPageLayout img="/registerimage.svg">
        <v-progress-circular :indeterminate="isLoading" v-if="isLoading"></v-progress-circular>
        <div v-if="isSuccess" class="font_roboto_mono">Kayıt işleminiz başarılı. Lütfen eposta adresinize gönderilen linke tıklayarak hesabınızı onaylayınız.</div>
        <div v-else-if="!isLoading && !isSuccess" class="font_roboto_mono">İşleminiz başarısız. Lütfen tekrar deneyin yada iletişime geçin.</div>
    </IdentityPageLayout>
</template>
<script>
export default {
    data(){
        return{
            isSuccess:false
        }
    },
     mounted(){
        axios.postForm("Identity/EmailConfirm",{userId:this.$route.params.userId, token:this.$route.params.token}).then(response=>this.isSuccess=response.isSuccess);
    },
}
</script>