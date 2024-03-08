<script setup>
import axios from 'axios';
import { DashboardLayout } from '@/components/dashboard/';
import { router_names } from '@/router';
import { VDialog,VCard,VCardText,VCardActions,VSpacer,VBtn, } from 'vuetify/components';
</script>
<template>
  <DashboardLayout>
    <v-data-table-server v-model:items-per-page="itemsPerPage" :headers="headers" :items-length="totalItems"
      :items="serverItems" :loading="isLoading" item-value="name" @update:options="loadItems" :page="page">
      <template v-slot:item.image="{ item }">
        <v-card class="my-2" elevation="0" rounded>
          <v-img :src="item.image" height="64"></v-img>
        </v-card>
      </template>
      <template v-slot:item.rating="{ item }">
        <v-rating :model-value="item.rating" color="orange-darken-2" density="compact" size="small" readonly></v-rating>
      </template>
      <template v-slot:item.actions="{ item,index }">
        <v-btn class="ma-2" :to="{ name: router_names.product, params: { id: item.id } }" target="_blank">Show</v-btn>
        <v-btn class="ma-2" :to="{ name: router_names.admin_product_update, params: { id: item.id } }">Edit</v-btn>
        <v-btn class="ma-2" color="red" @click="showDeleteDialog(index)">Delete</v-btn>
      </template>
    </v-data-table-server>
  </DashboardLayout>
  <v-dialog :modelValue="toggleDeleteDialog" width="500" @update:modelValue="(val) => toggleDeleteDialog = val">
        <v-card title="Cancel">
            <v-card-text>
                Are you sure for Delete?
            </v-card-text>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn text="Cancel" @click="() => toggleDeleteDialog = false"></v-btn>
                <v-btn text="Yes" color="red" @click="deleteProduct"></v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>
<script>
export default {
  data: () => ({
    initiated: false,
    itemsPerPage: 10,
    headers: [
      { title: "Image", key: "image", align: "center", sortable: false },
      { title: "Name", key: "name", align: "center" },
      { title: "Price", key: "price", align: "center" },
      { title: "Rating", key: "rating", align: "center", sortable: false },
      { title: "Actions", key: "actions", align: "center", sortable: false },
    ],
    serverItems: [],
    totalItems: 0,
    page: 1,
    toggleDeleteDialog:false,
    onDeleteIndex:undefined
  }),
  methods: {
    loadItems({ page, itemsPerPage, sortBy }) {
      if (this.initiated) {
        var query = { page, itemsPerPage };
        if (sortBy.length > 0) {
          query.sortByKey = sortBy[0].key
          query.sortByOrder = sortBy[0].order
        }
        this.$router.push({ query })
      }
      else {
        var queryParams = this.$route.query;
        this.request(queryParams.page, queryParams.itemsPerPage, queryParams.sortByKey, queryParams.sortByOrder);
        this.initiated = true;
      }

    },
    request(page = 1, itemsPerPage = this.itemsPerPage, sortByKey, sortByOrder) {
      var params = { page, itemsPerPage }
      if (sortByKey != null && sortByKey.length > 0) {
        params.sortKey = sortByKey;
        params.sortOrder = sortByOrder;
      }
      axios.get("Admin/Product/GetList", { params: params }).then(response => {
        if (response.isSuccess) {
          this.serverItems = response.data.values;
          this.totalItems = response.data.totalCount;
        }
        this.page = page
      })
    },
    showDeleteDialog(index){
      this.onDeleteIndex=index;
      this.toggleDeleteDialog=true;
    },
    deleteProduct(){
      axios.get("Admin/Product/Delete",{params:{id:this.serverItems[this.onDeleteIndex].id}})  
      this.serverItems.splice(this.onDeleteIndex,1)
            this.toggleDeleteDialog=false
    }
  },
  watch: {
    '$route.query'(to, from) {
      var { page, itemsPerPage, sortByKey, sortByOrder } = to;
      this.request(page, itemsPerPage, sortByKey, sortByOrder);
    }
  },
}
</script>