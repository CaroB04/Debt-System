import { Base, BaseEntity } from "@/core/model/base.model";

export default class BTableConfigAggregating<T extends Base> {
    public title: string;
    public icon: string;
    public btnText: string;
    public btnIcon: string;
    public saving: boolean;
    public routeTo: string;
    public isModal: boolean;
    public modal!: BTableConfigAggregatingModal<T>;

    constructor() {
        this.title = "";
        this.btnText = "Agregar";
        this.icon = "format-list-text";
        this.btnIcon = "plus-circle";
        this.routeTo = "add";
        this.isModal = false;
        this.saving = false;
        this.modal = new BTableConfigAggregatingModal<T>();
    }
    public add?(): void;
}

export class BTableConfigAggregatingModal<T extends Base> {
    public model: T | Base;
    public title: string;
    public active: boolean;
    public fullScreen: boolean;
    public canCancel: any;
    public closeOnSave: boolean;
    public width: number;
    public btnCloseText: string;
    public btnRestartText: string;
    public btnAddText: string;

    constructor() {
        this.model = new BaseEntity();
        this.title = 'Nuevo';
        this.active = false;
        this.closeOnSave = true;
        this.fullScreen = false;
        this.canCancel = ['escape', 'x', 'outside'];
        this.width = 960;
        this.btnCloseText = 'Cerrar';
        this.btnAddText = 'Guardar';
        this.btnRestartText = 'Reiniciar';
    }
    public save?(): void;
    public close?(): void;
    public restart?(): void;
}