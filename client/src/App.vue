<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import HelloWorld from './components/HelloWorld.vue'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
</script>

<template>
  <div class="theme_bg">
    <div class="container">
      <div class="row navbar pt-4 pb-4">
        <div class="col-3 logo">
          Salsha
        </div>
        <div class="col-6 navigation">
          <RouterLink to="/">Home</RouterLink>
          <RouterLink to="/">Shop</RouterLink>
          <RouterLink to="/">Features</RouterLink>
          <RouterLink to="/">Contact</RouterLink>
        </div>
        <div class="col-3 nav_user">
          <FontAwesomeIcon icon="bag-shopping"></FontAwesomeIcon>
          <button class="btn theme_btn">Login</button>
        </div>
      </div>
    </div>
  </div>
  <RouterView />
  <footer class="theme_bg_2 mt-5">
    <div class="container">
      <div class="row justify-content-between pt-5">
        <div class="col-3">
          <div class="logo_white">
            Salsha
          </div>
          <div class="footer_title">Social Media</div>
          <div class="social_list mt-3">
            <FontAwesomeIcon icon="fa-brands fa-instagram"></FontAwesomeIcon>
            <FontAwesomeIcon icon="fa-brands fa-square-twitter"></FontAwesomeIcon>
            <FontAwesomeIcon icon="fa-brands fa-square-facebook"></FontAwesomeIcon>
          </div>
        </div>
        <div class="col-2">
          <div class="footer_title">Shop</div>
          <div class="footer_link_list">
            <a href="#">Products</a>
            <a href="#">Overview</a>
            <a href="#">Prices</a>
            <a href="#">Releases</a>
          </div>
        </div>
        <div class="col-2">
          <div class="footer_title">Company</div>
          <div class="footer_link_list">
            <a href="#">About us</a>
            <a href="#">Contact</a>
            <a href="#">News</a>
            <a href="#">Support</a>
          </div>
        </div>
        <div class="col-4">
          <div class="footer_title">Stay up to date</div>
          <div class="mail_subscription pt-3">
            <input type="text" placeholder="Enter your email"><button>Submit</button>
          </div>
        </div>
      </div>
      <div class="row align-items-center pb-5 pt-5 footer_bottom">
        <div class="col-9 footer_bottom_border"></div>
        <div class="col-3 footer_bottom_links"> 
          <a href="#">Terms</a>
          <a href="#">Privacy</a>
          <a href="#">Cookies</a>
        </div>
      </div>
    </div>
  </footer>
</template>


<style scoped>
.footer_bottom_links > a{
    color: #FFF;
font-family: Poppins;
font-size: 18px;
font-style: normal;
font-weight: 500;
line-height: normal;
text-transform: capitalize;
    padding-right: 15px;
}
.footer_bottom_border{
    border: 1px solid #A3F3BE;
    height: 0;
}
.mail_subscription{
    display: flex;
    width: 100%;
}
.mail_subscription > input{
    width: 70%;
  background-color: transparent;
  color: white;
  padding: 9px 17px;
  border-radius: 3px  0px  0px 3px;
border: 2px solid #A3F3BE;
}
.mail_subscription > input::placeholder{
    opacity: 1;
}
.mail_subscription > button{
    width:30%;
    background: #A3F3BE;
    border: 2px solid #A3F3BE;
}
.footer_link_list{
  display: flex;
  flex-direction: column;
}
.footer_link_list > a:first-child{
  padding-top: 12px;
}
.footer_link_list > a{
  color: #C2EFD4;
font-family: Poppins;
font-size: 19px;
font-style: normal;
font-weight: 500;
line-height: normal;
text-transform: capitalize;
padding-bottom: 12px;
}
.social_list>svg {
  color: #C2EFD4;
  width: 20px;
  height: 20px;
  padding-right: 10px;
}

.footer_title {
  color: #FFF;
  font-family: Roboto Slab;
  font-size: 22px;
  font-style: normal;
  font-weight: 500;
  line-height: normal;
}

.logo {
  color: #224F34;
  font-family: Elephant;
  font-size: 33px;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
}

.logo_white {
  color: #FFF;
  font-family: Elephant;
  font-size: 50px;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
}

.navigation {
  display: flex;
  justify-content: space-evenly;
}

.navigation>a {
  color: #224F34;
  font-family: Poppins;
  font-size: 19px;
  font-style: normal;
  font-weight: 500;
  line-height: normal;
  text-transform: uppercase;
}

.nav_user {
  display: flex;
  align-items: center;
  gap: 25px;
}

.nav_user>svg {
  color: #224F34;
  width: 20px;
  height: 20px;
}

.theme_btn {
  padding-left: 25px;
  padding-right: 25px;
}</style>