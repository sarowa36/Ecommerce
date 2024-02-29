<script setup>
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { useCitiesAndDistrictsStore } from "@/stores/CitiesAndDistrictsStore";
import { OrderValue, OrderStatus,OrderLayout } from ".";
import {OrderItemStatus} from "@/components/orderItemComponent"
import { VDialog, VCard, VBtn, VCardText, VCardActions, VSpacer, VDataTable, VImg } from "vuetify/components";
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
            <button v-if="value.orderStatus == OrderStatus.Delivered" class="btn btn-outline-primary"
                @click="() => showRefundDialog = true">
                <FontAwesomeIcon icon="ban" />Refund
            </button>
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
            <v-data-table v-model:modelValue="selectedRefundIds"
                :items="value.orderItems.filter(x => x.orderItemStatus == OrderItemStatus.NotOnRefundProccess)" showSelect
                footer
                :headers="[{ title: 'Id', key: 'id' }, { title: 'ProductName', key: 'productName' }, { title: 'ProductImage', key: 'productImage' }]">
                <template #item.productImage="{ item }">
                    <v-img :src="item.productImage" height="150px" position="left" aspectRatio="1"></v-img>
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
            message: "",
            showRefundDialog: false,
            showCancelDialog: false
        }
    },
    methods: {
        toggleOrder() {
            this.show = !this.show;
        },
        async refundOrder() {
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