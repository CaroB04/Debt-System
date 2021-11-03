<template>
  <section >
    <nav class="navbar">
      <div class="navbar-brand">
        <span class="navbar-item title is-2">Agregar Préstamo</span>
      </div>
    </nav>
    <form @submit.prevent="validate">
      <b-collapse class="card" animation="slide" aria-id="contentIdForA11y3">
        <template #trigger="props">
          <div
            class="card-header"
            role="button"
            aria-controls="contentIdForA11y3"
          >
            <p class="card-header-title">Datos del préstamo</p>
            <a class="card-header-icon">
              <b-icon :icon="props.open ? 'menu-down' : 'menu-up'"> </b-icon>
            </a>
          </div>
        </template>

        <div class="card-content" id="app">
          <div class="content">
            <div class="columns is-multiline">
              <div class="column">
                <b-field label="Prestamista*" v-model="model.lenderId">
                  <b-select placeholder="Requerido">
                    <option
                      v-for="option in data"
                      :value="option.id"
                      :key="option.id"
                    >
                      {{ option.user.first_name }}
                    </option>
                  </b-select>
                </b-field>
              </div>

              <div class="column">
                <b-field label="Deudor*">
                  <b-select placeholder="Requerido" v-model="model.debtorId">
                    <option
                      v-for="option in deadline"
                      :value="option.value"
                      :key="option.name"
                    >
                      {{option.name}}
                    </option>
                  </b-select>
                </b-field>
              </div>

              <div class="column">
                <b-field label="Plazo*" v-model="model.term">
                  <b-select placeholder="Requerido">
                    <option value="Biweekly">
                    Quincenal
                    </option>
                    <option value="Monthly">
                    Mensual
                    </option>
                      <option value="Annual">
                    Anual
                    </option>
                  </b-select>
                </b-field>
              </div>
            </div>

            <div class="columns is-multiline">
              <div class="column">
                <b-field
                  label="Monto*"
                  :type="{ 'is-danger': errors.has('amount') }"
                  :message="errors.first('amount')"
                >
                  <b-input
                    v-model="model.amount"
                    name="amount"
                    data-vv-as="monto"
                    v-validate="'required'"
                    placeholder="Requerido"
                  />
                </b-field>
              </div>
              <div class="column">
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
              </div>
            </div>
          </div>
        </div>
      </b-collapse>
      <br />

      <nav class="level">
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

<style lang="scss" scoped>
</style>
<script lang="ts">
import { Component, Mixins, Vue } from "vue-property-decorator";
import { BaseFormAddMixin } from "@/mixins";
import { FileService } from "@/core/services";
import { LoanModel } from "@/core/model/debtSystem.models/loan.model";
import { Deadline } from "../../../core/utils/debtSystem.enums/enums";

@Component
export default class UserAddComponent extends Mixins<
  BaseFormAddMixin<LoanModel>
>(BaseFormAddMixin) {
  constructor() {
    super();
    this.controller = "Loan";
    this.model = new LoanModel();
  }
}

</script>
