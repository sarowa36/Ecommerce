import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import ProductListView from  "../views/ProductListView.vue"
import ProductView from "../views/ProductView.vue"
import ContactView from "../views/ContactView.vue"

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      alias:["/home"],
      name: 'home',
      component: HomeView
    },
    {
      path:"/ProductList",
      name:"productlist",
      component:ProductListView
    },
    {
      path:"/Product",
      name:"product",
      component:ProductView
    },
    {
      path:"/Contact",
      name:"contact",
      component:ContactView
    }
  ]
})

export default router
