<script setup>
import { RouterLink, RouterView } from 'vue-router'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { useLoginStore } from './stores/LoginStore';
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
        <div :class="'col-lg-8'+ ($screen.width > 992 ? '' : ' collapse')" id="navigation">
          <div class="navigation">
            <RouterLink to="/">Home</RouterLink>
            <RouterLink to="/ProductList">Shop</RouterLink>
            <RouterLink to="/">Features</RouterLink>
            <RouterLink to="/Contact">Contact</RouterLink>
            <RouterLink to="/">
              <FontAwesomeIcon v-if="$screen.width > 992" icon="bag-shopping"></FontAwesomeIcon><span v-else>Cart</span>
            </RouterLink>
            <RouterLink to="/Login" v-if="!loginStore.isLogged" :class="$screen.width > 992 ? 'btn btn-primary' : ''">Login</RouterLink>
            <RouterLink to="/Profile" v-else>Profile</RouterLink>
          </div>
        </div>
      </div>
    </div>
  </div>
  <RouterView />
  <footer class="theme_bg_2">
    <div class="container">
      <div class="row footer_menu_row justify-content-between pt-5">
        <div class="col-md-6 col-lg-3">
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
        <div class="col-md-6 col-lg-2">
          <div class="footer_border"></div>
          <div class="footer_title">Shop</div>
          <div class="footer_link_list">
            <a href="#">Products</a>
            <a href="#">Overview</a>
            <a href="#">Prices</a>
            <a href="#">Releases</a>
          </div>
        </div>
        <div class="col-md-6 col-lg-2">
          <div class="footer_border"></div>
          <div class="footer_title">Company</div>
          <div class="footer_link_list">
            <a href="#">About us</a>
            <a href="#">Contact</a>
            <a href="#">News</a>
            <a href="#">Support</a>
          </div>
        </div>
        <div class="col-md-6 col-lg-4">
          <div class="footer_border"></div>
          <div class="footer_title">Stay up to date</div>
          <div class="mail_subscription pt-3">
            <input type="text" placeholder="Enter your email"><button>Submit</button>
          </div>
        </div>
      </div>
      <div class="row align-items-center pb-5 pt-5 footer_bottom">
        <div class="col-3 col-sm-6 col-md-7 col-lg-9 footer_bottom_border"></div>
        <div class="col-9 col-sm-6 col-md-5 col-lg-3 footer_bottom_links">
          <a href="#">Terms</a>
          <a href="#">Privacy</a>
          <a href="#">Cookies</a>
        </div>
      </div>
    </div>
  </footer>
</template>


<style scoped>
.footer_bottom_links>a {
  color: var(--white-color);
  font-family: var(--first-font);
  font-size: 18px;
  font-style: normal;
  font-weight: 500;
  line-height: normal;
  text-transform: capitalize;
  padding-right: 15px;
}

.footer_bottom_border {
  border: 1px solid var(--seventh-color);
  height: 0;
}

.mail_subscription {
  display: flex;
  width: 100%;
}

.mail_subscription>input {
  width: 70%;
  background-color: transparent;
  color: var(--white-color);
  padding: 9px 17px;
  border-radius: 3px 0px 0px 3px;
  border: 2px solid var(--seventh-color);
  outline: none;
}

.mail_subscription>input::placeholder {
  opacity: 1;
}

.mail_subscription>button {
  width: 30%;
  background: var(--seventh-color);
  border: 2px solid var(--seventh-color);
  border-radius: 0 var(--bs-border-radius) var(--bs-border-radius) 0;
}

.footer_link_list {
  display: flex;
  flex-direction: column;
}

.footer_link_list>a:first-child {
  padding-top: 12px;
}

.footer_link_list>a {
  color: var(--sixth-color);
  font-family: var(--first-font);
  font-size: 19px;
  font-style: normal;
  font-weight: 500;
  line-height: normal;
  text-transform: capitalize;
  padding-bottom: 12px;
}

.social_list>svg {
  color: var(--sixth-color);
  width: 20px;
  height: 20px;
  padding-right: 10px;
}

.footer_title {
  color: var(--white-color);
  font-family: var(--first-title-font);
  font-size: 22px;
  font-style: normal;
  font-weight: 500;
  line-height: normal;
}

.logo {
  color: var(--first-color);
  font-family: var(--logo-font);
  font-size: 33px;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
}

.logo_white {
  color: var(--white-color);
  font-family: var(--logo-font);
  font-size: 50px;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
}

.navigation {
  display: flex;
  justify-content: space-evenly;
  align-items: center;
  gap: 25px;
}
.navigation>a:not(:hover){
  color: var(--first-color);
}
.navigation>a {
  font-family: var(--first-font);
  font-size: 19px;
  font-style: normal;
  font-weight: 500;
  line-height: normal;
  text-transform: uppercase;
}

.btn-primary {
  padding-left: 25px;
  padding-right: 25px;
}

/*.footer_menu_row{
row-gap: 20px;
}*/
@media (max-width:992px) {
  .navigation {
    position: absolute;
    flex-direction: column;
    margin-top: 15px;
    width: 100%;
    background-color: white;
    padding: 15px;
  }

  .navigation>a {
    width: 100%;
    text-align: center;
  }
  #navigation{
    position: relative;
  }
}

@media (min-width: 768px) and (max-width: 992px) {

  .footer_menu_row>div:where(:last-child, :nth-last-child(2))>.footer_border {
    border-bottom: 1px solid var(--seventh-color);
    margin: 25px 0;
  }
}

@media (max-width:768px) {
  .footer_border {
    border-bottom: 1px solid var(--seventh-color);
    margin: 25px 0;
  }
}
</style>
<script>
export default {
  data(){
    return {
      loginStore:useLoginStore()
    }
  },
  watch: {
    '$route'(to, from) {
      
      var isHashChanged = to.fullPath.replace(to.hash, "") == from.fullPath.replace(from.hash, "") && to.hash != from.hash;
      var isPathChanged = to.fullPath.replace(to.hash, "") == from.fullPath.replace(from.hash, "");
      if (!isPathChanged && isHashChanged)
        window.scrollTo(0, 0)
    }
  }
}
</script>