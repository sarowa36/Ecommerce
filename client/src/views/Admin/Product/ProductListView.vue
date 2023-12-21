<script setup>
import axios from 'axios';
import { DashboardLayout } from '@/components/dashboard/';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
</script>
<template>
  <DashboardLayout>
    <v-data-table-server v-model:items-per-page="itemsPerPage" :headers="headers" :items-length="totalItems"
      :items="serverItems" :loading="loading" item-value="name" @update:options="loadItems">
      <template v-slot:item.image="{ item }">
        <v-card class="my-2" elevation="0" rounded>
          <v-img :src="item.image" height="64"></v-img>
        </v-card>
      </template>
      <template v-slot:item.rating="{ item }">
        <v-rating :model-value="item.rating" color="orange-darken-2" density="compact" size="small" readonly></v-rating>
      </template>
      <template v-slot:item.actions="{ item }">
        <v-btn class="ma-2">Show</v-btn>
        <v-btn class="ma-2" :to="{ name: 'productUpdate', params: { id: item.id } }">Edit</v-btn>
        <v-btn class="ma-2" color="red">Delete</v-btn>

      </template>
    </v-data-table-server>
  </DashboardLayout>
</template>
<script>
export default {
  data: () => ({
    itemsPerPage: 10,
    headers: [
      { title: "Image", key: "image", align: "center", sortable: false },
      { title: "Name", key: "name", align: "center" },
      { title: "Price", key: "price", align: "center" },
      { title: "Rating", key: "rating", align: "center", sortable: false },
      { title: "Actions", key: "actions", align: "center", sortable: false },
    ],
    serverItems: [],
    loading: true,
    totalItems: 0,
  }),
  methods: {
    loadItems({ page, itemsPerPage, sortBy }) {
      this.loading = true
      console.log(arguments)
      var params = { page, itemsPerPage, }
      if (sortBy.length > 0) {
        params.sortKey = sortBy[0].key;
        params.sortOrder = sortBy[0].order;
      }
      axios.get("Admin/Product/GetList", { params: params }).then(response => {
        this.serverItems = response.data.values
        this.totalItems = response.data.totalCount
        this.loading = false
      })
    },
  },
}
</script>