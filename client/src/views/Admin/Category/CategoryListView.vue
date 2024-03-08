<script setup>
import { CategoryItemComponent, CategoryItemValue } from '@/components/categoryItemComponent';
import { DashboardLayout } from '@/components/dashboard';
import { VDialog, VCard, VCardText, VCardActions, VSpacer, VBtn, } from 'vuetify/components';
import axios from 'axios';
import draggable from 'vuedraggable';
</script>
<template>
    <DashboardLayout>
        <draggable v-model="values" @update:modelValue="draggableUpdate" handle=".btn-sortable" item-key="id">
            <template #item="{ element, index }">
                <CategoryItemComponent v-model="values[index]" @delete-category="showDeleteDialog(index)">
                </CategoryItemComponent>
            </template>
        </draggable>
        <div v-if="values.length==0">
        There is no category
        </div>
    </DashboardLayout>
    <v-dialog :modelValue="toggleDeleteDialog" width="500" @update:modelValue="(val) => toggleDeleteDialog = val">
        <v-card title="Cancel">
            <v-card-text>
                Are you sure for Delete?
            </v-card-text>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn text="Cancel" @click="() => toggleDeleteDialog = false"></v-btn>
                <v-btn text="Yes" color="red" @click="deleteCategory"></v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>
<script>
export default {
    data() {
        return {
            values: [new CategoryItemValue()],
            onDeleteIndex: 0,
            toggleDeleteDialog: false
        }
    },
    methods: {
        async loadValues() {
            this.values = (await axios.get("Admin/Category/GetList", { params: { id: this.$route.params.id } })).data
        },
        showDeleteDialog(index) {
            this.onDeleteIndex = index;
            this.toggleDeleteDialog = true;
        },
        deleteCategory() {
            axios.get("Admin/Category/Delete", { params: { id: this.values[this.onDeleteIndex].id } })
            this.values.splice(this.onDeleteIndex, 1)
            this.toggleDeleteDialog = false
        },
        draggableUpdate(){
            var itemsWithSortIndex={};
            this.values.forEach((item,index)=>itemsWithSortIndex[item.id]=index);
            axios.postForm("Admin/Category/UpdateSortIndex",itemsWithSortIndex)
        }
    },
    mounted() {
        this.loadValues()
    },
    watch: {
        '$route.query'(to, from) {
            this.loadValues();
        }
    },
}
</script>