<script setup>
import { DashboardLayout } from "@/components/dashboard/"
import { router_names } from "@/router";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome"
</script>
<template>
    <DashboardLayout>
        <div class="row ps-3">
            <div class="col-12 text-right pt-5 pb-3">
                <button class="btn btn-outline-primary" click="CreateAddress">Create List <FontAwesomeIcon
                        class="ms-1" icon="plus"></FontAwesomeIcon></button>
            </div>
            <div class="col-md-6 savelist_item theme_border">
                <div class="savelist_top d-flex justify-content-between align-items-center">
                    <RouterLink  class="h5 savelist_title m-0" :to="{name:router_names.user_save}">Meine List</RouterLink>
                    <div class="savelist_action_buttons">
                        <button class="btn btn-outline-primary"><FontAwesomeIcon icon="pencil"></FontAwesomeIcon></button>
                        <button class="btn btn-outline-primary"><FontAwesomeIcon icon="trash"></FontAwesomeIcon></button>
                    </div>
                </div>
                <div class="savelist_products d-flex">
                    <img src="/Admin/Product/ffcab2c3-8d8f-4695-bf83-e1ce3a372e6a/seashell.webp" class="savelist_product" alt="">
                    <img src="/Admin/Product/ffcab2c3-8d8f-4695-bf83-e1ce3a372e6a/seashell.webp" class="savelist_product" alt="">
                    <img src="/Admin/Product/ffcab2c3-8d8f-4695-bf83-e1ce3a372e6a/seashell.webp" class="savelist_product" alt="">
                    <div class="savelist_product"><FontAwesomeIcon icon="bars"></FontAwesomeIcon></div>
                </div>
            </div>
        </div>
    </DashboardLayout>
</template>
<style scoped>
.savelist_action_buttons>button:first-child{
    margin-right: 6px;
}
.savelist_item{
    padding: 15px 25px;
}
.savelist_products{
    margin-top: 15px;
    display: flex;
    column-gap: 10px;
}
.savelist_product{
    width: 100%;
    aspect-ratio:1;
    border: 1px solid color-mix(in srgb, var(--first-color) 25%, transparent);
  border-radius: 5px;
}
div.savelist_product{
    display: flex;
    justify-content: center;
    align-items: center;
}
div.savelist_product >svg{
    font-size: 24px;
}
</style>
<script>
export default {
}
</script>
