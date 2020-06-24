import { Pagination } from "./Pagination";
export class VMQueryObject implements Pagination {
    constructor() {
        this.EmployeeID = 0;
       this.FromDate=new  Date().toISOString().slice(0,10);
       this.ToDate=new  Date().toISOString().slice(0,10);
    
    }
    FromDate: string;
    ToDate: string;
    EmployeeID: number;
    Status: number;
    PageIndex: number;
    PageSize: number;
    VehicleID: number;
    DriverID: number;

    Name:string
}