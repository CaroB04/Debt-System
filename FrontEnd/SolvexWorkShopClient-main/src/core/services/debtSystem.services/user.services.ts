import { IEntityOperationResult } from "../../infraestructure/abstract";
import axios, { AxiosResponse } from "axios";
import {UserModel, UserModelGet} from "../../model/debtSystem.models/user.model";
import { IBaseService, BaseService} from "../base.service";
import { PaymentService } from "./payment.service";

export interface IUserService extends IBaseService<UserModel>{
}

export class UserService extends BaseService<UserModel> implements IUserService{
    constructor(controller : string)
    {
        super(controller);
    }

    public async get(){
        const response = await axios.get<UserModelGet>(this.apiUrl);
        return response;
    }

    public async edit( data: UserModel){
        const response = await axios.put<IEntityOperationResult<UserModel>>(this.apiUrl, data);
        return response;
    }
   
}