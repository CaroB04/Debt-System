<template>
  <section>
    <nav class="navbar">
      <div class="navbar-brand">
        <span class="navbar-item title is-2">Agregar usuario</span>
      </div>
    </nav>
    <form @submit.prevent="validate">
      <b-collapse class="card" animation="slide" aria-id="contentIdFor">
        <template #trigger="props">
          <div class="card-header" role="button" aria-controls="contentIdFor">
            <p class="card-header-title">Datos de usuario</p>
            <a class="card-header-icon">
              <b-icon :icon="props.open ? 'menu-down' : 'menu-up'"> </b-icon>
            </a>
          </div>
        </template>

        <div class="card-content">
          <div class="content">
            <div class="columns">
              <div class="column is-4">
                <b-field>
                  <b-upload
                    v-show="!successUpload"
                    @input="selectFile"
                    drag-drop
                    expanded
                  >
                    <section class="section">
                      <div class="content has-text-centered">
                        <p>
                          <b-icon icon="upload" size="is-large"></b-icon>
                        </p>
                        <p>Drop your files here or click to upload</p>
                      </div>
                    </section>
                  </b-upload>
                  <b-image
                    class="image-is-350x210"
                    v-show="successUpload"
                    :src="imageUrl"
                    alt="A random image"
                  ></b-image>
                </b-field>
                <b-field class="file">
                  <b-upload @input="selectFile" expanded>
                    <b-button
                      type="is-primary"
                      expanded
                      v-if="uploading"
                      loading
                      >Subiendo...</b-button
                    >
                    <a v-else class="button is-primary is-fullwidth">
                      <b-icon icon="upload"></b-icon>
                      <span>Cargar foto</span>
                    </a>
                  </b-upload>
                </b-field>
              </div>
              <div class="column">
                <b-field
                  label="Nombre de usuario*"
                  :type="{ 'is-danger': errors.has('username') }"
                  :message="errors.first('username')"
                >
                  <b-input
                    v-model="model.userName"
                    name="username"
                    data-vv-as="nombre"
                    v-validate="'required'"
                    placeholder="Requerido"
                  />
                </b-field>

                <b-field
                  label="Contraseña*"
                  :type="{ 'is-danger': errors.has('password') }"
                  :message="errors.first('password')"
                >
                  <b-input
                    type="password"
                    password-reveal
                    v-model="model.password"
                    name="password"
                    data-vv-as="contraseña"
                    v-validate="'required'"
                    placeholder="Requerido"
                  />
                </b-field>

                <b-field
                  label="Rol*"
                  :type="{ 'is-danger': errors.has('role') }"
                  :message="errors.first('role')"
                >
                  <b-radio
                    v-model="model.userRole"
                    name="role"
                    data-vv-as="rol"
                    v-validate="'required'"
                    :native-value="0"
                  >
                    Administrador
                  </b-radio>
                  <b-radio
                    v-model="model.userRole"
                    name="role"
                    data-vv-as="rol"
                    v-validate="'required'"
                    :native-value="1"
                  >
                    Usuario
                  </b-radio>
                </b-field>
              </div>
            </div>
          </div>
        </div>
      </b-collapse>
      <br />
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
                  label="Apellido*"
                  :type="{ 'is-danger': errors.has('lastName') }"
                  :message="errors.first('lastName')"
                >
                  <b-input
                    v-model="model.lastName"
                    name="lastName"
                    data-vv-as="apellido"
                    v-validate="'required'"
                    placeholder="Requerido"
                  />
                </b-field>
              </div>
            </div>

            <div class="columns is-multiline">
              <div class="column">
                <b-field
                  label="Segundo nombre"
                  :type="{ 'is-danger': errors.has('middleName') }"
                  :message="errors.first('middleName')"
                >
                  <b-input
                    v-model="model.middleName"
                    name="middleName"
                    placeholder="Opcional"
                  />
                </b-field>
              </div>
              <div class="column">
                <b-field
                  label="Segundo apellido"
                  :type="{ 'is-danger': errors.has('secondLastName') }"
                  :message="errors.first('secondLastName')"
                >
                  <b-input
                    v-model="model.secondLastName"
                    name="secondLastName"
                    placeholder="Opcional"
                  />
                </b-field>
              </div>
            </div>

            <div class="columns is-multiline">
              <div class="column">
                <b-field
                  label="Fecha de nacimiento*"
                  :type="{ 'is-danger': errors.has('dob') }"
                  :message="errors.first('dob')"
                >
                  <b-datepicker
                    v-model="model.dob"
                    name="dob"
                    data-vv-as="fecha de nacimiento"
                    v-validate="'required'"
                    placeholder="dd/MM/yyyy"
                    icon="calendar-today"
                    locale="es"
                  >
                  </b-datepicker>
                </b-field>
              </div>
              <div class="column">
                <b-field
                  label="Sexo*"
                  :type="{ 'is-danger': errors.has('gender') }"
                  :message="errors.first('gender')"
                >
                  <b-radio
                    v-model="model.gender"
                    name="gender"
                    data-vv-as="sexo"
                    v-validate="'required'"
                    :native-value="0"
                  >
                    Masculino
                  </b-radio>
                  <b-radio
                    v-model="model.gender"
                    name="gender"
                    data-vv-as="sexo"
                    v-validate="'required'"
                    :native-value="1"
                  >
                    Femenimo
                  </b-radio>
                </b-field>
              </div>
            </div>

            <div class="columns is-multiline">
              <div class="column">
                <b-field
                  label="Tipo de documento*"
                  :type="{ 'is-danger': errors.has('documentType') }"
                  :message="errors.first('documentType')"
                >
                  <b-select
                    expanded
                    v-model="model.documentType"
                    placeholder="Requerido"
                    data-vv-as="tipo de documento"
                    v-validate="'required'"
                  >
                    <option :value="0">Cédula</option>
                    <option :value="1">Pasaporte</option>
                  </b-select>
                </b-field>
              </div>
              <div class="column">
                <b-field
                  label="Documento*"
                  :type="{ 'is-danger': errors.has('documentTypeValue') }"
                  :message="errors.first('documentTypeValue')"
                >
                  <the-mask
                    v-if="model.documentType === 0"
                    name="documentTypeValue"
                    data-vv-as="documento"
                    v-model="model.documentTypeValue"
                    :mask="'###-#######-#'"
                    v-validate="'required'"
                    type="text"
                    class="input"
                    :class="{ 'is-danger': errors.has('documentTypeValue') }"
                    placeholder="Requerido"
                  />

                  <b-input
                    v-else
                    v-model="model.documentTypeValue"
                    name="documentTypeValue"
                    data-vv-as="documento"
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
import { FileService } from "@/core/services";
import { User } from "@/core/model";
@Component
export default class UserAddComponent extends Mixins<BaseFormAddMixin<User>>(
  BaseFormAddMixin
) {
  fileService = new FileService();
  uploading = false;
  successUpload = false;
  errorUpload = false;
  imageUrl = require("@/assets/sx-logo.png");
  constructor() {
    super();
    this.controller = "User";
    this.model = new User();
  }

  async selectFile(selected: File) {
    try {
      this.uploading = true;
      const document = await this.fileService.upload(selected);
      this.model.photo = document.data;
      this.imageUrl = this.fileService.getPublicUrl(
        this.model.photo.fileName,
        this.model.photo.contentType
      );
      this.successUpload = true;
    } catch (error) {
      this.errorUpload = true;
      this.imageUrl = require("@/assets/sx-logo.png");
      this.operationFailed("Error al enviar foto al servidor");
    } finally {
      this.uploading = false;
    }
  }
}
</script>

<style lang="scss" scoped>
</style>
