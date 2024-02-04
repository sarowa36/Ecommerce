<script setup>
import { DashboardLayout } from "@/components/dashboard/"
import { OrderValue, UserOrderItemComponent } from "@/components/userOrderItemComponent";
import { VPagination } from "vuetify/components";
import axios from "axios";
</script>
<template>
    <DashboardLayout>
        <div class="row pt-5">
            <UserOrderItemComponent v-for="(item, key) in orders" v-model="orders[key]" v-bind:key="key"></UserOrderItemComponent>
            <v-pagination v-model="page" :length="totalPage" :total-visible="7" @update:modelValue="paginationChange"></v-pagination>
        </div>
    </DashboardLayout>
</template>
<script>
const maxCount=12;

export default {
    data() {
        return {
            orders: [new OrderValue()],
            page: 1,
            totalPage:0
        }
    },
    mounted() {
        this.loadOrders();
    },
    methods: {
        async loadOrders() {
            var { data } = await axios.get("User/Order/GetList",{params:{index:(this.page-1)*maxCount}});
            this.orders = [];
            this.totalPage=data.count/maxCount;
            data.values.forEach(item => {
                this.orders.push(new OrderValue(item))
            });
        },
        async paginationChange(newPage){
            this.page=newPage;
            this.loadOrders()
        }
    }
}
</script>