<script setup>
import { DashboardMenuItemValue, DashboardMenuItem } from './';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { useLoginStore, UserRole } from "@/stores/LoginStore";
</script>
<template>
    <div class="container mt-5 mb-5">
        <div class="row">
            <div class="col-lg-3 profile_aside pt-5 pb-5 ps-0 pe-0">
                <div class="form-check form-switch d-flex align-items-center justify-content-center gap-2 p-2">
                    <label class="form-check-label">User</label>
                    <input class="form-check-input m-0" type="checkbox" role="switch" :checked="loginStore.user.roles.includes(UserRole.Admin)" @change="toggleRole">
                    <label class="form-check-label">Admin</label>
                </div>
                <div class="profile_basic_detail">
                    <img src="@/assets/e.webp" alt="">
                    <h6><strong>Daniel Machivelli</strong></h6>
                </div>
                <hr>
                <ul class="dashboard_menu">
                    <DashboardMenuItem v-if="loginStore.user.roles.includes(UserRole.Admin)" v-for="item in adminMenuItems"
                        :value="item" />
                    <DashboardMenuItem v-else v-for="item in userMenuItems" :value="item" />
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

.dashboard_menu {
    list-style: none;
    padding: 0;
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
</style>
<script>
export default {
    data() {
        return {
            loginStore: useLoginStore(),
            userMenuItems: [
                new DashboardMenuItemValue({icon: "bell", link: "", text: "Notifications"}),
                new DashboardMenuItemValue({ icon: "box", link: "/Orders", text: "Orders" }),
                new DashboardMenuItemValue({ icon: "question", link: "", text: "My Questions" }),
                new DashboardMenuItemValue({ icon: "user-pen", link: "", text: "Profile Detail" }),
                new DashboardMenuItemValue({ icon: "map", link: "/Address", text: "Address" }),
                new DashboardMenuItemValue({ icon: "lock", link: "", text: "Change Password" }),
                new DashboardMenuItemValue({ isHr: true }),
                new DashboardMenuItemValue({ icon: "heart", link: "", text: "My Favorites" }),
                new DashboardMenuItemValue({ icon: "bookmark", link: "", text: "My Lists" }),
                new DashboardMenuItemValue({ isHr: true }),
                new DashboardMenuItemValue({ icon: "right-from-bracket", link: "", text: "Exit" }),
            ],
            adminMenuItems: [
                new DashboardMenuItemValue({icon:"box",text:"Products",link:"/ProductList",childItems:[new DashboardMenuItemValue({icon:"plus",text:"Create",link:"/ProductCreate"})]}),
                new DashboardMenuItemValue({ isHr: true }),
                new DashboardMenuItemValue({ icon: "right-from-bracket", link: "", text: "Exit" }),
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
        },
        toggleRole(e) {
            var val = e.currentTarget.checked;
            if (val) {
                this.loginStore.user.roles = [];
                this.loginStore.user.roles.push(UserRole.Admin)
            }
            else {
                this.loginStore.user.roles = [];
                this.loginStore.user.roles.push(UserRole.User)
            }
        }
    }
}
</script>