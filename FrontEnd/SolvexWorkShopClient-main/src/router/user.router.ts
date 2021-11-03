import { RouteConfig } from "vue-router";
const item = {
  path: "/admin/users",
  name:"Users",
  component: () => import("../views/base.vue"),
  children: [
    {
      path: "",
      component: () => import("../views/admin/users/list.vue"),
    },
    {
      path: "add",
      component: () => import("../views/admin/users/add.vue")
    },
    {
        path: "edit/:id",
        component: () => import("../views/admin/users/edit.vue")
      }
  ],
} as RouteConfig;

export default item;