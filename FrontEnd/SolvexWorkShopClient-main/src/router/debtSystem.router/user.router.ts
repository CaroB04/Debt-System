import {RouteConfig} from "vue-router";

const item = {
    path: "/user",
    name: "User",
    component: () => import("../../views/base.vue"),
    children: [
        {
            path: "",
            component: () => import("../../views/debtSystem.views/user/info.vue"),
        },
        {
            path: "add",
            component: () => import("../../views/debtSystem.views/user/add.vue"),
        },
        {
            path: "edit",
            component: () => import("../../views/debtSystem.views/user/edit.vue"),
        }
    ]
} as RouteConfig;

export default item;