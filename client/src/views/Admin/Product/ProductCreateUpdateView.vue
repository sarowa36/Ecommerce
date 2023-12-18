<script setup>
import { DashboardLayout } from '@/components/dashboard/'
import { FileUploadValueArray, FileUploadComponent } from '@/components/fileUploadComponent'
import { ckeditor, ClassicEditor } from "@/components/ckeditorComponent";
import TextBox from '@/components/TextBox.vue';
import axios from 'axios'
</script>
<template>
  <DashboardLayout class="dashboard_layout">
    <div class="row pt-5">
      <h2 class="text_theme mb-3">Product Create</h2>
      <div class="col-6">
        <TextBox placeholder="Title" v-model="formValues.name" />
      </div>
      <div class="col-6">
        <TextBox placeholder="Price" v-model="formValues.price"/>
      </div>
      <h3 class="text_theme mt-3">Product Description</h3>
      <div class="mb-4">
      <ckeditor :editor="ClassicEditor" v-model="formValues.description" rows="150"></ckeditor></div>
      <h3 class="text_theme">Product Images</h3>
      <FileUploadComponent v-model="images" />
      <button class="btn btn-primary mt-3" @click="request">Send</button>
      
    </div>
  </DashboardLayout>
</template>
<style scoped>
.dashboard_layout  :deep(.ck-editor__editable ){
    height: 300px;
}
</style>
<script>
export default {
  data() {
    return {
      images: new FileUploadValueArray(),
      formValues:{
        name:"",
        price:"",
        description: "",
      },
      editorConfig:{

      }
    }
  },
  methods: {
    async request() {
      var data = new FormData()
      Object.keys(this.formValues).forEach(key=>{
        data.append(key,this.formValues[key])
      })
      this.images.AppendFilesToFormData("images", data);
      console.log(data)
      await axios.postForm('/Admin/Product/Create', data)
    }
  }
}
</script>
