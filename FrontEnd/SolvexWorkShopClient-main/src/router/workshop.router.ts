import { RouteConfig } from "vue-router";
const item = {
  path: "/admin/workshops",
  name:"Workshops",
  component: () => import("../views/base.vue"),
  children: [
    {
      path: "",
      component: () => import("../views/admin/workshops/list.vue"),
    },
    {
      path: "add",
      component: () => import("../views/admin/workshops/add.vue")
    },
    {
      path: "edit/:id",
      component: () => import("../views/admin/workshops/edit.vue")
    },
    {
      path: "addDay/:id",
      component: () => import("../views/admin/workshopdays/add.vue")
    },
    {
      path: "addMember/:id",
      component: () => import("../views/admin/workshopmembers/add.vue")
    }
  ],
} as RouteConfig;

export default item;