<script setup>
import { RouterMethodEnum } from '@/router';
import { DashboardLayout } from '@/components/dashboard/'
import { FileUploadValueArray, FileUploadComponent, FileUploadValue } from '@/components/fileUploadComponent'
import { ckeditor, ClassicEditor } from "@/components/ckeditorComponent";
import TextBox from '@/components/TextBox.vue';
import axios from 'axios'
</script>
<template>
  <DashboardLayout class="dashboard_layout">
    <div class="row pt-5">
      <h2 class="text_theme mb-3">Product {{ $route.meta.method }}</h2>
      <div class="col-6">
        <TextBox placeholder="Title" v-model="formValues.name" :errorMessage="errors.name" />
      </div>
      <div class="col-6">
        <TextBox placeholder="Price" v-model="formValues.price" :errorMessage="errors.price" />
      </div>
      <h3 class="text_theme mt-3">Product Description</h3>
      <div class="mb-4">
        <ckeditor :editor="ClassicEditor" v-model="formValues.description" rows="150"></ckeditor>
        <span v-if="errors.description" class="text-danger white-space-pre-line">{{ errors.description }}</span>
      </div>
      <h3 class="text_theme">Product Images</h3>
      <FileUploadComponent v-model="images" />
      <span v-if="errors.images" class="text-danger white-space-pre-line">{{ errors.images }}</span>
      <button class="btn btn-primary mt-3" @click="request">Send</button>

    </div>
  </DashboardLayout>
</template>
<style scoped>
.dashboard_layout :deep(.ck-editor__editable) {
  height: 300px;
}
</style>
<script>
export default {
  data() {
    return {
      images: new FileUploadValueArray(),
      formValues: {
        name: "",
        price: "",
        description: "",
      },
      errors:{}
    }
  },
  async mounted() {
    if (this.$route.meta.method == RouterMethodEnum.Update) {
      var val = (await axios.get("Admin/Product/Update", { params: { id: this.$route.params.id } })).data;
      Object.keys(val).forEach(key => key != "images" ? this.formValues[key] = val[key] : null)
      val.images.forEach(img => this.images.push(new FileUploadValue({ link: img, isUploaded: true })))
    }
  },
  methods: {
    async request() {
      var data = new FormData()
      Object.keys(this.formValues).forEach(key => {
        data.append(key, this.formValues[key])
      })
      this.images.AppendFilesToFormData("images", data);
      var response = null;
      switch (this.$route.meta.method) {
        case RouterMethodEnum.Create:
          response = await axios.postForm('Admin/Product/Create', data);
          break;
        case RouterMethodEnum.Update:
          response = await axios.postForm('Admin/Product/Update', data);
          break;
      }
      if(response!=null && response.status!=200){
        this.errors=response.data;
      }
    }
  }
}
</script>
