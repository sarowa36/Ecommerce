<script setup>
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { OrderRefundValue, RefundLayout, OrderRefundStatus } from ".";
import { VDialog, VCard, VBtn, VCardText, VCardActions, VSpacer } from "vuetify/components";
import axios from "axios";
</script>
<template>
    <RefundLayout v-model="value">
        <template #actionButtons>
            <button class="btn btn-outline-primary" v-if="value.orderRefundStatus == OrderRefundStatus.WaitingApprove"
                @click="() => showCancelDialog = true">
                <FontAwesomeIcon icon="ban" />Cancel
            </button>
        </template>
    </RefundLayout>
    <v-dialog :modelValue="showCancelDialog" width="500" @update:modelValue="(val) => showCancelDialog = val">
        <v-card title="Cancel">
            <v-card-text>
                Are you sure for Cancel?
            </v-card-text>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn text="Close" @click="() => showCancelDialog = false"></v-btn>
                <v-btn text="Cancel Refund" color="red" @click="cancelRefund"></v-btn>
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
            showCancelDialog: false
        }
    },
    methods: {
        toggleOrder() {
            this.show = !this.show;
        },
        async cancelRefund() {
            var response = await axios.postForm("User/OrderRefund/CancelRefund", { id: this.value.id });
            if (response.isSuccess) {
                this.value.orderRefundStatus = OrderRefundStatus.Canceled;
            }
            this.showCancelDialog = false;
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