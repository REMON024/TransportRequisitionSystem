import { Pagination } from "./Pagination";

export class VmUserFilter implements Pagination{
    PageIndex: number;
    PageSize: number;
    Name: string;
    Username: string;
    Status: number;
}