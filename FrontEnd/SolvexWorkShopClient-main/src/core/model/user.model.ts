import { DocumentType, Gender, UserRole } from "@/core/utils/enums";
import { BaseEntity } from "./base.model";
import { IDocument } from "./document.model";

export class User extends BaseEntity {
    fullName?: string = '';
    name: string = '';
    middleName?: string = '';
    lastName: string = '';
    secondLastName?: string;
    gender: Gender = Gender.MALE;
    dob?: Date | null | string = null;
    documentType: DocumentType = DocumentType.ID;
    documentTypeValue: string = '';
    photoId?: number | null;
    photo?: IDocument;
    photoFileName?: string;
    userName: string = '';
    password: string = '';
    userRole: UserRole = UserRole.ADMIN;
    signature?:string;
}

export class ChangePassword {
    password: string = '';
    confirmPassword: string = '';
}
