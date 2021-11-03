<template>
  <b-navbar>
    <template #brand>
      <b-navbar-item  @click="switchSidebarReduce">
        <b-tooltip
          :label="isSidebarReduced ? 'Expandir' : 'Reducir'"
          position="is-right"
        >
          <b-icon
            :icon="isSidebarReduced ? 'arrow-right' : 'arrow-left'"
            size="is-small"
          >
          </b-icon>
        </b-tooltip>
      </b-navbar-item>
    </template>
    <template #end>
      <b-navbar-dropdown position="is-bottom-left" :label="currentUser.name">
         <b-navbar-item >{{ currentUser.name}} </b-navbar-item>
        <b-navbar-item href="/user/profile"> Perfíl </b-navbar-item>
        <b-navbar-item href="/user/workshops"> Mis talleres </b-navbar-item>
      </b-navbar-dropdown>
      <b-navbar-item tag="div">
        <div class="buttons">
          <a @click="logout" class="button is-light"> Salir </a>
        </div>
      </b-navbar-item>
    </template>
  </b-navbar>
</template>

<script lang="ts">
import { Component, Mixins, Prop, Vue } from "vue-property-decorator";
import { SidebarMixin, AuthMixin } from "@/mixins";
@Component
export default class Navbar extends Mixins(SidebarMixin, AuthMixin) {}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
</style>
