import { Action } from "../common/Enums/Enums";


export interface VMAuditLog {
    UserId: string;
    Action: Action;
    CalledFunction: string;
    LogDescription: string;
    LogTime: Date;
    LogMessage: string;
}