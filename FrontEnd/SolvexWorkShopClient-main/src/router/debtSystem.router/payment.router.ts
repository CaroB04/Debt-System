import {RouteConfig} from "vue-router";

const item = {
    path: "/payment",
    name: "Payment",
    component: () => import("../../views/base.vue"),
    children: [
        {
            path: "list",
            component: () => import("../../views/debtSystem.views/payment/list.vue"),
        },
        {
            path: "edit",
            component: () => import("../../views/debtSystem.views/payment/edit.vue"),
        }
    ]
} as RouteConfig;

export default item;