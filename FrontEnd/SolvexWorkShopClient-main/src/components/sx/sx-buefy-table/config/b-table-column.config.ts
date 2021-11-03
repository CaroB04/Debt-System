import { BTableColumnType } from '@/core/utils/enums';

export class BTableColumn {
    public width?: string;
    public visible?: boolean;
    public numeric?: boolean;
    public centered?: boolean;
    public customKey?: string;
    public customSort?: ()=>void;
    public meta?: string;
    public customTemplate?: boolean;
    public templateSlot?: string;
    public order?: number;
    public type?: BTableColumnType;
    constructor(
        public field: string,
        public label?: string, public sortable: boolean = true) { 
        this.label = this.label || this.field;     
        this.visible = true;
    }
    public customFilter!: (value:any)=> string;
}

export default class BTableConfigColumnAction {
    public active: boolean;
    public customTemplate: boolean;
    public props: BTableColumn;
    
    constructor() {
        this.active = true;
        this.customTemplate = false;
        this.props = new BTableColumn('btn-action', 'Acciones');
        this.props.sortable = false;
        this.props.centered = true;
    }
}

