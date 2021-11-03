import axios, { AxiosResponse } from "axios";
import { BaseService, IBaseService } from "./base.service";
import { ChangePassword, User } from "../model";
import { IEntityOperationResult } from "../infraestructure/abstract";

export interface IUserService extends IBaseService<User> {
    changePassword(userId: number, model: ChangePassword): Promise<AxiosResponse<IEntityOperationResult<User>>>;
}

export class UserService extends BaseService<User> implements IUserService {

    constructor(controller: string) {
        super(controller)
    }
    
    public async changePassword(userId: number, model: ChangePassword) {
        const url = `${this.apiUrl}changePassword/${userId}`;
        const response = await axios.post(url, model)
        return response;
    }
}
