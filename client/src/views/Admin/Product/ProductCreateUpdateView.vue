<script setup>
import { DashboardLayout } from "@/components/dashboard/"
import { FileUploadComponent } from "@/components/fileUploadComponent";
</script>
<template>
    <DashboardLayout>
        <div class="row pt-5">
            <h3 class="text_theme">Product Images</h3>
            <FileUploadComponent v-model="images" />
        </div>
    </DashboardLayout>
</template>
<script>
export default {
    data(){
        return {
            images:[]
        }
    }
}
</script>
