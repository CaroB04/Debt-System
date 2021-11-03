import { BTableConfig } from '@/components/sx/sx-buefy-table/config';
import { Base } from '@/core/model/base.model';
import { RowTable } from '@/core/typings/b-row-table';
import { Component, Mixins, Vue } from 'vue-property-decorator';
import { CoreMixin } from './core.mixin';
@Component
export class SxBuefyTableMixin<T extends Base> extends Mixins(CoreMixin) {
    protected tableConfig = new BTableConfig<T>();
    created() {
        this.tableConfig.editing.edit = this.edit;
        this.tableConfig.removing.remove = this.remove;
    }
    protected add() {
        this.$router.push(`${this.$router.currentRoute.path}/add`)
    }
    protected view(id: number, data: T, props: RowTable<T>) {
        this.$emit('detail', data, props);
        this.$store.commit('setViewEntity', data);
        this.$router.push(`${this.$router.currentRoute.path}/detail/${id}`);
    }
    protected edit(id: number, data: T, props: RowTable<T>) {
        this.$emit('edit', data, props);
        this.$store.commit('setEditEntity', data);
        this.$router.push(`${this.$router.currentRoute.path}/edit/${id}`)
    }
    protected remove(id: number, data: T, props: RowTable<T>) {
        this.$buefy.dialog.confirm({
            title: 'Confirmación',
            message: '¿Desea eliminar este registro?',
            confirmText: 'Adelante!',
            cancelText: 'Cancelar',
            type: 'is-danger',
            size: 'is-medium',
            hasIcon: true,
            onConfirm: () => this.confirmRemove(id, data, props)
        })
    }

    protected async confirmRemove(id: number, data: T, props: RowTable<T>) {
        try {
            await this.tableConfig.apiService.delete(id);
            this.operationSuccess('Registro eliminado!');
            this.tableConfig.getData();
            this.$emit('remove', id, data, props);
        } catch (e) {
            // this.operationFailed(e);
        }
    }
}