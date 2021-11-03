import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import Home from '../views/Home.vue'
import User from './user.router'
import Workshop from './workshop.router'
import UserRouter from './debtSystem.router/user.router'
import Loan from './debtSystem.router/loan.router'
import Payment from './debtSystem.router/payment.router'


Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
  {
    path: '/',
    name: 'Home',
    component: Home,
  },
  {
    path: '/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  },
  User,
  Workshop,
  {
    path: '/user/profile',
    name: 'Profile',
    component: () => import(/* webpackChunkName: "profile" */ '../views/user/profile.vue')
  },
  {
    path: '/user/workshops',
    name: 'WorkShops',
    component: () => import(/* webpackChunkName: "profile" */ '../views/user/workshops.vue')
  },
  UserRouter,
  {
    path: '/user/info',
    name: 'Info',
    component: () => import('../views/debtSystem.views/user/info.vue')
  },
  {
    path: '/user/add',
    name: 'Add',
    component: () => import('../views/debtSystem.views/user/add.vue')
    },
    {
      path: '/user/edit',
      name: 'Edit',
      component: () => import('../views/debtSystem.views/user/edit.vue')
      },
  Loan,
  {
    path: '/loan/add',
    name: 'Add',
    component: () => import('../views/debtSystem.views/loan/add.vue')
  },
  {
    path: '/load/info',
    name: 'Info',
    component: () => import('../views/debtSystem.views/loan/list.vue')
  },
  {
    path: '/load/edit',
    name: 'Edit',
    component: () => import('../views/debtSystem.views/loan/edit.vue')
  },
Payment,
  {
    path: '/payment/edit',
    name: 'Edit',
    component: () => import('../views/debtSystem.views/payment/edit.vue')
  },
  {
    path: '/payment/info',
    name: 'Info',
    component: () => import('@/views/debtSystem.views/payment/list.vue')
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
