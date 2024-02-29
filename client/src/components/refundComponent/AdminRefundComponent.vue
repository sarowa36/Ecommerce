<script setup>
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { OrderRefundValue, RefundLayout, OrderRefundStatus } from ".";
import { VDialog, VCard, VBtn, VCardText, VCardActions, VSpacer } from "vuetify/components";
import TextBox from "@/components/TextBox.vue";
import axios from "axios";
</script>
<template>
    <RefundLayout v-model="value">
        <template #actionButtons>
            <button class="btn btn-outline-primary" v-if="value.orderRefundStatus == OrderRefundStatus.WaitingApprove"
                @click="() => showApprovedDialog = true">
                <FontAwesomeIcon icon="check" />Approve
            </button>
            <button class="btn btn-outline-primary"
                v-else-if="value.orderRefundStatus == OrderRefundStatus.ApprovedAndWaitingDelivery"
                @click="() => showAcceptDialog = true">
                <FontAwesomeIcon icon="check" />Accept
            </button>
            <button class="btn btn-outline-primary"
                v-if="value.orderRefundStatus == OrderRefundStatus.WaitingApprove || value.orderRefundStatus == OrderRefundStatus.ApprovedAndWaitingDelivery"
                @click="() => showIgnoreDialog = true">
                <FontAwesomeIcon icon="ban" />Ignore
            </button>
            <button class="btn btn-outline-primary" @click="() => showUpdateSellerMessageDialog = true">
                <FontAwesomeIcon icon="upload" />Update Seller Message
            </button>
        </template>
    </RefundLayout>

    <v-dialog :modelValue="showIgnoreDialog" width="500" @update:modelValue="(val) => showIgnoreDialog = val">
        <v-card title="Ignore">
            <v-card-text>
                Are you sure for Ignore?
            </v-card-text>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn text="Close" @click="() => showIgnoreDialog = false"></v-btn>
                <v-btn text="Ignore" color="red" @click="ignoreRefund"></v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
    <v-dialog :modelValue="showApprovedDialog" width="500" @update:modelValue="(val) => showApprovedDialog = val">
        <v-card title="Approve">
            <v-card-text>
                Are you sure for Approve?
            </v-card-text>
            <TextBox class="pe-3 ps-4" v-model="cargoCode" placeholder="Enter Cargo Code"
                :errorMessage="errors.cargoCode" />
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn text="Close" @click="() => showApprovedDialog = false"></v-btn>
                <v-btn text="Approve" color="red" @click="approveRefund"></v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
    <v-dialog :modelValue="showAcceptDialog" width="500" @update:modelValue="(val) => showAcceptDialog = val">
        <v-card title="Accept">
            <v-card-text>
                Are you sure for Accept?
            </v-card-text>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn text="Close" @click="() => showAcceptDialog = false"></v-btn>
                <v-btn text="Accept" color="red" @click="acceptRefund"></v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
    <v-dialog :modelValue="showUpdateSellerMessageDialog" width="500"
        @update:modelValue="(val) => showUpdateSellerMessageDialog = val">
        <v-card title="Update seller message">
            <TextBox class="pe-3 ps-4" v-model="sellerMessage" placeholder="Enter Seller Message"
                :errorMessage="errors.sellerMessage" />
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn text="Close" @click="() => showUpdateSellerMessageDialog = false"></v-btn>
                <v-btn text="Update" color="red" @click="updateSellerMessage"></v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>
<script>
export default {
    data() {
        return {
            show: false,
            errors: {},
            sellerMessage: "",
            cargoCode: "",
            showIgnoreDialog: false,
            showApprovedDialog: false,
            showAcceptDialog: false,
            showUpdateSellerMessageDialog: false
        }
    },
    methods: {
        toggleOrder() {
            this.show = !this.show;
        },
        async ignoreRefund() {
            var response = await axios.postForm("Admin/OrderRefund/Ignore", { id: this.value.id });
            if (response.isSuccess) {
                this.value.orderRefundStatus = OrderRefundStatus.Ignored;
                this.showIgnoreDialog = false;
            }
            else {
                this.errors = response.data;
            }
        },
        async approveRefund() {
            var response = await axios.postForm("Admin/OrderRefund/Approve", { id: this.value.id, cargoCode: this.cargoCode });
            if (response.isSuccess) {
                this.value.orderRefundStatus = OrderRefundStatus.ApprovedAndWaitingDelivery;
                this.showApprovedDialog = false;
            }
            else {
                this.errors = response.data;
            }
        },
        async acceptRefund() {
            var response = await axios.postForm("Admin/OrderRefund/Accept", { id: this.value.id });
            if (response.isSuccess) {
                this.value.orderRefundStatus = OrderRefundStatus.Accepted;
                this.showAcceptDialog = false;
            }
            else {
                this.errors = response.data;
            }
        },
        async updateSellerMessage() {
            var response = await axios.postForm("Admin/OrderRefund/UpdateSellerMessage", { id: this.value.id, message: this.sellerMessage })
            if (response.isSuccess) {
                this.value.sellerResponse = this.sellerMessage;
                this.showUpdateSellerMessageDialog = false;
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
    props: {
        modelValue: {
            type: OrderRefundValue,
            req: true
        }
    }
}
</script>