<template>
  <div class="section">
    <router-view></router-view>
  </div>
</template>
<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
@Component
export default class BaseView extends Vue {}
</script>


