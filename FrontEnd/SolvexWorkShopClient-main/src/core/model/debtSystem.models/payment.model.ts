import { BaseEntity } from "../base.model";

export interface PaymentModel extends BaseEntity {
    loanEntityId: number;
    voucher: string;
    dateRealization: string | null;
    setedDate: string;
    amount: number;
    done: boolean;
}

