import { createRouter, createWebHistory } from 'vue-router'
import RouterMethodEnum from './RouterMethodEnum'
import HomeView from '@/views/HomeView.vue'
import ShopView from '@/views/ShopView.vue'
import ProductView from '@/views/ProductView.vue'
import ContactView from '@/views/ContactView.vue'
import LoginView from '@/views/Identity/LoginView.vue'
import ProfileView from '@/views/ProfileView.vue'
import OrdersView from '@/views/User/OrdersView.vue'
import RegisterView from '@/views/Identity/RegisterView.vue'
import CartView from '@/views/CartView.vue'
import ProductCreateUpdateView from '@/views/Admin/Product/ProductCreateUpdateView.vue'
import ProductListView from '@/views/Admin/Product/ProductListView.vue'
import AddressView from '@/views/User/AddressView.vue'
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  scrollBehavior(to, from, savedPosition) {
    return { top: 0 }
  },
  routes: [
    {
      path: '/',
      alias: ['/home'],
      name: 'home',
      component: HomeView
    },
    {
      path: '/Shop',
      name: 'shop',
      component: ShopView
    },
    {
      path: '/Product/:id',
      name: 'product',
      component: ProductView
    },
    {
      path: '/Contact',
      name: 'contact',
      component: ContactView
    },
    {
      path: '/Login',
      name: 'login',
      component: LoginView
    },
    {
      path: '/Register',
      name: 'register',
      component: RegisterView
    },
    {
      path: '/Profile',
      name: 'profile',
      component: ProfileView
    },
    {
      path: '/Orders',
      name: 'orders',
      component: OrdersView
    },
    {
      path: '/Cart',
      name: 'cart',
      component: CartView
    },
    {
      path: '/ProductCreate',
      name: 'productCreate',
      meta: {
        method: RouterMethodEnum.Create
      },
      component: ProductCreateUpdateView
    },
    {
      path: '/ProductUpdate/:id(\\d+)',
      name: 'productUpdate',
      meta: {
        method: RouterMethodEnum.Update
      },
      component: ProductCreateUpdateView
    },
    {
      path: '/ProductList',
      name: 'productList',
      component: ProductListView
    },
    {
      path:'/Address',
      name:'address',
      component:AddressView
    }
  ]
})
export { RouterMethodEnum, router }
export default router
