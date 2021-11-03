<template>
  <section>
    <nav class="navbar">
      <div class="navbar-brand">
        <span class="navbar-item title is-2">Agregar usuario</span>
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
            <p class="card-header-title">Datos personales</p>
            <a class="card-header-icon">
              <b-icon :icon="props.open ? 'menu-down' : 'menu-up'"> </b-icon>
            </a>
          </div>
        </template>

        <div class="card-content">
          <div class="content">
            <div class="columns is-multiline">
              <div class="column">
                <b-field
                  label="Nombre*"
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
              </div>

              <div class="column">
                <b-field
                  label="Cédula*"
                  :type="{ 'is-danger': errors.has('identityCard') }"
                  :message="errors.first('identityCard')"
                >
                  <the-mask
                    name="identityCard"
                    data-vv-as="cédula"
                    v-validate="'required'"
                    placeholder="Requerido"
                    v-model="model.identityCard"
                    :mask="'###-#######-#'"
                    type="text"
                    class="input"
                    :class="{ 'is-danger': errors.has('cédula') }"
           
                  />
                </b-field>
              </div>
            </div>

            <div class="columns is-multiline">
              <div class="column">
                <b-field
                  label="Dirección*"
                  :type="{ 'is-danger': errors.has('address') }"
                  :message="errors.first('address')"
                >
                  <b-input
                    v-model="model.address"
                    name="address"
                    data-vv-as="dirección"
                    v-validate="'required'"
                    placeholder="Requerido"
                  />
                </b-field>
            
              </div>
              <div class="column">
                <b-field
                  label="Teléfono*"
                  :type="{ 'is-danger': errors.has('phoneNumber') }"
                  :message="errors.first('phoneNumber')"
                >
                  <the-mask
                    name="phoneNumber"
                    data-vv-as="phoneNumber"
                    v-model="model.phoneNumber"
                    :mask="'###-###-####'"
                    v-validate="'required'"
                    type="text"
                    class="input"
                    :class="{ 'is-danger': errors.has('teléfono') }"
                    placeholder="Requerido"
                  />
                </b-field>
              </div>
            </div>

            <div class="columns is-multiline">
              <div class="column">
                <b-field label="Email">
                  <b-input type="email" maxlength="50" v-model="model.email">
                  </b-input>
                </b-field>
              </div>

              <div class="column">
                   <b-field
                  label="Contraseña*"
                  :type="{ 'is-danger': errors.has('password') }"
                  :message="errors.first('password')"
                >
                  <b-input
                    v-model="model.password"
                    name="password"
                    data-vv-as="contraseña"
                    v-validate="'required'"
                    placeholder="Requerido"
                    minlenght = 6
                    type="password"
                    password-reveal
                  />
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
              >Guardar</b-button>
          </p>
        </div>
        <b-button @click="getUsers()">
          Ver users en consola
        </b-button>
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
import { UserModel } from "@/core/model/debtSystem.models/user.model";
import axios from "axios";
import settings from "@/core/utils/app-settings";
import {UserService} from "@/core/services/debtSystem.services/user.services"

@Component
export default class UserAddComponent extends Mixins<
  BaseFormAddMixin<UserModel>
>(BaseFormAddMixin) {
  constructor() {
    super();
    this.controller = "User";
    this.model = new UserModel();
  }

  

 getUsers(): void {

    //axios.get(settings.API_URL+"api/user").then(response => console.log(response.data));
    const service = new UserService(this.controller);

    service.get().then(response => console.log(response.data));

  }
}
</script>
