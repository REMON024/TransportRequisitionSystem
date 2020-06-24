import { Pagination } from "./Pagination";
 export class VMAdvancedReport implements Pagination{
     constructor()
     {
       this.EmployeeID=0;
       this.DriverID=0;
       this.VehicleID=0;
       this.FromDate=new  Date().toISOString().slice(0,10);
       this.ToDate=new  Date().toISOString().slice(0,10);

       
     }
     EmployeeID:number;
     DriverID:number;
     VehicleID:number;
     FromDate:string;
     ToDate:string;
     PageIndex: number;
    PageSize: number;


 }