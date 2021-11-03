import { Base } from "@/core/model/base.model";

export default class BTableConfigPagination<T extends Base> {
    public backend: boolean;
    public simple: boolean;
    public total: number;
    public perPage: number;
    public currentPage: number;
    public sizeCss: string;
    public labels: BTableConfigPaginationLabel;
    public quantityOptions: number[];
    public quantityText: string;

    constructor() {
        this.backend = true;
        this.simple = false;
        this.total = 0;
        this.perPage = Number.parseFloat(process.env.VUE_APP_PAGE_SIZE as any) || 10;
        this.currentPage = 1;
        this.sizeCss = '';
        this.quantityText = 'Cantidad';
        this.labels = new BTableConfigPaginationLabel();
        this.quantityOptions = [5,10,20,50,100]
    }
    public change?: (page: number) => void;
    public onChanged?: (data: T[]) => void;
}

export class BTableConfigPaginationLabel {
    public next: string;
    public previous: string;
    public page: string;
    public current: string;
    constructor() {
        this.next = 'Siguiente';
        this.previous = 'Anterior';
        this.page = 'Página';
        this.current = 'Página actual';
    }
}