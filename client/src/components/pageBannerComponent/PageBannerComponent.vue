<script setup>
defineProps({
  img:{
    type:String,
    default:"/src/assets/img/123.svg"
  },
  title:String,
  breadcrumbList:[]
})
</script>
<template>
    <section class="text-center position-relative">
      <img class="background_image" :src="img" alt="">
      <div class="banner_text_container container-fluid">
        <div class="row pt-5 pb-5">
          <div class="col-12">
            <span class="pagetitle__subheading"></span>
            <span class="h1 text_theme">{{ title }}</span>
            <nav aria-label="breadcrumb" v-if="breadcrumbList && breadcrumbList.length>0">
              <ol class="breadcrumb justify-content-center">
                <li v-for="(item,index) in breadcrumbList" :key="index" class="breadcrumb-item" :class="{'active':breadcrumbList.length-1==index}">
                  <RouterLink v-if="breadcrumbList.length-1!=index" :to="item.link">{{ item.text }}</RouterLink>
                  <template v-else>{{ item.text }}</template>
                </li>
                
              </ol>
            </nav>
          </div>
        </div>
      </div>
    </section>
</template>
<style scoped>
.background_image{
  position: absolute;
  width: 100%;
  height: 100%;
  left: 0;
  z-index: -1;
  object-fit: cover;
}
.banner_text_container{
    background-color: color-mix(in srgb, var(--fifth-color) 55%, transparent);
}
</style>