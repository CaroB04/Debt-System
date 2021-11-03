<template>
  <section>
    <nav class="navbar">
      <div class="navbar-brand">
        <span class="navbar-item title is-2">Agregar Día</span>
      </div>
    </nav>
    <form @submit.prevent="validate">
      <b-collapse class="card" animation="slide" aria-id="contentIdFor">
        <template #trigger="props">
          <div class="card-header" role="button" aria-controls="contentIdFor">
            <p class="card-header-title">Detalles del Día</p>
            <a class="card-header-icon">
              <b-icon :icon="props.open ? 'menu-down' : 'menu-up'"> </b-icon>
            </a>
          </div>
        </template>

        <div class="card-content">
          <div class="content">
            <div class="columns">
              
              <div class="column">
                <!--day-->
                <b-field
                  label="Día*"
                  :type="{ 'is-danger': errors.has('day') }"
                  :message="errors.first('day')"
                >
                  <b-radio
                    v-model="model.day"
                    name="day"
                    data-vv-as="dia"
                    v-validate="'required'"
                    :native-value="0"
                  >
                    Lunes
                  </b-radio>
                  <b-radio
                    v-model="model.day"
                    name="day"
                    data-vv-as="dia"
                    v-validate="'required'"
                    :native-value="1"
                  >
                    Martes
                  </b-radio>
                  <b-radio
                    v-model="model.day"
                    name="day"
                    data-vv-as="dia"
                    v-validate="'required'"
                    :native-value="2"
                  >
                    Miércoles
                  </b-radio>
                  <b-radio
                    v-model="model.day"
                    name="day"
                    data-vv-as="dia"
                    v-validate="'required'"
                    :native-value="3"
                  >
                    Jueves
                  </b-radio>
                  <b-radio
                    v-model="model.day"
                    name="day"
                    data-vv-as="dia"
                    v-validate="'required'"
                    :native-value="4"
                  >
                    Viernes
                  </b-radio>
                  <b-radio
                    v-model="model.day"
                    name="day"
                    data-vv-as="dia"
                    v-validate="'required'"
                    :native-value="5"
                  >
                    Sábado
                  </b-radio>
                  <b-radio
                    v-model="model.day"
                    name="day"
                    data-vv-as="dia"
                    v-validate="'required'"
                    :native-value="6"
                  >
                    Domingo
                  </b-radio>
                </b-field>
                
                <!--mode-->
                <b-field
                  label="Modalidad*"
                  :type="{ 'is-danger': errors.has('Mode') }"
                  :message="errors.first('Mode')"
                >
                  <b-radio
                    v-model="model.mode"
                    name="mode"
                    data-vv-as="modalidad"
                    v-validate="'required'"
                    :native-value="0"
                  >
                    Presencial
                  </b-radio>
                  <b-radio
                    v-model="model.mode"
                    name="mode"
                    data-vv-as="modalidad"
                    v-validate="'required'"
                    :native-value="1"
                  >
                    Virtual
                  </b-radio>
                </b-field>

                <!--modeLocation-->
                <b-field
                  label="Ubicación*"
                  :type="{ 'is-danger': errors.has('modeLocation') }"
                  :message="errors.first('modeLocation')"
                >
                  <b-input
                    v-model="model.modeLocation"
                    name="modeLocation"
                    data-vv-as="ubicacion"
                    v-validate="'required'"
                    placeholder="Requerido"
                  />
                </b-field>

                <!--startHour-->
                <b-field
                  label="Hora de inicio*"
                  :type="{ 'is-danger': errors.has('startHour') }"
                  :message="errors.first('startHour')"
                >
                  <b-timepicker 
                    v-model="model.startHour"
                    inline
                    :enable-seconds="true"
                    :hour-format="hourFormat"
                  ></b-timepicker>
                </b-field>

                <!--endHour-->
                <b-field
                  label="Hora de término"
                  :type="{ 'is-danger': errors.has('endHour') }"
                  :message="errors.first('endHour')"
                >
                  <b-timepicker 
                    v-model="model.endHour"
                    inline
                    :enable-seconds="true"
                    :hour-format="hourFormat"
                  ></b-timepicker>
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
import { WorkShopDay } from "@/core/model";
@Component({})
export default class WorkShopDayAddComponent extends Mixins<BaseFormAddMixin<WorkShopDay>>(
  BaseFormAddMixin
) {
  hourFormat: string = "12";
  currentWorkshopId: string = this.$router.currentRoute.path.substring(24)

  constructor() {
    super();
    this.controller = "WorkShopDay";
    this.model = new WorkShopDay();
    this.model.workShopId = parseInt(this.currentWorkshopId);
    
  }

}
</script>