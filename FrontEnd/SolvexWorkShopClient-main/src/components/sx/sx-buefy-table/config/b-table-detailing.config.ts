import { Base } from "@/core/model/base.model";

export default class BTableConfigDetailing<T extends Base> {
    public showDetailIcon: boolean;
    public openedDefault: number[];
    public key: string;

    constructor() {
        this.key = 'id';
        this.showDetailIcon = true;
        this.openedDefault = [];
    }
    public visibility!: (row: T) => void;
    public open!: (row: T) => void;
    public close!: (row: T) => void;

}