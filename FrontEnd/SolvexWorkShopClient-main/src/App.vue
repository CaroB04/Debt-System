<template>
  <div id="app">
    <sidebar />
    <div
      class="main-content"
      :class="{
        'main-content-on-sidebar-reduced': isSidebarReduced,
        'main-content-on-sidebar-not-reduced': !isSidebarReduced,
      }"
    >
      <navbar />

      <!-- Borrar -->
      <b-button><router-link to = "/user/info">Ver Info User</router-link> </b-button>
      <br>
      <b-button><router-link to = "/user/add">Crear User</router-link> </b-button>
      <br>
      <b-button><router-link to = "/user/edit">Editar User</router-link> </b-button>
      <br>
      <b-button><router-link to = "/payment/list">Ver Info Payments</router-link> </b-button>
      <br>
       <b-button><router-link to = "/payment/edit">Editar Payments</router-link> </b-button>
      <br>
      <b-button><router-link to = "/loan/list">Ver Info Loan</router-link> </b-button>
      <br>
      <b-button><router-link to = "/loan/add">Crear Loan</router-link> </b-button>
      <br>
      <b-button><router-link to = "/loan/edit">Editar Loan</router-link> </b-button>
 
      <!-- Hasta aquí -->

      <router-view />
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Mixins, Vue, Watch } from "vue-property-decorator";
import { SidebarMixin } from "./mixins";


@Component
export default class App extends Mixins(SidebarMixin) {}
</script>
<style lang="scss" >
.main-content {
  position: absolute;
  right: 0;
}
.main-content-on-sidebar-not-reduced {
  width: calc(100% - 260px);
}
.main-content-on-sidebar-reduced {
  width: calc(100% - 90px);
}
.sidebar-reduced .menu-list li a span:last-child {
  display: none !important;
}
.icon-image-rounded {
  height: 40px;
  align-items: center;
  width: 40px;
  color: white;
  font-size: 20px;
  justify-content: center;
  text-align: center;
  display: flex;
  border-radius: 100px;
}
.cursor-ban {
  cursor: not-allowed;
}
.relative{
  position: relative !important;
}

.image-is-350x210 img{
  max-width: 350px;
  max-height: 210px;

}
</style>
