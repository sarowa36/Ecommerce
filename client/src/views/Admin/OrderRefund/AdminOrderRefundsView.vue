<script setup>
import {AdminRefundComponent, OrderRefundValue} from '@/components/refundComponent';
import { DashboardLayout } from '@/components/dashboard';
import axios from 'axios';
</script>
<template>
<DashboardLayout>
    <div class="row">
        <AdminRefundComponent class="col-12" v-for="(item,index) in values" v-model="values[index]" :key="index"></AdminRefundComponent>
    </div>
</DashboardLayout>
</template>
<script>
export default {
    data(){
        return {
            values:[new OrderRefundValue()]
        }
    },
    mounted(){
        this.loadValues();
    },
    methods:{
        async loadValues(){
            var response=await axios.get("Admin/OrderRefund/GetList");
            if(response.isSuccess){
                this.values=[];
                response.data.forEach(element => {
                    this.values.push(new OrderRefundValue(element))
                });
            }
        }
    }
}
</script>