<template>
  <section>
    <b-sidebar
      :class="{
        'sidebar-reduced': isSidebarReduced,
        'sidebar-not-reduced': !isSidebarReduced,
      }"
      type="is-light"
      open
      fullheight
      :reduce="isSidebarReduced"
    >
      <div class="p-1">
        <div class="block">
          <img
            src="../../assets/sx-logo.png"
            alt="Lightweight UI components for Vue.js based on Bulma"
          />
        </div>
        <b-menu class="is-custom-mobile">
          <b-menu-list label="Menu">
            <b-menu-item
              active
              icon="home"
              label="Inicio"
              tag="router-link"
              :to="{ path: '/' }"
            >
            </b-menu-item>
            <b-menu-item
              icon="certificate"
              label="Mis talleres"
              tag="router-link"
              :to="{ path: '/user/workshops' }"
            >
            </b-menu-item>
            <b-menu-item
              v-if="isCurrentUserAdmin"
              expanded
              icon="security"
              label="Administración"
            >
              <b-menu-item
                icon="account-multiple"
                label="Usuarios"
                tag="router-link"
                :to="{ path: '/admin/users' }"
              ></b-menu-item>
              <b-menu-item
                icon="cogs"
                label="Talleres"
                tag="router-link"
                :to="{ path: '/admin/workshops' }"
              ></b-menu-item>
            </b-menu-item>
            <b-menu-item
              icon="account"
              label="Mi perfíl"
              tag="router-link"
              :to="{ path: '/user/profile' }"
            >
            </b-menu-item>
          </b-menu-list>
          <b-menu-list label="Acciones">
            <b-menu-item icon="logout" label="Salir"></b-menu-item>
          </b-menu-list>
        </b-menu>
      </div>
    </b-sidebar>
  </section>
</template>


<script lang="ts">
import { Component, Mixins, Prop, Vue } from "vue-property-decorator";
import { AuthMixin, SidebarMixin } from "@/mixins";
@Component
export default class Sidebar extends Mixins(SidebarMixin, AuthMixin) {}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
.p-1 {
  padding: 1em !important;
}
</style>
