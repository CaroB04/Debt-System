import { ODataQuery } from '@/core/infraestructure/odata';
import { Base, IBase } from '@/core/model/base.model';
import { BaseService } from '@/core/services';
import {
    BTableColumn,
    BTableConfigAggregating,
    BTableConfigChecking,
    BTableConfigColumnAction,
    BTableConfigDetailing,
    BTableConfigEditing,
    BTableConfigPagination,
    BTableConfigRemoving,
    BTableConfigSelection,
    BTableConfigSorting,
    BTableConfigStyle
} from '.';

export default class BTableConfig<T extends Base> {
    public controller!: string;
    public resource!: string;
    public $filter!: string;
    public $expand!: string;
    public data: T[];
    public columns: BTableColumn[];
    public loading: boolean;
    public enableDefaultColumns: boolean;//Enables Id, Fecha and Usuario columns
    public customRowKey: string;
    public loadOnMount: boolean;
    public showReload: boolean;
    public reloadText: string;
    public actions: BTableConfigColumnAction;
    public customEmptyTemplate!: boolean;
    public customButtomLeftTemplate!: boolean;
    public emptyText: string;
    //  EDITION
    public editable: boolean;
    public editing: BTableConfigEditing<T>;

    //  REMOVING
    public removable: boolean;
    public removing: BTableConfigRemoving<T>;

    //  AGGREGATION
    public aggregatable: boolean;
    public aggregating: BTableConfigAggregating<T>;

    //  SELECTION
    public selectable: boolean;
    public selection: BTableConfigSelection<T>;
    public focusable: boolean;

    //  CHECKING
    public checkable: boolean;
    public checking: BTableConfigChecking<T>;

    // DETAILING
    public detailed: boolean;
    public detailing: BTableConfigDetailing<T>;

    //  PAGINATION
    public paginated: boolean;
    public pagination: BTableConfigPagination<T>;

    // SORTING
    public sorting: BTableConfigSorting;

    // STYLING
    public style: BTableConfigStyle;

    //HTTP
    private service = new BaseService<T>(this.controller);


    //Methods
    public rowClass?: (row: T, index: number) => void;
    public click!: (row: T) => void;
    public dblclick!: (row: T) => void;
    public rightClick!: (row: T) => void;

    getODataQuery(): ODataQuery {
        let instace = new ODataQuery();
        instace.$orderby = this.sorting.field + (this.sorting.order === "desc" ? " desc" : "");
        instace.$skip = (this.pagination.currentPage - 1) * this.pagination.perPage;
        instace.$top = this.pagination.perPage;
        instace.$filter = this.$filter;
        instace.$expand = this.$expand;
        return instace;
    }


    constructor() {
        this.columns = [];
        this.data = [];
        this.loading = false;
        this.customRowKey = 'id';
        this.loadOnMount = true;
        this.emptyText = "No hay datos."
        this.reloadText = "Reiniciar"
        this.enableDefaultColumns = true;
        this.showReload = true;
        this.actions = new BTableConfigColumnAction();

        this.aggregatable = true;
        this.aggregating = new BTableConfigAggregating<T>();

        this.editable = true;
        this.editing = new BTableConfigEditing<T>();

        this.removable = true;
        this.removing = new BTableConfigRemoving<T>();

        this.selectable = false;
        this.focusable = false;
        this.selection = new BTableConfigSelection<T>();

        this.checkable = false;
        this.checking = new BTableConfigChecking<T>();

        this.detailed = false;
        this.detailing = new BTableConfigDetailing();

        this.paginated = true;
        this.pagination = new BTableConfigPagination();

        this.sorting = new BTableConfigSorting();

        this.style = new BTableConfigStyle();

    }
    get apiService() {
        return this.service.controller ? this.service : new BaseService<T>(this.controller);
    }
    public setRows(rows: T[], callback?: (payload: T[]) => void) {
        this.data = rows;
        if (callback) callback(this.data);
    }
    public addRows(rows: T[], callback?: (payload: T[]) => void) {
        rows.forEach(x => this.data.push(x));
        if (callback) callback(this.data);
    }
    public insertRows(...rows: T[]) {
        rows.forEach(x => this.data.push(x));
    }
    public addRow(row: T, callback?: (payload: T[]) => void) {
        this.data.push(row);
        if (callback) callback(this.data);
    }
    public setColumns(columns: BTableColumn[], callback?: (payload: BTableColumn[]) => void) {
        this.columns = columns;
        if (callback) callback(this.columns);
    }
    public addColums(columns: BTableColumn[], callback?: (payload: BTableColumn[]) => void) {
        columns.forEach(x => this.columns.push(x));
        if (callback) callback(this.columns);
    }
    public insertColumns(...columns: BTableColumn[]) {
        columns.forEach(x => this.columns.push(x));
    }
    public addColum(columns: BTableColumn, callback?: (payload: BTableColumn[]) => void) {
        this.columns.push(columns);
        if (callback) callback(this.columns);
    }
    public onLoad?: (data: T[]) => void;
    public onError?: (erorr: any) => void;

    async getData(callback?: (payload: T[]) => void) {
        this.loading = true;
        const resource = this.resource || "";
        const odataQuery = this.getODataQuery();
        try {
            const response = await this.apiService.getAll(
                resource,
                odataQuery
            );

            const value = response.data.value;

            this.setRows(value);

            if (this.pagination.backend)
                this.pagination.total = response.data.count;

            if (callback) callback(response.data.value);

            if (this.onLoad) this.onLoad(value);
        } catch (error) {
            if (this.onError) this.onError(error);
        } finally {
            this.loading = false;
        }
    }

}




