<template>
  <section>
    <nav class="navbar">
      <div class="navbar-brand">
        <span class="navbar-item title is-2">Agregar WorkShop</span>
      </div>
    </nav>
    <form @submit.prevent="validate">
      <b-collapse class="card" animation="slide" aria-id="contentIdFor">
        <template #trigger="props">
          <div class="card-header" role="button" aria-controls="contentIdFor">
            <p class="card-header-title">Detalles del WorkShop</p>
            <a class="card-header-icon">
              <b-icon :icon="props.open ? 'menu-down' : 'menu-up'"> </b-icon>
            </a>
          </div>
        </template>

        <div class="card-content">
          <div class="content">
            <div class="columns">
              
              <div class="column">
                <!--name-->
                <b-field
                  label="Nombre del WorkShop*"
                  :type="{ 'is-danger': errors.has('name') }"
                  :message="errors.first('name')"
                >
                  <b-input
                    v-model="model.name"
                    name="name"
                    data-vv-as="nombre"
                    v-validate="'required'"
                    placeholder="Requerido"
                  />
                </b-field>
                
                <!--description-->
                <b-field
                  label="Descripción*"
                  :type="{ 'is-danger': errors.has('description') }"
                  :message="errors.first('description')"
                >
                  <b-input
                    v-model="model.description"
                    name="description"
                    data-vv-as="descripcion"
                    v-validate="'required'"
                    placeholder="Requerido"
                  />
                </b-field>

                <!--startDate-->
                <b-field
                  label="Fecha de inicio*"
                  :type="{ 'is-danger': errors.has('startDate') }"
                  :message="errors.first('startDate')"
                >
                  <b-datepicker
                    v-model="model.startDate"
                    name="startDate"
                    data-vv-as="fecha de inicio"
                    v-validate="'required'"
                    placeholder="dd/MM/yyyy"
                    icon="calendar-today"
                    locale="es"
                  >
                  </b-datepicker>
                </b-field>

                <!--endDate-->
                <b-field
                  label="Fecha de finalización*"
                  :type="{ 'is-danger': errors.has('endDate') }"
                  :message="errors.first('endDate')"
                >
                  <b-datepicker
                    v-model="model.endDate"
                    name="endDate"
                    data-vv-as="fecha de finalizacion"
                    v-validate="'required'"
                    placeholder="dd/MM/yyyy"
                    icon="calendar-today"
                    locale="es"
                  >
                  </b-datepicker>
                </b-field>

                <!--contentSupport-->
                <b-field
                  label="Contenido de apoyo*"
                  :type="{ 'is-danger': errors.has('contentSupport') }"
                  :message="errors.first('contentSupport')"
                >
                  <b-input
                    v-model="model.contentSupport"
                    name="contentSupport"
                    data-vv-as="contenido de apoyo"
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
import { WorkShop } from "@/core/model";
@Component
export default class WorkShopAddComponent extends Mixins<BaseFormAddMixin<WorkShop>>(
  BaseFormAddMixin
) {

  constructor() {
    super();
    this.controller = "WorkShop";
    this.model = new WorkShop();
  }

}
</script>

<style lang="scss" scoped>
</style>