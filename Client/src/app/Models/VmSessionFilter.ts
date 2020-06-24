import { Pagination } from "./Pagination";

export class VmSessionFilter implements Pagination {
    PageIndex: number;
    PageSize: number;
    IsLoggedIn: boolean | number;
    FromDate: Date;
    ToDate: Date;
    UserId: string;
}