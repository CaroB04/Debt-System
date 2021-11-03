<template>
  <div>
    <sx-buefy-table :config="tableConfig">

      <template v-slot:name="{ row }" @click=workshopDetails(row.id)>
        <div class="media">

          <div class="media-left">
            <p class="icon-image-rounded has-background-primary">W</p>
          </div>

          <div class="media-content">
            <p class="title is-6">
              <b-tooltip label="WorkShop no disponible">
                <a class="has-text-link cursor-ban">{{ row.name }}</a>
              </b-tooltip>
            </p>
            
          </div>
        </div>
      </template>

      <template v-slot:description="{ row }">
        <p class="subtitle is-6">
          {{ row.description }}
        </p>
      </template>

      <template v-slot:startDate="{ row }">
        <p class="subtitle is-6">
          {{ row.startDate }}
        </p>
      </template>

      <template v-slot:contentSupport="{ row }">
        <p class="subtitle is-6">
          {{ row.contentSupport }}
        </p>
      </template>

      <template v-slot:addDay="{ row }">
        <div class="buttons is-centered" >
          <b-button label="+"  type="is-primary" outlined @click="addDay(row.id)" />
        </div>
      </template>

      <template v-slot:addMember="{ row }">
        <div class="buttons is-centered">
          <b-button label="+" type="is-primary" outlined @click="addMember(row.id)"/>
        </div>
      </template>

      <template v-slot:detalles="{ row }">
        <div class="buttons is-centered">
          <b-button label="+ detalles" type="is-primary" outlined  @click="workshopDetails(row.id)"/>
        </div>
      </template>

      <b-modal v-if="isCardModalActive === true" :width="640" scroll="keep">
        <div class="card">
          <div class="card-content">
            <p>{{ selectedWorkShop.name }} / {{ selectedWorkShop.description }} / {{ selectedWorkShop.contentSupport }}</p> <br/>
          </div>
        </div>
      </b-modal>

    </sx-buefy-table>
  </div>
</template>

<script lang="ts">
import { Component, Mixins } from "vue-property-decorator";
import { SxBuefyTableMixin } from "@/mixins";
import { BTableColumn } from "@/components/sx/sx-buefy-table/config";
import { WorkShop } from "@/core/model";
import axios from "axios";
import settings from "@/core/utils/app-settings";

@Component({

})
export default class WorkShopListComponent extends Mixins<SxBuefyTableMixin<WorkShop>>(SxBuefyTableMixin) {
  


  created() {
    this.setTableConfig();
    this.formatColumns();
  }
  setTableConfig() {
    this.tableConfig.controller = "WorkShop";
    this.tableConfig.aggregatable = true;
    this.tableConfig.aggregating.title = "Listado de Talleres";
  }
  formatColumns() {
    let name = new BTableColumn("name", "Taller");
    name.customTemplate = true;
    
    let description = new BTableColumn("description", "Descripción");
    description.customTemplate = true;

    let startDate = new BTableColumn("startDate", "Fecha Inicio");
    startDate.customTemplate = true;

    let addDay = new BTableColumn("addDay","Agregar Día")
    addDay.customTemplate = true;
    
    let addMember = new BTableColumn("addMember","Agregar Miembro")
    addMember.customTemplate = true;
    let detalles = new BTableColumn("detalles", "Ver Detalles")
    detalles.customTemplate = true;

    this.tableConfig.insertColumns(name, description, startDate, addDay, addMember, detalles);
  }
  isCardModalActive: boolean = false;
  selectedWorkShop = [];

  async workshopDetails(id: number){
    const resWorkShops = await axios.get(settings.API_URL + `api/WorkShop/${id}`);
    this.selectedWorkShop = resWorkShops.data;
    this.isCardModalActive = true;
  }

  addDay(id: number){
    this.$router.push(
      `${this.$router.currentRoute.path}/addDay/${id}`
    );
  }

  addMember(id: number){
    this.$router.push(
      `${this.$router.currentRoute.path}/addMember/${id}`
    );
  }
}
</script>
