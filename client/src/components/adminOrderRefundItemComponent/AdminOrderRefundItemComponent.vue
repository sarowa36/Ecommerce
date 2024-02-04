<script setup>
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import {  dateToDateTimeString } from "@/helpers/";
import { OrderRefundValue, OrderRefundStatusDescriber, OrderRefundStatus } from ".";
import { router_names } from "@/router";
import { VDialog,VCard,VBtn,VCardText,VCardActions,VSpacer } from "vuetify/components";
import TextBox from "@/components/TextBox.vue";
import axios from "axios";
defineProps({
    modelValue: {
        type: OrderRefundValue,
        req: true
    }
})
</script>
<template>
    <div :class="{ 'order_outer': true, 'active': show }">
        <div class="order_mini" @click="toggleOrder()">
            <div class="order_mini_images">
                <img v-for="imagelink in value.refundItems.map(x => x.productImage)" v-bind:key="imagelink"
                    :src="imagelink" alt="">
            </div>
            <div class="order_mini_meta">
                <span>Order number: <strong class="text_theme">{{ value.id }}</strong></span>
                <span>{{ dateToDateTimeString(value.createDate) }}</span>
            </div>
            <div class="order_mini_status">
                <FontAwesomeIcon icon="check-circle" />
                <span>{{ OrderRefundStatusDescriber(value.orderRefundStatus) }}</span>
            </div>
            <div class="order_mini_amount text_theme">
                {{ value.totalRefundAmount }} $
            </div>
            <div class="order_mini_toggle_icon">
                <FontAwesomeIcon :icon="show ? 'chevron-up' : 'chevron-down'" />
            </div>
        </div>
        <Transition name="nested">
            <div v-if="show" class="order_detail_outer">
                <div class="order_detail_inner">
                    <div class="order_products">
                        <div v-for="order_product in value.refundItems" v-bind:key="order_product.id"
                            class="order_product">
                            <div class="order_product_card_image">
                                <RouterLink class="order_product_image_outer"
                                    :to="{ name: router_names.product, params: { id: order_product.productId } }"><img
                                        :src="order_product.productImage" alt=""></RouterLink>
                            </div>
                            <div class="order_product_card_content">
                                <RouterLink class="order_product_image_outer"
                                    :to="{ name: router_names.product, params: { id: order_product.productId } }">
                                    <h6>{{ order_product.productName }}</h6>
                                </RouterLink>

                                <div class="text-success">{{ order_product.price }}$ x {{ order_product.quantity }} piece</div>
                            </div>
                        </div>
                    </div>
                    <div class="order_action_buttons" >
                        <button class="btn btn-outline-primary" v-if="value.orderRefundStatus==OrderRefundStatus.WaitingApprove" @click="()=>showApprovedDialog=true">
                            <FontAwesomeIcon icon="check" />Approve
                        </button>
                        <button class="btn btn-outline-primary" v-else-if="value.orderRefundStatus==OrderRefundStatus.ApprovedAndWaitingDelivery" @click="()=>showAcceptDialog=true">
                            <FontAwesomeIcon icon="check" />Accept
                        </button>
                        <button class="btn btn-outline-primary" v-if="value.orderRefundStatus==OrderRefundStatus.WaitingApprove||value.orderRefundStatus!=OrderRefundStatus.ApprovedAndWaitingDelivery" @click="()=>showIgnoreDialog=true">
                            <FontAwesomeIcon icon="ban" />Ignore
                        </button>
                        <button class="btn btn-outline-primary" @click="()=>showUpdateSellerMessageDialog=true">
                            <FontAwesomeIcon icon="upload" />Update Seller Message
                        </button>
                    </div>
                    <div class="row">
                       <div class="col-12 mt-4">
                        <div>
                            Status: {{ OrderRefundStatusDescriber(value.orderRefundStatus) }}
                        </div>
                        <div>
                            Refund Amount: {{ value.totalRefundAmount }} $
                        </div>
                        <div v-if="value.userMessage">
                            Customer Message: {{ value.userMessage }}
                        </div>
                        <div class="text-danger" v-if="value.sellerResponse">
                            Seller Response: {{ value.sellerResponse }}
                        </div>
                       </div>
                    </div>
                </div>
            </div>
        </Transition>
    </div>
<v-dialog :modelValue="showIgnoreDialog" width="500" @update:modelValue="(val)=>showIgnoreDialog=val">
    <v-card title="Ignore">
      <v-card-text>
        Are you sure for Ignore?
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn
          text="Close"
          @click="()=>showIgnoreDialog = false"
        ></v-btn>
        <v-btn
          text="Ignore" color="red"
          @click="ignoreRefund"
        ></v-btn>
      </v-card-actions>
    </v-card>   
</v-dialog>
<v-dialog :modelValue="showApprovedDialog" width="500" @update:modelValue="(val)=>showApprovedDialog=val">
    <v-card title="Approve">
      <v-card-text>
        Are you sure for Approve?
      </v-card-text>
      <TextBox class="pe-3 ps-4" v-model="cargoCode" placeholder="Enter Seller Message" :errorMessage="errors.cargoCode" />
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn
          text="Close"
          @click="()=>showApprovedDialog = false"
        ></v-btn>
        <v-btn
          text="Approve" color="red"
          @click="approveRefund"
        ></v-btn>
      </v-card-actions>
    </v-card>   
