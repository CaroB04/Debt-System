<template>
  <section>
    <nav v-if="config.aggregatable" class="level">
      <!-- Left side -->
      <div class="level-left">
        <div class="level-item">
          <p class="subtitle is-4">{{ config.aggregating.title }}</p>
        </div>
      </div>

      <!-- Right side -->
      <div class="level-right">
        <p class="level-item">
          <b-button type="is-primary" @click="add">{{
            config.aggregating.btnText
          }}</b-button>
        </p>
      </div>
    </nav>
    <slot v-else name="nav-table"></slot>
    <hr v-if="config.aggregatable" />
    <b-table
      :custom-row-key="config.customRowKey"
      :loading="config.loading"
      :data="config.data"
      :selected.sync="selectableColumn"
      :focusable="config.focusable"
      @select="select"
      :checkable="config.checkable"
      :checked-rows.sync="config.checking.rows"
      :is-row-checkable="config.checking.isCheckableRow"
      @check="check"
      @check-all="checkAll"
      :custom-is-checked="config.checking.customIsChecked"
      :header-checkable="config.checking.header"
      :name="config.ref"
      :detailed="config.detailed"
      :showDetailIcon="config.detailing.showDetailIcon"
      :opened-detailed="config.detailing.openedDefault"
      :detail-key="config.detailing.key"
      :has-detailed-visible="config.detailing.visibility"
      @details-open="openRow"
      @details-close="closeRow"
      :paginated="config.paginated"
      :pagination-simple="config.pagination.simple"
      :backend-pagination="config.pagination.backend"
      :total="config.pagination.total"
      :per-page="config.pagination.perPage"
      :pagination-size="config.pagination.sizeCss"
      :aria-next-label="config.pagination.labels.next"
      :aria-previous-label="config.pagination.labels.previous"
      :aria-page-label="config.pagination.labels.page"
      :aria-current-label="config.pagination.labels.current"
      @page-change="pageChange"
      :backend-sorting="config.sorting.backend"
      :default-sort="[config.sorting.field, config.sorting.order]"
      :default-sort-direction="config.sorting.defaultSortDirection"
      @sort="sort"
      :bordered="config.style.bordered"
      :striped="config.style.striped"
      :narrowed="config.style.narrowed"
      :hoverable="config.style.hoverable"
      :mobile-cards="config.style.mobileCards"
      :icon-pack="config.style.iconPack"
      :row-class="config.rowClass"
      @click="click"
      @dblclick="dblclick"
      @contextmenu="rightClick"
    >
      <b-table-column
        v-for="(column, index) in config.columns"
        :key="index"
        :field="column.field"
        :label="column.label"
        :visible="column.visible"
        :width="column.width"
        :numeric="column.numeric"
        :centered="column.centered"
        :sortable="column.sortable"
        :customKey="column.customKey"
        :customSort="column.customSort"
        :meta="column.meta"
        v-slot="props"
      >
        <slot
          v-if="column.customTemplate"
          :name="column.templateSlot || column.field"
          :row="props.row"
          :index="index"
          :column="column"
        ></slot>
        <span v-else>
          <b-tooltip
            type="is-primary"
            v-if="column.type === 2"
            :label="getValueTime(props.row, column.field)"
          >
            <b-tag rounded type="is-light">{{
              getFieldValue(props.row, column.field) | date
            }}</b-tag>
          </b-tooltip>
          <b-tooltip
            v-else-if="column.type === 3"
            :label="getValueDate(props.row, column.field)"
          >
            <b-tag rounded type="is-light">{{
              getFieldValue(props.row, column.field) | time
            }}</b-tag>
          </b-tooltip>
          <span v-else>{{
            getFieldValue(props.row, column.field)
              | bTableColumnValue(column, props)
          }}</span>
        </span>
      </b-table-column>
      <b-table-column
        v-if="config.actions.active"
        :field="config.actions.props.field"
        :label="config.actions.props.label"
        :visible="config.actions.props.visible"
        :width="config.actions.props.width"
        :numeric="config.actions.props.numeric"
        :centered="config.actions.props.centered"
        :sortable="config.actions.props.sortable"
        :customKey="config.actions.props.customKey"
        :customSort="config.actions.props.customSort"
        :meta="config.actions.props.meta"
        v-slot="props"
      >
        <slot
          v-if="config.actions.customTemplate"
          name="actions"
          :row="props.row"
        ></slot>
        <div
          v-else
          class="buttons"
          :class="{ 'is-centered': config.actions.props.centered }"
        >
          <b-button
            v-show="config.editable"
            @click="edit(props.row.id, props.row, props)"
            :disabled="!config.editing.isEditableRow(props.row)"
            :outlined="config.editing.outlined"
            :type="config.editing.type"
            :icon-right="config.editing.icon"
          />
          <b-button
            v-show="config.removable"
            :disabled="!config.removing.isRemovableRow(props.row)"
            @click="remove(props.row.id, props.row, props)"
            :outlined="config.removing.outlined"
            :type="config.removing.type"
            :icon-right="config.removing.icon"
          />
        </div>
      </b-table-column>
      <template slot="empty">
        <section v-if="!config.customEmptyTemplate" class="section">
          <div class="content has-text-grey-light has-text-centered">
            <p>
              <b-icon icon="database" size="is-large"></b-icon>
            </p>
            <p>{{ config.emptyText }}</p>
            <hr v-show="config.showReload" />
            <b-button
              v-show="config.showReload"
              @click="config.getData"
              type="is-primary"
              icon-left="reload"
              outlined
              rounded
              >{{ config.reloadText }}</b-button
            >
          </div>
        </section>
        <slot v-else name="empty"></slot>
      </template>
      <template slot="bottom-left">
        <section v-if="!config.customButtomLeftTemplate">
          <div class="content has-text-grey-light">
            <div class="field is-horizontal">
              <div class="field-body">
                <div class="field">
                  <label class="has-text-dark"
                    >{{ config.pagination.quantityText }}:</label
                  >
                </div>
                <div class="field">
                  <!---->
                  <div class="control is-expanded">
                    <b-select
                      v-model="config.pagination.perPage"
                      v-show="config.paginated && config.pagination"
                      expanded
                    >
                      <option
                        :key="index"
                        v-for="(item, index) in config.pagination
                          .quantityOptions"
                        :value="item"
                      >
                        {{ item }}
                      </option>
                    </b-select>
                    <!---->
                  </div>
                  <!---->
                </div>
              </div>
              <!---->
            </div>
          </div>
        </section>
        <slot v-else name="bottom-left"></slot>
      </template>
      <template #detail="props">
        <slot name="detail" :row="props.row"></slot>
      </template>
    </b-table>

    <b-modal
      :on-cancel="onModalClose"
      :can-cancel="config.aggregating.modal.canCancel"
      :width="config.aggregating.modal.width"
      :fullScreen="config.aggregating.modal.fullScreen"
      :active.sync="config.aggregating.modal.active"
      has-modal-card
      trap-focus
      aria-role="dialog"
      aria-modal
    >
      <div class="modal-card">
        <header class="modal-card-head">
          <p class="modal-card-title">{{ config.aggregating.modal.title }}</p>
        </header>
        <section class="modal-card-body">
          <slot name="modal-content"></slot>
        </section>
        <footer class="modal-card-foot justify-content-space-between">
          <b-button @click="onModalRestart">{{
            config.aggregating.modal.btnRestartText
          }}</b-button>
          <div class="buttons">
            <b-button type="is-danger" @click="onModalClose">{{
              config.aggregating.modal.btnCloseText
            }}</b-button>
            <b-button type="is-primary" @click="onModalSave">{{
              config.aggregating.modal.btnAddText
            }}</b-button>
          </div>
        </footer>
      </div>
    </b-modal>

    <b-modal
      :on-cancel="onModalClose"
      :can-cancel="config.editing.modal.canCancel"
      :width="config.editing.modal.width"
      :fullScreen="config.editing.modal.fullScreen"
      :active.sync="config.editing.modal.active"
      has-modal-card
      trap-focus
      aria-role="dialog"
      aria-modal
    >
      <div class="modal-card">
        <header class="modal-card-head">
          <p class="modal-card-title">{{ config.editing.modal.title }}</p>
        </header>
        <section class="modal-card-body">
          <slot name="modal-content"></slot>
        </section>
        <footer class="modal-card-foot justify-content-space-between">
          <b-button @click="onModalRestart">{{
            config.editing.modal.btnRestartText
          }}</b-button>
          <div class="buttons">
            <b-button type="is-danger" @click="onModalClose">{{
              config.editing.modal.btnCloseText
            }}</b-button>
            <b-button type="is-primary" @click="onModalSave">{{
              config.editing.modal.btnAddText
            }}</b-button>
          </div>
        </footer>
      </div>
    </b-modal>
  </section>
