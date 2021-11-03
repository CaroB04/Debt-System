export default class BTableConfigSorting {
    public field: string;
    public order: string;
    public defaultSortDirection: string;
    public backend: boolean;

    constructor() {
        this.backend = true;
        this.field = 'id';
        this.order = 'desc';
        this.defaultSortDirection = 'desc';
    }
    public change!: (field: string, order: string) => void;
}