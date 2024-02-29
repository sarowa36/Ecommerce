<script setup>
import { DashboardLayout } from '@/components/dashboard';
import { AdminOrderComponent, OrderStatus,OrderStatusDescriber, OrderValue } from '@/components/orderComponent';
import axios from 'axios';
import { FilterValueArray,FilterComponent, FilterTypeEnum, FilterValue } from '@/components/filterComponent';
import {enumToKeyArray} from "@/helpers/"
import { VPagination } from 'vuetify/components';
</script>
<template>
    <DashboardLayout>
            <div class="col-12 mt-3 mb-3">
                <FilterComponent v-model="filterValues" describeText="Select List Value" :filterType="FilterTypeEnum.checkboxListOnlyOneSelectable" icon="filter" @applyEvent="loadOrders" />
            </div>
            <AdminOrderComponent v-for="(item,index) in orders" v-model="orders[index]" :key="index"></AdminOrderComponent>
            <v-pagination v-model="page" :length="totalPage" :total-visible="7" @update:modelValue="paginationChange"></v-pagination>
</DashboardLayout>
</template>
<script>
const maxCount=12;
export default {
    data(){
        return { 
            orders:[new OrderValue()],
            filterValues:new FilterValueArray(),
            page: 1,
            totalPage:0
        }
    },
    mounted(){
        this.filterValues.push(new FilterValue({selected:true,text:"All",value:null}))
        enumToKeyArray(OrderStatus).forEach(key=>this.filterValues.push(new FilterValue({value:key,text:OrderStatusDescriber(key)})))
        this.loadOrders()
    },
    methods:{
        async loadOrders(){
            this.orders=[];
            var response= await axios.get("Admin/Order/GetList",{params:{orderStatus:this.filterValues.GetSelectedValues()[0].value,index:(this.page-1)*maxCount}});
            if(response.isSuccess){
                response.data.values.forEach(element => {
                    this.orders.push(new OrderValue(element))
                });
                this.totalPage=response.data.count/maxCount;
            }
        },
        tabchange(){
            console.log(arguments)
        },
        async paginationChange(newPage){
            this.page=newPage;
            this.loadOrders()
        }
    }
}
</script>