</template>

<script lang='ts' >
import Helpers from "@/core/utils/helpers";
import { Component, Mixins, Prop } from "vue-property-decorator";
import { Watch } from "vue-property-decorator";
import { BTableColumnType } from "@/core/utils/enums";
import { CoreMixin } from "@/mixins";
import { RowTable } from "@/core/typings/b-row-table";
import { BaseEntity, IBase, IBaseEntity } from "@/core/model/base.model";
import { BTableColumn, BTableConfig, BTableConfigPagination } from "./config";
@Component({})
export default class SxBuefyTableComponent extends Mixins(CoreMixin) {
  @Prop() public config!: BTableConfig<IBaseEntity>;
  public isEditing = false;

  created() {
    if (this.config.enableDefaultColumns) this.insertDefaultColumns();
  }
  mounted() {
    this.sortColumns();

    if (this.config.loadOnMount) this.config.getData();
  }

  get selectableColumn() {
    return this.config.selectable ? this.config.selection.selected : null;
  }

  set selectableColumn(object: IBaseEntity | null) {
    this.config.selection.selected = object;
  }

  insertDefaultColumns() {
    let fecha = new BTableColumn("createdDate", "Fecha");
    fecha.type = BTableColumnType.DateTime;
    fecha.order = 200;
    this.config.insertColumns(fecha);
  }

