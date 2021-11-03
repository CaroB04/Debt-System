import { BaseEntity } from "./base.model";

export class WorkShop extends BaseEntity {
    name: string = '';
    description: string = '';
    startDate: Date | null | string = null;
    endDate?: Date | null | string = null;
    contentSupport: string = '';
}
