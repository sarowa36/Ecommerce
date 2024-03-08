<script setup>
import DashboardLayout from '@/components/dashboard/DashboardLayout.vue';
import TextBox from '@/components/TextBox.vue';
import router, { router_names, RouterMethodEnum } from '@/router';
import axios from 'axios';
import { VCombobox } from 'vuetify/components';
</script>
<template>
    <DashboardLayout>
        <div class="row">
            <div class="col-12">
                <TextBox placeholder="Category Name" v-model="formValue.name" :error-message="errors.name" />
            </div>
            <div class="col-12">
                <VCombobox v-model:model-value="formValue.parentId" :items="categories" item-title="name"
                    item-value="id" :return-object="false" placeholder="please select parent category"></VCombobox>
            </div>
            <button class="btn btn-primary" @click="sendRequest">Save</button>
        </div>
    </DashboardLayout>
</template>
<script>
export default {
    data() {
        return {
            formValue: {
                id: 0,
                name: "",
                parentId: undefined
            },
            errors: {},
            categories: []
        }
    },
    methods: {
        async loadvalues() {
            if (this.$route.meta.method == RouterMethodEnum.Create)
                this.categories = (await axios.get("Admin/Category/GetlistWithParentNames")).data;
            else {
                this.categories = (await axios.get("Admin/Category/GetlistWithParentNames", { params: { id: this.$route.params.id } })).data;
                this.formValue = (await axios.get("Admin/Category/Update", { params: { id: this.$route.params.id } })).data;
            }
            this.categories.unshift({id:null,name:"Select as main category"})
        },
        async sendRequest() {
            var response;
            if (this.$route.meta.method == RouterMethodEnum.Create)
                response = await axios.postForm("Admin/Category/Create", this.formValue);
            else
                response = await axios.postForm("Admin/Category/Update", this.formValue);
            if (response.isSuccess) {
                this.$router.push({ name: router_names.admin_category_list })
            }
            else {
                this.errors = response.data
            }
        }
    },
    mounted() {
        this.loadvalues();
    },
}
</script>