<template>
  <section>
    <b-collapse class="card" animation="slide" aria-id="contentIdForA11y3">
      <!--Poner ciclo aquí-->
      <template #trigger="props">
        <div
          class="card-header"
          role="button"
          aria-controls="contentIdForA11y3"
        >
          <p class="card-header-title">Pago</p>
          <a class="card-header-icon">
            <b-icon :icon="props.open ? 'menu-down' : 'menu-up'"> </b-icon>
          </a>
        </div>
      </template>

      <div class="card-content">
        <div class="content">
          <div class="columns is-multiline">
            <div class="column">
              <h4>Fecha de realización</h4>
              <p>Fecha de realización</p>
            </div>

            <div class="column">
              <h4>Fecha señalada</h4>
              <p>Fecha señalada</p>
            </div>
          </div>

          <div class="columns is-multiline">
            <div class="column">
              <h4>Monto</h4>
              <p>Monto</p>
            </div>

            <div class="column">
              <h4>Confirmación</h4>
              <p>Confirmación</p>
            </div>
          </div>

          <div class="column">
            <p>
              <b-icon pack="fas" icon="check" size="is-large" type="is-success">
              </b-icon>
              Completo
            </p>
          </div>
        </div>
      </div>
      <footer class="card-footer">
        <a class="card-footer-item">Edit</a>
      </footer>
    </b-collapse>
  </section>
</template>

<style scoped>
</style>
