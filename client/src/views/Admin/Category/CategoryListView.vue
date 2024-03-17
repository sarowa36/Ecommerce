<script setup>
    import { CategoryItemComponent, CategoryItemValue } from '@/components/categoryItemComponent';
    import { DashboardLayout } from '@/components/dashboard';
    import { VDialog, VCard, VCardText, VCardActions, VSpacer, VBtn, } from 'vuetify/components';
    import { useCategoryStore } from '@/stores/CategoryStore';
    import axios from 'axios';
    import draggable from 'vuedraggable';
</script>
<template>
    <DashboardLayout>
        <draggable v-if="categories" v-model="categories" @update:modelValue="draggableUpdate" handle=".btn-sortable"
            item-key="id">
            <template #item="{ element, index }">
                <CategoryItemComponent v-model="categories[index]" @delete-category="showDeleteDialog(element.id)">
                </CategoryItemComponent>
            </template>
        </draggable>
        <div v-if="categories.length == 0">
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
                onDeleteId: 0,
                toggleDeleteDialog: false,
                categoryStore: useCategoryStore()
            }
        },
        methods: {
            showDeleteDialog(id) {
                this.onDeleteId = id;
                this.toggleDeleteDialog = true;
            },
            deleteCategory() {
                axios.get("Admin/Category/Delete", { params: { id: this.onDeleteId } })
                this.categoryStore.load();
                this.toggleDeleteDialog = false
            },
            async draggableUpdate(ary) {
                console.log(arguments)
                var itemsWithSortIndex = {};
                ary.forEach((item, index) => itemsWithSortIndex[item.id] = index);
                await axios.postForm("Admin/Category/UpdateSortIndex", itemsWithSortIndex)
                this.categoryStore.load();
            }
        },
        computed: {
            categories() {
                var id = parseInt(this.$route.params.id);
                return id ? this.categoryStore.get(id).childrens : this.categoryStore.values.filter(x => x.parentId == null);
            }
        },
    }
</script>