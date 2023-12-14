<script setup>
import { DashboardLayout } from '@/components/dashboard/'
import { FileUploadValueArray, FileUploadComponent } from '@/components/fileUploadComponent'
import axios from 'axios'
</script>
<template>
  <DashboardLayout>
    <div class="row pt-5">
      <h3 class="text_theme">Product Images</h3>
      <FileUploadComponent v-model="images" />
      <button class="btn btn-primary" @click="request">Send</button>
    </div>
  </DashboardLayout>
</template>
<script>
export default {
  data() {
    return {
      images: new FileUploadValueArray()
    }
  },
  methods: {
    async request() {
      var data = new FormData()
      this.images.AppendFilesToFormData("images",data);
      console.log(data)
      await axios.postForm('/Admin/Product/Create', data)
    }
  }
}
</script>