  add() {
    this.$emit("add");
    if (this.config.aggregating.add) {
      this.config.aggregating.add();
    } else {
      if (this.config.aggregating.isModal) {
        this.config.aggregating.modal.active = true;
      } else {
        this.$router.push(
          `${this.$route.path}/${this.config.aggregating.routeTo}`
        );
      }
    }
  }

  edit(id: number, row: IBaseEntity, props: RowTable<IBaseEntity>) {
    this.$emit("edit", id, row, props);
    this.isEditing = true;
    if (this.config.editing.edit) {
      this.config.editing.edit(id, row, props);
    } else {
      if (this.config.editing.isModal) {
        this.config.editing.modal.model = this.copy(row) as any;
        this.config.editing.modal.modelId = id;
        this.config.editing.modal.active = true;
      } else {
        this.$router.push(
          `${this.$router.currentRoute.path}/${this.config.editing.routeTo}/${id}`
        );
      }
    }
  }
  remove(id: number, row: IBaseEntity, props: RowTable<IBaseEntity>) {
    this.$emit("delete", id, row, props);
    if (this.config.removing.remove) {
      this.config.removing.remove(id, row, props);
    } else {
      if (this.config.removing.showConfirm) {
        this.config.removing.setupDefaultConfirm(() =>
          this.sendRemove(id, row, props)
        );
        this.$buefy.dialog.confirm(this.config.removing.confirm);
      } else {
        this.$router.push(
          `${this.$router.currentRoute.path}/${this.config.removing.routeTo}/${id}`
        );
      }
    }
  }
  async sendRemove(id: number, row: object, props: RowTable<object>) {
    try {
      await this.config.apiService.delete(id);
      this.$emit("removed", id, row, props);
      this.operationSuccess(this.config.removing.successText);
      this.config.getData();
    } catch (e) {
      this.operationFailed(e);
    }
  }
  async sendAdd(model: IBaseEntity) {
    try {
      var isValid = await this.$validator.validateAll();
      if (isValid) {
        this.config.aggregating.saving = true;
        let result = await this.config.apiService.post(model);
        this.config.aggregating.saving = false;
        this.$emit("add", result);
        this.operationSuccess();

        if (this.config.aggregating.modal.closeOnSave) this.onModalClose();
        return;
      }
      throw this.$validator.errors.items.map((e) => e.msg);
    } catch (errors) {
      this.operationFailedErrors(errors);
    }
  }
  async sendEdit(id: number, model: IBaseEntity) {
    try {
      var isValid = await this.$validator.validateAll();
      if (isValid) {
        this.config.editing.modal.saving = true;
        var result = await this.config.apiService.put(id, model);
        this.config.editing.modal.saving = false;
        this.$emit("edit", result);
        this.operationSuccess();
        if (this.config.editing.modal.closeOnSave) this.onModalClose();
        return;
      }

      throw this.$validator.errors.items.map((e) => e.msg);
    } catch (errors) {
      this.operationFailedErrors(errors);
    }
  }
  onModalRestart() {
    this.$emit("restart");
    if (this.isEditing) {
      if (this.config.editing.modal.restart)
        this.config.editing.modal.restart();
      this.config.editing.modal.model = new BaseEntity();
    } else {
      if (this.config.aggregating.modal.restart)
        this.config.aggregating.modal.restart();

      this.config.aggregating.modal.model = new BaseEntity();
    }
  }
  onModalClose() {
    if (this.isEditing) {
      this.config.editing.modal.active = false;
      this.$emit("modal-close");
      if (this.config.editing.modal.close) this.config.editing.modal.close();
    } else {
      this.config.aggregating.modal.active = false;

      this.$emit("modal-close");
      if (this.config.aggregating.modal.close)
        this.config.aggregating.modal.close();
    }
    this.isEditing = false;
  }
  onModalSave() {
    if (this.isEditing) {
      if (this.config.editing.modal.save) this.config.editing.modal.save();
      else {
        let model = this.copy(this.config.editing.modal.model);
        let id = this.config.editing.modal.modelId;
        this.sendEdit(id, model);
      }
      this.$emit("edited", this.config.editing.modal.model);
      if (this.config.editing.modal.closeOnSave) {
        this.onModalClose();
      }
    } else {
      this.$emit("added", this.config.aggregating.modal.model);
      if (this.config.aggregating.modal.save)
        this.config.aggregating.modal.save();
      else {
        let model = this.copy(this.config.aggregating.modal.model);
        this.sendAdd(model);
      }
      if (this.config.aggregating.modal.closeOnSave) {
        this.onModalClose();
      }
    }
    this.isEditing = false;
  }
  sortColumns() {
    this.config.columns = this.config.columns.sort(
      (a: BTableColumn, b: BTableColumn) => {
        if (a.order === undefined) a.order = this.config.columns.indexOf(a);

        if (b.order === undefined) b.order = this.config.columns.indexOf(b);

        return a.order - b.order;
      }
    );
  }
  getValueTime(row: IBase, value: string) {
    let val = this.getFieldValue(row, value);
    return Helpers.Filters.Time(val);
  }
  getValueDate(row: IBase, value: string) {
    let val = this.getFieldValue(row, value);
    return Helpers.Filters.Date(val);
  }
  getFieldValue(row: IBase, field: string) {
    return Helpers.GetValueFromPath(row, field);
  }

