<template>
  <div>
    <b-collapse class="card" animation="slide" aria-id="contentIdFor">
      <template #trigger="props">
        <div class="card-header" role="button" aria-controls="contentIdFor">
          <p class="card-header-title">Mis Talleres</p>
          <a class="card-header-icon">
            <b-icon :icon="props.open ? 'menu-down' : 'menu-up'"> </b-icon>
          </a>
        </div>
      </template>
      <div class="card-content">
        <div class="columns">
          <div class="column" v-for="workshop in workShops" :key="workshop.id" :value="workshop.id">
            <p>{{ workshop.name }} / {{ workshop.description }} / {{ workshop.contentSupport }}</p> <br/>
          </div>
        </div>
      </div>
    </b-collapse>
  </div>
</template>

<script lang="ts">
import { Component, Mixins } from "vue-property-decorator";
import { SxBuefyTableMixin } from "@/mixins";
import { User } from "@/core/model";
import axios from "axios";
import settings from "@/core/utils/app-settings";

@Component
export default class ProfileComponent extends Mixins<
  SxBuefyTableMixin<User>
>(SxBuefyTableMixin) {
  workShops = [];

  async created(){
    const resWorkShops = await axios.get(settings.API_URL + "api/WorkShop");
    this.workShops = resWorkShops.data;
  }
}
</script>
