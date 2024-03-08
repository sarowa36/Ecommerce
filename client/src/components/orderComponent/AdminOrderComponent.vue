<script setup>
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { OrderValue, OrderLayout,OrderStatus } from ".";
import { VDialog, VCard, VBtn, VCardText, VCardActions, VSpacer } from "vuetify/components";
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
        <template #actionButtons v-if="value.orderStatus == OrderStatus.WaitingApprove">
            <button class="btn btn-success" @click="acceptOrder" :disabled="isLoading">
                <FontAwesomeIcon icon="check" /> Accept
            </button>
            <button class="btn btn-danger" @click="ignoreOrder" :disabled="isLoading">
                <FontAwesomeIcon icon="check" /> Ignore
            </button>
        </template>
        <template #actionButtons v-else-if="value.orderStatus == OrderStatus.ApprovedAndPreparing">
            <TextBox placeholder="Enter Cargo Code" v-model="cargoCode" :errorMessage="errors.cargoCode" />
            <button class="btn btn-success" @click="sendToCargo" :disabled="isLoading">
                <FontAwesomeIcon icon="check" /> Accept
            </button>
            <button class="btn btn-danger" @click="ignoreOrder" :disabled="isLoading">
                <FontAwesomeIcon icon="check" /> Cancel
            </button>
        </template>
        <template #actionButtons v-else-if="value.orderStatus == OrderStatus.OnCargo">
            <button class="btn btn-success" @click="deliverOrder" :disabled="isLoading">
                <FontAwesomeIcon icon="check" />Set Delivered
            </button>
            <button class="btn btn-danger" @click="ignoreOrder" :disabled="isLoading">
                <FontAwesomeIcon icon="check" /> Cancel
            </button>
        </template>
    </OrderLayout>

    <v-dialog :modelValue="showRefundDialog" width="500" @update:modelValue="(val) => showRefundDialog = val">
        <v-card title="Refund">
            <v-card-text>
                Are you sure for Refund?
            </v-card-text>

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
                <v-btn text="Cancel" @click="() => showCancelDialog = false"></v-btn>
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
            cargoCode: "",
            errors: {},
            showRefundDialog: false,
            showCancelDialog: false
        }
    },
    methods: {
        toggleOrder() {
            this.show = !this.show;
        },
        async acceptOrder() {
            var response = await axios.postForm("Admin/Order/Accept", { id: this.value.id })
            if (response.isSuccess) {
                this.value = new OrderValue(response.data);
            }
            else {
                this.errors = response.data;
            }
        },
        async ignoreOrder() {
            var response = await axios.postForm("Admin/Order/Ignore", { id: this.value.id })
            if (response.isSuccess) {
                this.value = new OrderValue(response.data);
            }
            else {
                this.errors = response.data;
            }
        },
        async sendToCargo() {
            var response = await axios.postForm("Admin/Order/SendToCargo", { id: this.value.id, cargoCode: this.cargoCode })
            if (response.isSuccess) {
                this.value = new OrderValue(response.data);
            }
            else {
                this.errors = response.data;
            }
        },
        async deliverOrder() {
            var response = await axios.postForm("Admin/Order/Delivered", { id: this.value.id })
            if (response.isSuccess) {
                this.value = new OrderValue(response.data);
            }
            else {
                this.errors = response.data;
            }
        },
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
    }
}
</script>
<style scoped>
@media (max-width:992px) {
    .btn {
        width: 100%;
    }
}
</style>