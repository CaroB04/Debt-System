import { Base } from '@/core/model/base.model';
import { BaseService } from '@/core/services';
import { Component, Mixins } from 'vue-property-decorator';
import { CoreMixin } from './core.mixin';
    
@Component
export class BaseFormAddMixin<T extends Base> extends Mixins(CoreMixin) {
    service!: BaseService<T>;
    model!: T;
    controller!: string;
    saving = false;
    exitOnSave = true;
    modelInit!: T;
    
    created(){
        this.service = new BaseService<T>(this.controller);
        this.modelInit = this.map<T>(this.model);
    }
    get modelDoNotChange() {
        return this.equals(this.modelInit, this.model);
    }

    async validate() {
        let result = await this.$validator.validateAll();
        if (!result){
            const errors =  this.$validator.errors.items.map(e => e.msg);
            this.operationFailedErrors(errors);
            return;
        }
        this.add();
    }
    async add() {

        try {
            this.saving = true;
            let result = await this.service.post(this.model);
            this.saving = false;
            this.$emit('add', result);
            this.clean();
            this.operationSuccess();

            if (this.exitOnSave)
                this.confirmedCancel();

        } catch (errors) {
            // this.operationFailedErrors(errors);
        } finally {
            this.saving = false;
        }
    }

    clean() {
        setTimeout(() => this.$validator.errors.clear(), 10);
        this.model = this.map<T>(this.modelInit);
    }
    cancel() {
        if (this.modelDoNotChange) {
            this.confirmedCancel();
            return;
        }

        this.$buefy.dialog.confirm({
            title: 'Advertencia',
            type: 'is-warning',
            size: 'is-medium',
            hasIcon: true,
            confirmText: 'Salir',
            cancelText: 'No',
            message: '¿Seguro que desea salir de la sección?',
            onConfirm: () => this.confirmedCancel()
        })

    }

    confirmedCancel() {
        this.model = this.map<T>(this.modelInit);
        this.$router.push(`${this.$router.currentRoute.path.split('/add')[0]}`);
    }
}