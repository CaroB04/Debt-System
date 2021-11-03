import axios, { AxiosResponse } from 'axios'
import { IEntityOperationResult } from "../../infraestructure/abstract";
import { IBaseService, BaseService } from "../base.service";
import { PaymentModel } from "../../model/debtSystem.models/payment.model"
import { IODataResult, ODataQuery, IODataQuery, IApiQueryResult } from "../../infraestructure/odata";

export interface IPaymentService extends IBaseService<PaymentModel> {
}

export class PaymentService extends BaseService<PaymentModel> implements IPaymentService {
    constructor(controller: string) {
        super(controller);
    }

    public async get() {
        const response = await axios.get<PaymentModel>(this.apiUrl);
        return response;
    }

    public async getByLoan(id: number | string) {
        const response =  await axios.get<IApiQueryResult<PaymentModel>>(this.apiUrl + "loan" + id);
        return response;
    }

    public async edit(data: PaymentModel) {
        const response = await axios.put<IEntityOperationResult<PaymentModel>>(this.apiUrl, data);
        return response;
    }


}

