<template>
    <DashboardLayout>
        <ul v-for="item in values" :key="item.id">
            <li>{{ item.name }}</li>
            <li>
                <RouterLink :to="'/ProductUpdate/' + item.id">Edit</RouterLink>
            </li>
        </ul>
    </DashboardLayout>
</template>
<script>
import axios from 'axios';
import {DashboardLayout} from '@/components/dashboard/';

export default {
    data() {
        return {
            values: []
        };
    },
    mounted() {
        axios.get("Admin/Product/GetList").then(x => this.values = x.data);
    },
    components: { DashboardLayout }
}
</script>