  //  WATCHES
  @Watch("config.columns", { deep: true })
  onColumnsChange(columns: IBaseEntity[]) {
    this.$emit("columns-change", columns);
  }
  @Watch("config.data", { deep: true })
  onRowsChange(data: IBaseEntity[]) {
    this.$emit("rows-change", data);
  }
  @Watch("config.pagination", { deep: true })
  onPaginationChange(value: BTableConfigPagination<IBaseEntity>) {
    this.$emit("page-change", value);
  }
  //  SELECTION
  select(row: IBaseEntity, oldRow: IBaseEntity) {
    if (this.config.selectable === false) {
      this.config.selection.selected = null;
      return;
    }

    if (!this.config.selection.change) {
      this.$emit("select", row, oldRow);
    } else this.config.selection.change(row, oldRow);
  }

  //  DETAILS
  public openRow(row: IBaseEntity) {
    if (!this.config.detailing.open) {
      this.$emit("details-open", row);
    } else this.config.detailing.open(row);
  }
  public closeRow(row: IBaseEntity) {
    if (!this.config.detailing.close) {
      this.$emit("details-close", row);
    } else this.config.detailing.close(row);
  }
  //  PAGINATION
  public pageChange(page: number) {
    if (!this.config.pagination.change) {
      this.$emit("page-change", page);
      this.config.pagination.currentPage = page;
      if (this.config.paginated && this.config.pagination.backend)
        this.config.getData(this.config.pagination.onChanged);
    } else {
      this.config.pagination.change(page);
      if (this.config.pagination.onChanged)
        this.config.pagination.onChanged(this.config.data);
    }
  }
  //  SORTING
  public sort(field: string, order: string) {
    if (!this.config.sorting.change) {
      this.config.sorting.field = field;
      this.config.sorting.order = order;
      this.$emit("sort", field, order);

      if (this.config.sorting.backend) this.config.getData();
    } else this.config.sorting.change(field, order);
  }
  //  CHECKING
  public check(list: IBaseEntity[], row: IBaseEntity) {
    if (!this.config.checking.check) {
      this.$emit("check", list, row);
    } else this.config.checking.check(list, row);
  }
  public checkAll(list: IBaseEntity[]) {
    if (!this.config.checking.checkAll) {
      this.$emit("checkAll", list);
    } else this.config.checking.checkAll(list);
  }
  //  CLICKING
  public click(row: IBaseEntity) {
    if (!this.config.click) {
      this.$emit("click", row);
    } else this.config.click(row);
  }
  public dblclick(row: IBaseEntity) {
    if (!this.config.dblclick) {
      this.$emit("dblclick", row);
    } else this.config.dblclick(row);
  }
  public rightClick(row: IBaseEntity) {
    if (!this.config.rightClick) {
      this.$emit("rightClick", row);
    } else this.config.rightClick(row);
  }
}
</script>
