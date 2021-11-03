export enum UserRole {
    ADMIN,
    USER
}
export enum DocumentType {
    ID,
    PASSPORT
}

export enum Gender {
    MALE,
    FEMALE
}

export enum WeekDay {
    MONDAY,
    TUESDAY,
    WEDNESDAY,
    THURSDAY,
    FRIDAY,
    SATURDAY,
    SUNDAY
}

export enum WorkShopDayMode {
    ON_SITE,
    VIRTUAL
}

export enum WorkShopMemberRole {
    STUDENT,
    TEACHER
}

export enum BTableColumnType{
    Date = 0,
    Time = 1,
    DateTime = 2,
    TimeDate = 3,
    Bool = 4,
    Custom = 5
}

export enum ODataConst{
    context = '@odata.context',
    count = "@odata.count"
}