</v-dialog>
<v-dialog :modelValue="showAcceptDialog" width="500" @update:modelValue="(val)=>showAcceptDialog=val">
    <v-card title="Accept">
      <v-card-text>
        Are you sure for Accept?
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn
          text="Close"
          @click="()=>showAcceptDialog = false"
        ></v-btn>
        <v-btn
          text="Accept" color="red"
          @click="acceptRefund"
        ></v-btn>
      </v-card-actions>
    </v-card>   
</v-dialog>
<v-dialog :modelValue="showUpdateSellerMessageDialog" width="500" @update:modelValue="(val)=>showUpdateSellerMessageDialog=val">
    <v-card title="Update seller message">
      <TextBox class="pe-3 ps-4" v-model="sellerMessage" placeholder="Enter Seller Message" :errorMessage="errors.sellerMessage" />
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn
          text="Close"
          @click="()=>showUpdateSellerMessageDialog = false"
        ></v-btn>
        <v-btn
          text="Update" color="red"
          @click="updateSellerMessage"
        ></v-btn>
      </v-card-actions>
    </v-card>   
</v-dialog>
</template>
<script>
export default {
    data() {
        return {
            show: false,
            errors:{},
            sellerMessage:"",
            cargoCode:"",
            showIgnoreDialog:false,
            showApprovedDialog:false,
            showAcceptDialog:false,
            showUpdateSellerMessageDialog:false
        }
    },
    methods: {
        toggleOrder() {
            this.show = !this.show;
        },
        async ignoreRefund(){
            var response=await axios.postForm("Admin/OrderRefund/Ignore",{id:this.value.id});
            if(response.isSuccess){
                this.value.orderRefundStatus=OrderRefundStatus.Ignored;
            }
            this.showIgnoreDialog=false;
        },
       async approveRefund(){
            var response=await axios.postForm("Admin/OrderRefund/Approve",{id:this.value.id,cargoCode:this.cargoCode});
            if(response.isSuccess){
                this.value.orderRefundStatus=OrderRefundStatus.ApprovedAndWaitingDelivery;
            }
            this.showApprovedDialog=false;
        },
        async acceptRefund(){
            var response=await axios.postForm("Admin/OrderRefund/Accept",{id:this.value.id});
            if(response.isSuccess){
                this.value.orderRefundStatus=OrderRefundStatus.Accepted;
            }
            this.showAcceptDialog=false;
        },
        async updateSellerMessage(){
            var response=await axios.postForm("Admin/OrderRefund/UpdateSellerMessage",{id:this.value.id,message:this.sellerMessage})
            if(response.isSuccess){
                this.value.sellerResponse=this.sellerMessage;
                this.showUpdateSellerMessageDialog=false;
            }
            else{
                this.errors=response.data;
            }
        }
    },
    computed:{
        value:{
            get(){
                return this.modelValue;
            },
            set(val){
            this.$emit("update:modelValue",val);
            }
        }
    }
}
</script>
<style scoped>
.order_outer {
    border: 1px solid color-mix(in srgb, var(--first-color) 25%, transparent);
    width: 99%;
    margin: 0 auto 20px;
    transition: 0.3s;
}

.order_mini {
    display: flex;
    justify-content: space-between;
    padding: 15px;
    align-items: center;
    border-radius: 10px;
    font-size: 15px;
    flex-wrap: wrap;
    cursor: pointer;
}

.order_mini_images {
    display: flex;
}

.order_mini_images>img {
    aspect-ratio: 1;
    width: 50px;
    object-fit: cover;
    border: 3px solid #f5f5f5;
    border-radius: 50%;
}

.order_mini_images>img:not(:first-child) {
    margin-left: -20px;
}

.order_mini_meta {
    display: flex;
    flex-direction: column;
}

.order_mini_meta>span {
    color: black;
}

.order_mini_status>svg {
    margin-right: 15px;
}

.order_mini_toggle_icon>svg {
    background-color: #f5f5f5;
    padding: 10px;
    border-radius: 50%;
}

.order_detail_inner {
    padding-bottom: 20px;
}

.order_detail_inner>* {
    margin: 20px;
}

.order_product_card_content {
    display: flex;
    flex-direction: column;
    justify-content: center;
}

.order_product {
    display: flex;
    width: 100%;
    align-items: center;
    padding: 15px 30px;
    gap: 15px;
}

.order_product_card_image {
    width: 80px;
}

.order_products {
    border: 1px solid color-mix(in srgb, var(--first-color) 25%, transparent);
}

.order_outer.active {
    box-shadow: 0 8px 32px rgba(72, 72, 72, .16);
    border-color: transparent;
}

.order_action_buttons {
    display: flex;
    flex-wrap: wrap;
    gap: 15px;
    align-items: center;
}

.order_action_buttons>.btn>svg {
    margin-right: 10px;
}

@media (max-width:992px) {
    .order_mini_status {
        width: 100%;
        order: 99;
        margin-top: 10px;
    }

    .order_action_buttons>* {
        width: 100%;
    }
}

@media (max-width:576px) {
    .order_mini_meta {
        display: none;
    }
}</style>