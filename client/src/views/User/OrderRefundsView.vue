<script setup>
import { UserOrderRefundItemComponent, OrderRefundValue, OrderRefundItemValue } from '@/components/userOrderRefundItemComponent/';
import { DashboardLayout } from "@/components/dashboard"
import axios from 'axios';
</script>
<template>
    <DashboardLayout>
        <div class="row pt-5">
            <UserOrderRefundItemComponent v-for="(item, key) in values" v-model="values[key]" :key="key">
            </UserOrderRefundItemComponent>
        </div>
    </DashboardLayout>
</template>
<script>
export default {
    data() {
        return {
            values: [new OrderRefundValue({ refundItems: [new OrderRefundItemValue({ productImage: "/Admin/Product/fc98c776-8944-4bde-a332-c99b01380250/woman1.png", })], sellerResponse: "I cant accept that", userMessage: "I dont want this items", })]
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