<script setup>
import { UserRefundComponent, OrderRefundValue } from '@/components/refundComponent/';
import { OrderItemValue } from '@/components/orderItemComponent';
import { DashboardLayout } from "@/components/dashboard"
import axios from 'axios';
</script>
<template>
    <DashboardLayout>
        <div class="row pt-5">
            <UserRefundComponent v-for="(item, key) in values" v-model="values[key]" :key="key">
            </UserRefundComponent>
        </div>
    </DashboardLayout>
</template>
<script>
export default {
    data() {
        return {
            values: [new OrderRefundValue({ refundItems: [new OrderItemValue({ productImage: "/Admin/Product/fc98c776-8944-4bde-a332-c99b01380250/woman1.png", })], sellerResponse: "I cant accept that", userMessage: "I dont want this items", })]
        }
    },
    mounted(){
        this.loadValues()
    },
    methods:{
        async loadValues(){
            var response=await axios.get("User/OrderRefund/Getlist");
            if(response.isSuccess){
                this.values=[];
                response.data.forEach(item=>this.values.push(new OrderRefundValue(item)))
            }
        }
    }
}
</script>