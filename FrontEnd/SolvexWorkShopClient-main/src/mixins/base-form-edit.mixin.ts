import { Base } from '@/core/model/base.model';
import { BaseService } from '@/core/services';
import { Component, Mixins, Vue } from 'vue-property-decorator';
import { CoreMixin } from './core.mixin';
@Component
export class BaseFormEditMixin<T extends Base> extends Mixins(CoreMixin) {
    protected service!: BaseService<T>;
    protected model!: T;
    protected id!: number;
    protected controller!: string;
    protected saving = false;
    protected exitOnSave = true;
    protected loading = false;
    protected modelInit!: T;
    protected loadFromAPI = false;

    created() {
        this.id = Number.parseInt(this.$route.params.id);
        this.service = new BaseService<T>(this.controller);
        this.getData();
    }
    get modelToEdit() {
        return this.$store.state.editingEntity;
    }
    get modelDoNotChange() {
        return this.equals(this.modelInit, this.model);
    }
    protected async getData() {
        try {
            if (this.modelToEdit && !this.loadFromAPI)
                this.model = this.map<T>(this.modelToEdit);
            else {
                this.loading = true;
                const response = await this.service.getById(this.id);
                this.model = response.data;
            }
            this.modelInit = this.map<T>(this.model);
        } catch (errors) {
            // this.operationFailedErrors(errors);
        } finally {
            this.loading = false;
        }
    }
    async validate() {
        let result = await this.$validator.validateAll();
        if (!result){
            const errors =  this.$validator.errors.items.map(e => e.msg);
            this.operationFailedErrors(errors);
            return;
        }
        this.edit();
    }
    protected async edit() {
        try {
            this.saving = true;
            var result = await this.service.put(this.id, this.model);
            this.saving = false;
            this.$emit('edit', result);
            this.clean();
            this.operationSuccess();

            if (this.exitOnSave)
                this.confirmedCancel();

        } catch (error ) {
            // var errors: any[] = [];
            // if(Object.prototype.toString.call(error.data) === "[object String]"){
            //     errors.push(error.data);
            // } else if (error.data && error.data.length > 0) {
            //     var errorMessages = error.data as any[];
            //     errorMessages.forEach(x => errors.push(x.errorMessage));
            // }else{
            //     errors.push(error);
            // }
            // console.log(errors);
            // this.operationFailedErrors(errors);
        } finally {
            this.saving = false;
        }
    }

    protected clean() {
        setTimeout(() => this.$validator.errors.clear(), 10);
        this.model = this.map<T>(this.modelInit);
    }
    protected cancel() {
        if (this.modelDoNotChange) {
            this.confirmedCancel();
            return;
        }

        this.$buefy.dialog.confirm({
            title: 'Avertencia',
            type: 'is-warning',
            size: 'is-medium',
            hasIcon: true,
            confirmText: 'Salir',
            cancelText: 'No',
            message: '¿Seguro que desea salir de la sección?',
            onConfirm: () => this.confirmedCancel()
        })

    }

    protected confirmedCancel() {
        this.model = this.map<T>(this.modelInit);
        this.$router.push(`${this.$router.currentRoute.path.split('/edit')[0]}`)
    }

    destroyed() {
        if (this.modelToEdit) {
            this.$store.commit('setEditEntity', null);
        }
    }
}