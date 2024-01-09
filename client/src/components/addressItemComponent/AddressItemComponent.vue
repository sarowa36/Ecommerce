<script setup>
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import TextBox from "@/components/TextBox.vue";
import { VSelect } from "vuetify/components";
import { AddressItemValue } from "./"
import { useCitiesAndDistrictsStore } from "@/stores/CitiesAndDistrictsStore";
import axios from "axios";
defineProps({
    modelValue: {
        type: AddressItemValue,
        req: true
    }
})
</script>
<template>
    <div :class="{ 'address_outer': true, 'active': show }">
        <div class="address_mini" @click="() => show = !show">
            <div class="address_mini_amount text_theme">
                {{ value.name || "Name Of Address" }}
            </div>
            <div class="address_mini_toggle_icon">
                <FontAwesomeIcon :icon="show ? 'chevron-up' : 'chevron-down'" />
            </div>
        </div>
        <Transition name="nested">
            <div v-if="show" class="row">
                <TextBox placeholder="Address Name" v-model="value.name" :errorMessage="errors.name" />
                <v-select label="Select City" v-model="value.cityId" :items="citiesAndDistricts.values" item-title="name"
                    item-value="id" @update:modelValue="cityUpdate" :error-messages="errors.cityId"></v-select>
                <v-select label="Select District" v-model="value.districtId"
                    :items="value.cityId ? citiesAndDistricts.values[value.cityId - 1].districts : []" item-title="name"
                    item-value="id" :disabled="value.cityId == null" :error-messages="errors.districtId"></v-select>
                <TextBox placeholder="Address Zip" v-model="value.zip" :errorMessage="errors.zip" />
                <TextBox placeholder="Address Detail" v-model="value.detail" :errorMessage="errors.detail" />
                <div class="text-right pb-4">
                <button class="btn btn-primary" @click="save">Save</button>
            </div>
            </div>
        </Transition>
    </div>
</template>
<script>
export default {
    data() {
        return {
            _show: false,
            citiesAndDistricts: useCitiesAndDistrictsStore(),
            errors: {}
        }
    },
    methods: {
        cityUpdate(val) {
            this.value.districtId = null;
            this.value.cityId = val;
        },
        async save() {
            this.errors = {};
            var requestUrl="";
            if (this.value.id != null)
                requestUrl = "User/Address/Update";
            else
                requestUrl = "User/Address/Create";
            var response = await axios.postForm(requestUrl, this.value)
            if (response.isSuccess) {
                if(this.value.id==null)
                this.value.id = response.data.id;
                this._show=false;
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
                this.$emit("update:modelValue", val)
            }
        },
        show: {
            get() {
                return this._show || !this.value.id;
            },
            set(val) {
                this._show = val;
            }
        }
    }
}
</script>
<style scoped>
.address_outer {
    border: 1px solid color-mix(in srgb, var(--first-color) 25%, transparent);
    width: 99%;
    margin: 0 auto 20px;
    transition: 0.3s;
}

.address_mini {
    display: flex;
    justify-content: space-between;
    padding: 15px;
    align-items: center;
    border-radius: 10px;
    font-size: 15px;
    flex-wrap: wrap;
    cursor: pointer;
}

.address_mini_toggle_icon>svg {
    background-color: #f5f5f5;
    padding: 10px;
    border-radius: 50%;
}



.address_outer.active {
    box-shadow: 0 8px 32px rgba(72, 72, 72, .16);
    border-color: transparent;
}
</style>