import { IBaseEntity } from "@/core/model";

export default class BTableConfigSelection<T> {
    public selected: T | IBaseEntity | null;
    constructor() {
        this.selected = null;
    }
    change!: (row: T , oldRow: T ) => void;
}