<script setup>
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
</script>
<template>
    <div class="container mt-5 mb-5">
        <div class="row">
            <div class="col-lg-2 profile_aside pt-5 pb-5 ps-0 pe-0">
                <div class="profile_basic_detail">
                    <img src="@/assets/e.webp" alt="">
                    <h6><strong>Daniel Machivelli</strong></h6>
                </div>
                <hr>
                <ul class="dashboard_pages">
                    <li><RouterLink to="/"><FontAwesomeIcon icon="bell" />Notifications</RouterLink></li>
                    <li><RouterLink to="/Orders"><FontAwesomeIcon icon="box" />Orders</RouterLink></li>
                    <li><RouterLink to="/"><FontAwesomeIcon icon="question" />My Questions</RouterLink></li>
                    <li><RouterLink to="/"><FontAwesomeIcon icon="user-pen" />Profile Detail</RouterLink></li>
                    <li><RouterLink to="/"><FontAwesomeIcon icon="lock" />Change Password</RouterLink></li>
                    <li><hr></li>
                    <li><RouterLink to="/"><FontAwesomeIcon icon="heart" />My Favorites</RouterLink></li>
                    <li><RouterLink to="/"><FontAwesomeIcon icon="bookmark" />My Lists</RouterLink></li>
                    <li><hr></li>
                    <li><RouterLink to="/"><FontAwesomeIcon icon="right-from-bracket" />Exit</RouterLink></li>
                </ul>
            </div>
            <div class="col-lg-10">
<slot></slot>
            </div>
        </div>
    </div>
</template>
<style scoped>
.profile_aside {
    padding-left: 35px
}
.dashboard_pages{
    list-style: none;
    padding: 0;
}
.dashboard_pages a{
    padding: 10px 5px;
    font-size: 15px;
    display: block;
    color: #484848;
    transition: 0.3s;
}
.dashboard_pages a:where(:hover, .router-link-active){
   color: var(--second-color);
}
.dashboard_pages a svg{
    margin-right: 5px;
    width: 30px;
    font-size: 17px;
}
.profile_basic_detail{
    display: flex;
    align-items: center;
    margin-bottom: 25px;
}
.profile_basic_detail >img{
    aspect-ratio:1;
    width: 50%;
    max-width: 86px;
    object-fit: cover;
    border-radius: 50%;
}
.profile_basic_detail > h6{
    margin:0 15px;
}</style>