import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import ProductListView from  "../views/ProductListView.vue"
import ProductView from "../views/ProductView.vue"
import ContactView from "../views/ContactView.vue"
import LoginView from "../views/Identity/LoginView.vue"
import ProfileView from "../views/ProfileView.vue"
import OrdersView from "../views/OrdersView.vue"
import RegisterView from "../views/Identity/RegisterView.vue"
import CartView from '../views/CartView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  scrollBehavior(to,from,savedPosition){
    return{top:0}
  },
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
    },
    {
      path:"/Login",
      name:"login",
      component:LoginView
    },
    {
      path:"/Register",
      name:"register",
      component:RegisterView
    },
    {
      path:"/Profile",
      name:"profile",
      component:ProfileView 
    }, 
    {
      path:"/Orders",
      name:"orders",
      component:OrdersView
    }, {
      path:"/Cart",
      name:"cart",
      component:CartView
    },
  ]
})

export default router
