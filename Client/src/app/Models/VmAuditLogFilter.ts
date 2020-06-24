import { Pagination } from "./Pagination";
import { Action } from "../common/Enums/Enums";

export class VmAuditLogFilter implements Pagination {
    PageIndex: number;
    PageSize: number;
    Username: string;
    Action: Action;
    FromDate: Date;
    ToDate: Date;
}