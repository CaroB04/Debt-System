import { WorkShopMemberRole } from "@/core/utils/enums";
import { BaseEntity } from "./base.model";

export class WorkShopMember extends BaseEntity {
    role: WorkShopMemberRole = WorkShopMemberRole.STUDENT;
    workShopId: number = 1;
    userId: number = 1;
}