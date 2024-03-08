<script setup>
import { router_names } from "@/router";
import { CategoryItemValue } from ".";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
defineProps({
    modelValue: CategoryItemValue
})
defineEmits(["deleteCategory"])
</script>
<template>
    <div class="card mb-4">
        <div class="card-body d-flex justify-content-between align-center">
            <div class="category_name">{{ modelValue.name }}</div>
            <div class="category_buttons d-flex gap-2">
                <RouterLink class="btn btn-dark"
                    :to="{ name: router_names.admin_category_list, params: { id: modelValue.id } }">Alt Kategori({{
                modelValue.childrens.length }})</RouterLink>
                <button class="btn btn-sortable btn-dark">
                    <FontAwesomeIcon icon="minimize" />
                </button>

                <RouterLink :to="{ name: router_names.admin_category_update, params: { id: modelValue.id } }"
                    class="btn btn-dark">
                    <FontAwesomeIcon icon="pen-to-square" />
                </RouterLink>

                <button class="btn btn-dark" @click="$emit('deleteCategory')">
                    <FontAwesomeIcon icon="trash" />
                </button>
            </div>
        </div>
    </div>
</template>
<style scoped>
.btn-sortable {
    cursor: move;
}
</style>