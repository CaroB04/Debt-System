import { Base } from "@/core/model/base.model";

export default class BTableConfigChecking<T extends Base> {
    public rows: T[];
    public header: boolean;
    constructor() {
        this.rows = [];
        this.header = true;
    }
    public customIsChecked(tableRow: T, dataValue: T) {
        if (tableRow && dataValue){
            return !!tableRow.id && tableRow.id === dataValue.id
        }
        return tableRow === dataValue;
    }
    public check!: (list: T[], row: T) => void;
    public checkAll!: (list: T[]) => void;
    public isCheckableRow(row: T): boolean {
        return true;
    };
}