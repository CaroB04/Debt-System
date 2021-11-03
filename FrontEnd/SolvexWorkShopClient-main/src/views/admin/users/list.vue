<template>
  <div>
    <sx-buefy-table :config="tableConfig">

      <template v-slot:fullName="{ row }">
        <div class="media">
          <div class="media-left">
            <p class="icon-image-rounded has-background-primary">{{row.signature}}</p>
          </div>
          <div class="media-content">
            <p class="title is-6">
              <b-tooltip label="Perfil no disponible">
                <a class="has-text-link cursor-ban">{{ row.fullName }}</a>
              </b-tooltip>
            </p>
            <p class="subtitle is-6">@{{ row.userName }}</p>
          </div>
        </div>
      </template>



      <template v-slot:documentType="{ row }">
        <p class="title is-6">
          {{ row.documentTypeValue | documentTypeValue(row.documentType) }}
        </p>
        <p class="subtitle is-6">{{ row.documentType | documentType }}</p>
      </template>
    </sx-buefy-table>
  </div>
</template>

<script lang="ts">
import { Component, Mixins } from "vue-property-decorator";
import { SxBuefyTableMixin } from "@/mixins";
import { BTableColumn } from "@/components/sx/sx-buefy-table/config";
import Helpers from "@/core/utils/helpers";
import { BTableColumnType, UserRole } from "@/core/utils/enums";
import { User } from "@/core/model";

@Component
export default class UserListComponent extends Mixins<
  SxBuefyTableMixin<User>
>(SxBuefyTableMixin) {
  created() {
    this.setTableConfig();
    this.formatColumns();
  }
  setTableConfig() {
    this.tableConfig.controller = "User";
    this.tableConfig.aggregatable = true;
    this.tableConfig.aggregating.title = "Listado de usuarios";
  }
  formatColumns() {
    let fullName = new BTableColumn("fullName", "Usuario");
    fullName.sortable = false;
    fullName.customTemplate = true;

    let documentType = new BTableColumn("documentType", "Identificación");
    documentType.customTemplate = true;

    let role = new BTableColumn("userRole", "Rol");
    role.type = BTableColumnType.Custom;
    role.customFilter = (value: UserRole) => Helpers.Filters.UserRole(value);

    this.tableConfig.insertColumns(fullName, documentType, role);
  }
}
</script>

