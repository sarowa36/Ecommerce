<script setup>
import { DashboardLayout } from "@/components/dashboard"
import { AddressItemComponent, AddressItemValue } from "@/components/addressItemComponent";
import axios from "axios";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
</script>
<template>
    <DashboardLayout>
        <div class="row">
            <div class="col-12 text-right pt-5 pb-5">
                <button class="btn btn-outline-primary" @click="CreateAddress">Create Address <FontAwesomeIcon class="ms-1"
                        icon="plus"></FontAwesomeIcon></button>
            </div>
            <AddressItemComponent v-for="(item, index) in values" :key="item.id" v-model="values[index]" />
        </div>
    </DashboardLayout>
</template>
<script>
export default {
    data() {
        return {
            values: [new AddressItemValue()]
        }
    },
    async mounted() {
        this.values = [];
        var response = await axios.get("User/Address/GetList");
        if (response.isSuccess) {
            response.data.forEach(element => {
                this.values.push(new AddressItemValue(element))
            });
        }

    },
    methods: {
        async CreateAddress() {
            if (!this.values.some(item => !item.id))
                this.values.unshift(new AddressItemValue())
        }
    }
}
</script>

