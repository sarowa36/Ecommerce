<script setup>
    import DashboardLayout from '@/components/dashboard/DashboardLayout.vue';
    import { router_names } from "@/router"
</script>

<template>
    <dashboard-layout>
        <div class="row h-100 justify-content-center align-content-center">
            <span class="col-12 text-danger white-space-pre-line text-center">{{ errors.modelOnly }}</span>
            <img src="/logoutimage.svg" style="max-height: 500px;" alt="">
        </div>
    </dashboard-layout>
</template>
<script>
    import axios from 'axios';

    export default {
        data(){
          return{
            errors:{}
          }  
        },
        async mounted() {
            var response = await axios.get("identity/Logout");
            if (!response.isSuccess) {
                this.errors=response.data;
            }
        }
    }
</script>
