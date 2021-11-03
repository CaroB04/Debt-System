import { WorkShopDayMode, WeekDay } from "@/core/utils/enums";
import { BaseEntity } from "./base.model";

export class WorkShopDay extends BaseEntity {
    day: WeekDay = WeekDay.MONDAY;
    mode: WorkShopDayMode = WorkShopDayMode.ON_SITE;
    modeLocation: string = '';
    startHour: Date | null = null
    endHour?: Date | null = null
    workShopId: number = 1;
}