import { BaseEntity } from "../base.model";
import {UserModel} from "./user.model";

// export interface ILoanModel extends BaseEntity {
//     lenderId: number;
//     debtorId: number;
//     amount: number;
//     startDate: string | null;
//     creationDate: string;
//     term: string;
//     amountPayments: number;
//     status: string;
//     amountPerPayment: number;
//     paymentsCreated: boolean;
// }

export class LoanModel extends BaseEntity {
    lenderId: number = 0;
    debtorId: number = 0;
    amount: number = 0;
    startDate: string | null = null;
    creationDate: string = '';
    term: string = '';
    amountPayments: number = 0;
    status: string = '';
    amountPerPayment: number = 0;
    paymentsCreated: boolean = false;
}

export interface LoanModelGet extends BaseEntity {
    lender: UserModel;
    debtor: UserModel;
    amount: number;
    startDate: string | null;
    creationDate: string;
    term: string;
    amountPayments: number;
    status: string;
    amountPerPayment: number;
    paymentsCreated: boolean;
}