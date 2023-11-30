<script setup>
import { RouterLink, RouterView } from 'vue-router'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { useLoginStore } from '../../stores/LoginStore';
</script>
<template>  
<div class="theme_bg">
    <div class="container">
      <div class="row navbar pt-4 pb-4">
        <div class="col-3 col-lg-4 logo">
          Salsha
        </div>
        <div class="col-4 d-flex d-lg-none justify-content-end">
          <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navigation"
            aria-controls="navigation" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
          </button>
        </div>
        <div :class="'col-lg-8' + ($screen.width > 992 ? '' : ' collapse')" id="navigation">
          <div class="navigation">
            <RouterLink to="/">Home</RouterLink>
            <RouterLink to="/ProductList">Shop</RouterLink>
            <div class="nav_link_with_dropdown nav_links">
            <RouterLink to="/">Features</RouterLink> <button class="btn" @click="collapseSubmenu"><FontAwesomeIcon icon="chevron-down" /></button>
            <div class="nav_submenu_outer">
              <div class="nav_submenu_inner">
                <div class="nav_submenu_links">
                  <RouterLink to="/Orders">Orders</RouterLink>
                  <RouterLink to="/Register">Register</RouterLink>
                </div>
              </div>
            </div>
          </div>
            <RouterLink to="/Contact">Contact</RouterLink>
            <div class="nav_link_with_dropdown nav_cart">
              <RouterLink to="/">
                <FontAwesomeIcon v-if="$screen.width > 992" icon="bag-shopping"></FontAwesomeIcon><span v-else>Cart</span>
              </RouterLink>
              <div v-if="$screen.width > 992" class="nav_submenu_outer nav_cart_dropdown">
                <div class="nav_submenu_inner">
                <h6 class="nav_submenu_title">Cart (1)</h6>
                <div class="nav_cart_products">
                    <a class="nav_cart_product" href="#">
                      <img src="http://img.sarowa36.com.tr/woman1.png" alt="">
                      <div class="nav_cart_product_info">
                        <span class="nav_cart_product_title">Piece Of Beauty</span>
                        <span class="nav_cart_product_detail">Size: XS</span>
                        <span class="nav_cart_product_price">1800,00 $</span>
                      </div>
                    </a>
                    <a class="nav_cart_product" href="#">
                      <img src="http://img.sarowa36.com.tr/woman1.png" alt="">
                      <div class="nav_cart_product_info">
                        <span class="nav_cart_product_title">Piece Of Beauty</span>
                        <span class="nav_cart_product_detail">Size: XS</span>
                        <span class="nav_cart_product_price">1800,00 $</span>
                      </div>
                    </a>
                </div>
                <div class="nav_submenu_buttons">
                  <RouterLink to="/" class="btn btn-primary">Cart</RouterLink><RouterLink to="/" class="btn btn-secondary">Order</RouterLink>
                </div>
              </div>
              </div>
            </div>
            <RouterLink to="/Login" v-if="!loginStore.isLogged" :class="$screen.width > 992 ? 'btn btn-primary btn_login' : ''">
              Login</RouterLink>
            <RouterLink to="/Profile" v-else>Profile</RouterLink>
          </div>
        </div>
      </div>
    </div>
  </div>
  </template>
  <style scoped>
  .navigation {
  display: flex;
  justify-content: space-evenly;
  align-items: center;
  gap: 25px;
  z-index: 9;
}

.navigation>a:not(:hover) {
  color: var(--first-color);
}

.navigation :where( a, div.nav_link_with_dropdown > a){
  font-family: var(--first-font);
  font-size: 19px;
  font-style: normal;
  font-weight: 500;
  line-height: normal;
  text-transform: uppercase;
}
  /* NavSubmenu */
.nav_link_with_dropdown{
    position: relative;
}
.nav_submenu_inner{
  padding: 15px;
    background-color: white;
    width: 100%;
    border: 1px solid #e6e6e6;
    box-shadow: 0 3px 10px 0 rgba(0, 0, 0, 0.07);
}
@media (min-width: 992px) {
    .nav_submenu_outer{
    position: absolute;
    width: 300px;
    right: calc(-50% - 60px);
    z-index: 999;
    opacity: 0;
    visibility: hidden;
    padding-top: 15px;
}

.nav_submenu_inner,.nav_submenu_outer{
  transition: .2s linear .4s;
}
.nav_link_with_dropdown:hover > .nav_submenu_outer{
  opacity: 1;
  visibility: visible;
}
.nav_link_with_dropdown:hover :where(.nav_submenu_outer, .nav_submenu_inner){
  transition: .2s linear 0s;
}
}
.nav_submenu_title{
    padding: 4px 0 8px;
}
.nav_submenu_buttons{
    margin-top: 10px;
    border-top: 1px solid #e6e6e6;
    padding-top: 10px;
    display: flex;
    justify-content: space-between;
    column-gap: 15px;
}
/* NavCart */
.nav_cart_dropdown{
    right: -40px;
}
.nav_cart_product{
    display: flex;
}
.nav_cart_product:not(:first-child){
    margin-top: 10px;
}
.nav_cart_product>img{
    width: 55px;
    margin-right: 15px;
    border-radius: 3px;
}
.nav_cart_product_info{
    display: flex;  
    flex-direction: column;
    justify-content: space-between;
}
.nav_cart_product_detail{
    opacity: .6;
}
.nav_cart_product_price{
    color: #ffa900;
}
.nav_cart .nav_submenu_buttons>*{
width: 50%;
}
/* End NavSubmenu */
/* NavSubmenuLinks */
.nav_submenu_links>a{
    display: block;
    padding: 8px 0;
}
.nav_submenu_links>a:not(:first-child){
  border-top: 1px solid #e6e6e6;
}
.nav_links>.btn{
    display: none;
}
@media (max-width:992px){
    .nav_links{
    display: flex;
    flex-wrap: wrap;
    align-items: center;
}
.nav_links>a:first-child{
    width: 90%;
    height: 100%;
    
}
.nav_links>.btn{
    display: initial;
    width: 10%;
    text-align: center;
    border: 1px solid #e6e6e6;
    padding: 5px;
    font-size: 16px;
}
   .nav_links > .nav_submenu_outer{
    width: 100%;
       transition: 0.3s linear;
    transform: scaleY(0);
    transform-origin: top;
       position: absolute;
       top:100%;
       margin-top: 10px;
    }
    .nav_links.active >.nav_submenu_outer{
        position: initial;
        transform: scale(1);
    }
    
}
/* End NavSubmenuLinks */
.btn_login {
  padding-left: 25px;
  padding-right: 25px;
}
@media (max-width:992px) {
  .navigation {
    position: absolute;
    flex-direction: column;
    margin-top: 15px;
    width: 100%;
    background-color: white;
    padding: 15px;
  }

  .navigation > * {
    width: 100%;
    text-align: left;
  }

  #navigation {
    position: relative;
  }
}
</style>
<script>
export default {
  data() {
    return {
      loginStore: useLoginStore()
    }
  },
  methods:{
    collapseSubmenu(e){
        e.currentTarget.parentElement.classList.toggle("active");
    }
  }
}
</script>