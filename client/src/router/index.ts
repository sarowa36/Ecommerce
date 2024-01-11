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

class _router_names {
  home:string= "home";
  shop: string = "shop";
  product: string = "product";
  contact: string = "contact";
  login: string = "login";
  register: string = "register";
  admin:string="admin";
  admin_product_create:string="admin_product_create";
  admin_product_update:string="admin_product_update";
  admin_product_list:string="admin_product_list";
  user:string="user";
  user_orders:string="user_orders";
  user_profile:string="user_profile";
  user_cart:string="user_cart";
  user_address:string="user_address";
}
const router_names = new _router_names();

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
      name: router_names.shop,
      component: ShopView
    },
    {
      path: '/Product/:id',
      name: router_names.product,
      component: ProductView
    },
    {
      path: '/Contact',
      name: router_names.contact,
      component: ContactView
    },
    {
      path: '/Login',
      name: router_names.login,
      component: LoginView
    },
    {
      path: '/Register',
      name: router_names.register,
      component: RegisterView
    },
    {
      path: '/Admin',
      name: router_names.admin,
      children: [
        {
          path: "Product",
          children: [
            {
              path: 'Create',
              name: router_names.admin_product_create,
              meta: {
                method: RouterMethodEnum.Create
              },
              component: ProductCreateUpdateView
            },
            {
              path: 'Update/:id(\\d+)',
              name: router_names.admin_product_update,
              meta: {
                method: RouterMethodEnum.Update
              },
              component: ProductCreateUpdateView
            },
            {
              path: 'List',
              name: router_names.admin_product_list,
              component: ProductListView
            }
          ]
        },
      ],
    },
    {
      path: '/User',
      name: router_names.user,
      children: [
        {
          path: 'Cart',
          name: router_names.user_cart,
          component: CartView
        },
        {
          path: 'Profile',
          name: router_names.user_profile,
          component: ProfileView
        },
        {
          path: 'Orders',
          name: router_names.user_orders,
          component: OrdersView
        },
        {
          path: 'Address',
          name: router_names.user_address,
          component: AddressView
        },
      ]
    },

  ]
})
export { RouterMethodEnum, router, router_names }
export default router