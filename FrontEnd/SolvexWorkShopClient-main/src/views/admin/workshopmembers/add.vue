<template>
  <section>
    <nav class="navbar">
      <div class="navbar-brand">
        <span class="navbar-item title is-2">Agregar Miembro</span>
      </div>
    </nav>
    <form @submit.prevent="validate">
      <b-collapse class="card" animation="slide" aria-id="contentIdFor">
        <template #trigger="props">
          <div class="card-header" role="button" aria-controls="contentIdFor">
            <p class="card-header-title">Detalles del Miembro</p>
            <a class="card-header-icon">
              <b-icon :icon="props.open ? 'menu-down' : 'menu-up'"> </b-icon>
            </a>
          </div>
        </template>

        <div class="card-content">
          <div class="content">
            <div class="columns">
              
              <div class="column">  
                <!--role-->
                <b-field
                  label="Rol*"
                  :type="{ 'is-danger': errors.has('Role') }"
                  :message="errors.first('Role')"
                >
                  <b-radio
                    v-model="model.role"
                    name="role"
                    data-vv-as="rol"
                    v-validate="'required'"
                    :native-value="0"
                  >
                    Maestro
                  </b-radio>
                  <b-radio
                    v-model="model.role"
                    name="role"
                    data-vv-as="rol"
                    v-validate="'required'"
                    :native-value="1"
                  >
                    Aprendiz
                  </b-radio>
                </b-field>

                <!--workShopId-->
                <b-field
                  label="ID del Taller*"
                  :type="{ 'is-danger': errors.has('workShopId') }"
                  :message="errors.first('workShopId')"
                >
                  <b-input
                    v-model="currentWorkshopId"
                    name="workShopId"
                    data-vv-as="id del taller"
                    v-validate="'required'"
                    placeholder="Requerido"
                  />
                </b-field>

                <!--userId-->
                <b-field
                  label="ID del Usuario*"
                  :type="{ 'is-danger': errors.has('userId') }"
                  :message="errors.first('userId')"
                >
                <b-select
                  expanded
                  name="userId"
                  v-model="model.userId"
                  data-vv-as="Usuario"
                  v-validate="'required'"
                  placeholder="Requerido"
                >
                  <option v-for="user in users" :key="user.id" :value="user.id">
                    {{ user.name }} {{ user.lastName }}
                  </option>

                </b-select>

                </b-field>

              </div>
            </div>
          </div>
        </div>
      </b-collapse>
      <br />

      <nav class="level">
        <!-- Left side -->
        <div class="level-left">
          <p class="level-item">
            <b-button
              :disabled="(modelDoNotChange && !errors.any()) || saving"
              type="is-light"
              @click="clean"
              icon-right="eraser"
              >Reiniciar</b-button
            >
          </p>
          <p class="level-item">
            <b-button
              type="is-danger"
              :disabled="saving"
              @click="cancel"
              icon-right="cancel"
              >Cancelar</b-button
            >
          </p>
        </div>

        <!-- Right side -->
        <div class="level-right">
          <p class="level-item">
            <b-button
              type="is-primary"
              native-type="submit"
              :disabled="errors.any()"
              :loading="saving"
              icon-right="content-save"
              >Guardar</b-button
            >
          </p>
        </div>
      </nav>
    </form>
  </section>
</template>


<script lang="ts">
import { Component, Mixins, Vue } from "vue-property-decorator";
import { BaseFormAddMixin } from "@/mixins";
import { WorkShopMember } from "@/core/model";
import axios from "axios";
import settings from "@/core/utils/app-settings";

@Component({})
export default class WorkShopMemberAddComponent extends Mixins<BaseFormAddMixin<WorkShopMember>>(
  BaseFormAddMixin
) {
  
  hourFormat: string = "12";
  currentWorkshopId: string = this.$router.currentRoute.path.substring(27)
  userId: string = '';
  users = [];


  
  async created(){
    const resUsers = await axios.get(settings.API_URL + "api/User");
    //const resWorkShop = await axios.get(settings.API_URL + "api/workshop");
    this.users = resUsers.data;
  }
  


  constructor() {
    super();
    this.controller = "WorkShopMember";
    this.model = new WorkShopMember();
    this.model.workShopId = parseInt(this.currentWorkshopId);
    //this.model.userId = parseInt(this.userId);
  }

}
</script>