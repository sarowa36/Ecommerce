<script setup>
import { DashboardMenuItemValue, DashboardMenuItem } from './';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
</script>
<template>
    <div class="container mt-5 mb-5">
        <div class="row">
            <div class="col-lg-3 profile_aside pt-5 pb-5 ps-0 pe-0">
                <div class="profile_basic_detail">
                    <img src="@/assets/e.webp" alt="">
                    <h6><strong>Daniel Machivelli</strong></h6>
                </div>
                <hr>
                <ul class="dashboard_pages">
                    <DashboardMenuItem v-for="item in menuItems" :value="item" />
                    <!-- <li>
                        <button class="btn dashboard_submenu_collapser" @click="collapseSubmenu"><FontAwesomeIcon icon="angle-down" /></button>
                        <RouterLink to="/">
                            <FontAwesomeIcon icon="bell" />Notifications
                        </RouterLink>
                        <ul class="dashboard_submenu">
                            <li>
                                <button class="btn dashboard_submenu_collapser"><FontAwesomeIcon icon="angle-down" /></button>
                        <RouterLink to="/">
                            <FontAwesomeIcon icon="bell" />Notifications
                        </RouterLink>
                        <ul class="dashboard_submenu">
                            <li>
                                <RouterLink to="/">
                                    <FontAwesomeIcon icon="question" />My Questions
                                </RouterLink>
                            </li>
                            <li>
                                <RouterLink to="/">
                                    <FontAwesomeIcon icon="user-pen" />Profile Detail
                                </RouterLink>
                            </li>
                            <li>
                                <RouterLink to="/">
                                    <FontAwesomeIcon icon="lock" />Change Password
                                </RouterLink>
                            </li>
                        </ul>
                            </li>
                            <li>
                                <RouterLink to="/">
                                    <FontAwesomeIcon icon="user-pen" />Profile Detail
                                </RouterLink>
                            </li>
                            <li>
                                <RouterLink to="/">
                                    <FontAwesomeIcon icon="lock" />Change Password
                                </RouterLink>
                            </li>
                        </ul>
                    </li>
                    <li>
                        <RouterLink to="/Orders">
                            <FontAwesomeIcon icon="box" />Orders
                        </RouterLink>
                    </li> 
                    <li>
                        <RouterLink to="/">
                            <FontAwesomeIcon icon="question" />My Questions
                        </RouterLink>
                    </li>
                    <li>
                        <RouterLink to="/">
                            <FontAwesomeIcon icon="user-pen" />Profile Detail
                        </RouterLink>
                    </li>
                    <li>
                        <RouterLink to="/">
                            <FontAwesomeIcon icon="lock" />Change Password
                        </RouterLink>
                    </li>
                    <li>
                        <hr>
                    </li>
                    <li>
                        <RouterLink to="/">
                            <FontAwesomeIcon icon="heart" />My Favorites
                        </RouterLink>
                    </li>
                    <li>
                        <RouterLink to="/">
                            <FontAwesomeIcon icon="bookmark" />My Lists
                        </RouterLink>
                    </li>
                    <li>
                        <hr>
                    </li>
                    <li>
                        <RouterLink to="/">
                            <FontAwesomeIcon icon="right-from-bracket" />Exit
                    </RouterLink>
                </li>-->
                </ul>
            </div>
            <div class="col-lg-9">
                <slot></slot>
            </div>
        </div>
    </div>
</template>
<style>
.profile_aside {
    padding-left: 35px
}

.dashboard_pages {
    list-style: none;
    padding: 0;
}

.dashboard_pages a {
    padding: 10px 5px;
    font-size: 15px;
    display: block;
    color: #484848;
    transition: 0.3s;
}

.dashboard_pages a:where(:hover, .router-link-active) {
    color: var(--second-color);
}

.dashboard_pages a svg {
    margin-right: 5px;
    width: 30px;
    font-size: 17px;
}

.profile_basic_detail {
    display: flex;
    align-items: center;
    margin-bottom: 25px;
}

.profile_basic_detail>img {
    aspect-ratio: 1;
    width: 50%;
    max-width: 86px;
    object-fit: cover;
    border-radius: 50%;
}

.profile_basic_detail>h6 {
    margin: 0 15px;
}

.dashboard_pages li {
    display: flex;
    flex-direction: row-reverse;
    flex-wrap: wrap;
    justify-content: start;
}

.dashboard_pages li hr {
    width: 100%;
}

.dashboard_pages a {
    width: 100%;
}

.dashboard_submenu_collapser {
    width: 20%;
}

.dashboard_submenu_collapser+a {
    width: 80%;
}

.dashboard_submenu {
    list-style: none;
    padding-left: 18px;
    width: 100%;
    height: 0px;
    overflow: hidden;
    transition: 0.3s;
}

.dashboard_submenu.active {
    border-left: 1px solid color-mix(in srgb, var(--first-color) 30%, transparent);
    height: max-content;
    box-shadow: -5px 0 10px 2px rgba(72, 72, 72, .1);
    margin: 10px 0;
}
</style>
<script>
export default {
    data() {
        return {
            menuItems: [
                new DashboardMenuItemValue({
                    icon: "bell", link: "/", text: "Notifications",
                    childItems: [new DashboardMenuItemValue({ icon: "box", link: "/Orders", text: "Orders" }),
                    new DashboardMenuItemValue({ icon: "question", link: "/", text: "My Questions" }),
                    new DashboardMenuItemValue({ icon: "user-pen", link: "/", text: "Profile Detail" }),
                    new DashboardMenuItemValue({ icon: "lock", link: "/", text: "Change Password" })]
                }),
                new DashboardMenuItemValue({ icon: "box", link: "/Orders", text: "Orders" }),
                new DashboardMenuItemValue({ icon: "question", link: "/", text: "My Questions" }),
                new DashboardMenuItemValue({ icon: "user-pen", link: "/", text: "Profile Detail" }),
                new DashboardMenuItemValue({ icon: "lock", link: "/", text: "Change Password" }),
                new DashboardMenuItemValue({ isHr: true }),
                new DashboardMenuItemValue({ icon: "heart", link: "/", text: "My Favorites" }),
                new DashboardMenuItemValue({ icon: "bookmark", link: "/", text: "My Lists" }),
                new DashboardMenuItemValue({ isHr: true }),
                new DashboardMenuItemValue({ icon: "right-from-bracket", link: "/", text: "Exit" }),
            ]
        }
    },
    methods: {
        collapseSubmenu(e) {
            var listNode = e.currentTarget.nextElementSibling.nextElementSibling;
            this.active = true;
            if (listNode.classList.contains("active")) {
                listNode.classList.remove("active")
                listNode.style.height = 0 + "px";
            }
            else {
                var totalHeight = 0;
                listNode.childNodes.forEach(x => totalHeight += x.offsetHeight)
                listNode.style.height = totalHeight + "px"
                listNode.classList.add("active")
            }
        }
    }
}
</script>