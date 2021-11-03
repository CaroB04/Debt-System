import axios, {AxiosResponse} from 'axios'
import { IEntityOperationResult } from "../../infraestructure/abstract";
import {IBaseService, BaseService} from "../base.service";
import {LoanModel, LoanModelGet} from '../../model/debtSystem.models/loan.model';

export interface ILoanService extends IBaseService<LoanModel>{
}

export class LoanService extends BaseService<LoanModel> implements IBaseService<LoanModel>
{
    constructor(controller : string){
        super(controller);
    }

    public async get(){
        const response = await axios.get<LoanModel>(this.apiUrl);
        return response;
    }

    public async getLoanById(id: string | number){
        const response = await axios.get<LoanModelGet>(this.apiUrl + id);
        return response;
    }

    public async getLoanTaken(id : string | number){
        const response = await axios.get<LoanModelGet>(this.apiUrl + "loanstaken/" + id);
        return response;
    }

    public async getLoanGiven(id : string | number){
        const response = await axios.get<LoanModelGet>(this.apiUrl + "loansgiven/" + id);
        return response;
    }

    public async edit( data: LoanModel){
        const response = await axios.put<IEntityOperationResult<LoanModel>>(this.apiUrl, data);
        return response;
    }
}