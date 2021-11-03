import { BaseService, IBaseService } from "./base.service";
import { WorkShop } from "../model/workshop.model";

export interface IWorkShopService extends IBaseService<WorkShop> {
    
}

export class WorkShopService extends BaseService<WorkShop> implements IWorkShopService {

    constructor(controller: string) {
        super(controller)
    }
}