import { BaseEntity } from "../base.model";
import {LoanModel} from "./loan.model"

export interface UserModel extends BaseEntity{
    name: string;
    identityCard: string;
    address: string;
    phoneNumber: string;
    passWord: string;
    email: string;
}

export class UserModel extends BaseEntity{
    name: string = '';
    identityCard: string = '';
    address: string = '';
    phoneNumber: string = '';
    passWord: string = '';
    email: string = '';
}

export interface UserModelGet extends BaseEntity {
    name: string;
    identityCard: string;
    address: string;
    phoneNumber: string;
    email: string;
    loansGiven: LoanModel[];
    loansTaken: LoanModel[];
}