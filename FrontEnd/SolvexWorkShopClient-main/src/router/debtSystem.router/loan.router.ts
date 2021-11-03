import {RouteConfig} from "vue-router";

const item = {
    path: "/loan",
    name: "Loan",
    component: () => import("../../views/base.vue"),
    children: [
        {
            path: "list",
            component: () => import("../../views/debtSystem.views/loan/list.vue"),
        },
        {
            path: "add",
            component: () => import("../../views/debtSystem.views/loan/add.vue"),
        },
        {
            path: "edit",
            component: () => import("../../views/debtSystem.views/loan/edit.vue"),
        }
    ]
} as RouteConfig;

export default item;