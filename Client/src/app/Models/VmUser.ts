import { UserStatus } from "../common/Enums/Enums";

export interface VmUser {
    Username: string;
    Name: string;
    Email: string;
    MobileNo: string;
    AccessRight: string;
    Status: UserStatus;
}