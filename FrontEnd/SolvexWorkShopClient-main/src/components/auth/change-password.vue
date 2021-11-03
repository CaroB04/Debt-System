<template>
  <b-modal
    @close="close"
    :active="active"
    :can-cancel="['x']"
    has-modal-card
    trap-focus
    :destroy-on-hide="false"
    aria-role="dialog"
    aria-label="Example Modal"
    aria-modal
  >
    <template #default>
      <form action="">
        <div class="modal-card" style="width: auto">
          <header class="modal-card-head">
            <p class="modal-card-title">Cambiar contraseña</p>
            <button type="button" class="delete" @click="$emit('close')" />
          </header>
          <section class="modal-card-body">
            <b-field
              label="Nueva contraseña*"
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
                ref="password"
              />
            </b-field>

            <b-field
              label="Confirmar contraseña*"
              :type="{ 'is-danger': errors.has('confirmPassword') }"
              :message="errors.first('confirmPassword')"
            >
              <b-input
                type="password"
                password-reveal
                v-model="model.confirmPassword"
                name="confirmPassword"
                data-vv-as="confirmar contraseña"
                v-validate="'confirmed:password'"
                placeholder="Requerido"
              />
            </b-field>
          </section>
          <footer class="modal-card-foot">
            <b-button label="Cerrar" @click="close" />
            <b-button
              :loading="loading"
              :disabled="loading"
              @click="validate"
              label="Cambiar contraseña"
              type="is-primary"
            />
          </footer>
        </div>
      </form>
    </template>
  </b-modal>
</template>

<script lang="ts">
import { Component, Mixins, Prop, Vue, Watch } from "vue-property-decorator";
import { AuthMixin, CoreMixin } from "@/mixins";
import { IUserService, UserService } from "@/core/services";
import { ChangePassword } from "@/core/model";
@Component
export default class ChangePasswordComponent extends Mixins(
  AuthMixin,
  CoreMixin
) {
  @Prop() public isActive!: boolean;
  @Prop() public userId!: number;

  model = new ChangePassword();
  userService: IUserService = new UserService("user");
  active = false;
  loading = false;

  @Watch("isActive")
  onIsActiveChange(value: boolean) {
    this.active = value;
    this.model = new ChangePassword();
  }
  close() {
    this.$emit("update:isActive", false);
  }

  async validate() {
    let result = await this.$validator.validateAll();
    if (!result) {
      const errors = this.$validator.errors.items.map((e) => e.msg);
      this.operationFailedErrors(errors);
      return;
    }
    this.changePassword();
  }

  async changePassword() {
    try {
      this.loading = true;
      const response = await this.userService.changePassword(
        this.userId,
        this.model
      );
      if (response.data.isSuccess) {
        this.operationSuccess("Contraseña cambiada exitosamente");
        this.close();
      }
    } catch (error) {
      this.operationFailed("Error al cambiar contraseña");
    } finally {
      this.loading = false;
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
</style>
