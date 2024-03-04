<script setup>
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { useCitiesAndDistrictsStore } from "@/stores/CitiesAndDistrictsStore";
import { OrderValue, OrderStatus,OrderLayout } from ".";
import {OrderItemStatus} from "@/components/orderItemComponent"
import { VDialog, VCard, VBtn, VCardText, VCardActions, VSpacer, VDataTable, VImg } from "vuetify/components";
import { QuantityCounterComponent } from "../quantityCounterComponent";
import TextBox from "@/components/TextBox.vue";
import axios from "axios";

defineProps({
    modelValue: {
        type: OrderValue,
        req: true
    }
})
</script>
<template>
    <OrderLayout v-model="value">
        <template #actionButtons>
            <a href="#" class="btn btn-outline-primary">
                <FontAwesomeIcon icon="people-carry-box" />Cargo Tracking
            </a>
            <!-- <button v-if="value.orderStatus == OrderStatus.Delivered" class="btn btn-outline-primary"
                @click="() => showRefundDialog = true">
                <FontAwesomeIcon icon="ban" />Refund
            </button> -->
            <button v-if="value.orderStatus == OrderStatus.WaitingApprove" class="btn btn-outline-primary"
                @click="() => showCancelDialog = true">
                <FontAwesomeIcon icon="ban" />Cancel
            </button>
            <a href="#" class="btn btn-outline-primary">
                <FontAwesomeIcon icon="file-invoice" />Show Bill
            </a>
        </template>
    </OrderLayout>
    <v-dialog :modelValue="showRefundDialog" width="100%" @update:modelValue="(val) => showRefundDialog = val">
        <v-card title="Refund">
            <v-card-text>
                Are you sure for Refund?
            </v-card-text>
            <v-data-table @update:model-value="updateItems"
                :items="value.orderItems.filter(x => x.orderItemStatus == OrderItemStatus.NotOnRefundProccess)" 
                showSelect
                footer
                :headers="[{ title: 'Id', key: 'id' }, { title: 'Product Name', key: 'productName' }, { title: 'Product Image', key: 'productImage' }, { title: 'Item count', key: 'itemCount' }]">
                <template #item.productImage="{ item }">
                    <v-img :src="item.productImage" height="150px" position="left" aspectRatio="1"></v-img>
                </template>
                <template #item.itemCount="{ item }">
                  <!-- <QuantityCounterComponent v-model="items[item.id].quantity"></QuantityCounterComponent> -->
                </template>
                <template #bottom></template>
            </v-data-table>
            <TextBox class="col-12 ps-3 pe-3" v-model="message" placeholder="Your Refund Message"
                :errorMessage="errors.message" />
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn text="Cancel" @click="() => showRefundDialog = false"></v-btn>
                <v-btn text="Refund" color="red" @click="refundOrder"></v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
    <v-dialog :modelValue="showCancelDialog" width="500" @update:modelValue="(val) => showCancelDialog = val">
        <v-card title="Cancel">
            <v-card-text>
                Are you sure for Cancel?
            </v-card-text>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn text="No" @click="() => showCancelDialog = false"></v-btn>
                <v-btn text="Yes" color="red" @click="cancelOrder"></v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>
<script>
export default {
    data() {
        return {
            show: false,
            citiesAndDistricts: useCitiesAndDistrictsStore(),
            errors: {},
            selectedRefundIds: [],
            items:{},
            message: "",
            showRefundDialog: false,
            showCancelDialog: false
        }
    },
    mounted(){
        this.modelValue.orderItems.forEach(x=>this.items[x.id]={selected:false,quantity:1})
    },
    methods: {
        toggleOrder() {
            this.show = !this.show;
        },
        async refundOrder() {
            console.log(this.selectedRefundIds)
            var response = await axios.postForm("User/OrderRefund/CreateRefund", { ids: this.selectedRefundIds, message: this.message })
            if (response.isSuccess) {
                this.showRefundDialog = false;
            }
            else {
                this.errors = response.data;
            }
        },
        async cancelOrder() {
            var response = await axios.postForm("User/Order/Cancel", { orderId: this.value.id })
            if (response.isSuccess) {
                this.showCancelDialog = false;
                this.value.orderStatus = OrderStatus.Cancelled;
            }
            else {
                this.errors = response.data;
            }
        },
        updateItems(newValAry){
            Object.keys(this.items).forEach(key=>this.items[key].selected=newValAry.includes(parseInt(key)));
        }
    },
    computed: {
        value: {
            get() {
                return this.modelValue;
            },
            set(val) {
                this.$emit("update:modelValue", val);
            }
        }
    },
}
</script>
<style scoped>
.btn>svg {
    margin-right: 10px;
}
</style>