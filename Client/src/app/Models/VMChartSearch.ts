export class VmRequsationFilter
    {
        constructor(){
            this.DriverID=0,
            this.VehicleID=0
            this.FromDate=new  Date().toISOString().slice(0,10);

            var someDate = new Date();
            var numberOfDaysToAdd = 7;
            someDate.setDate(someDate.getDate() + numberOfDaysToAdd); 
             this.ToDate=new  Date(someDate).toISOString().slice(0,10);

        }
        FromDate: string ;
        ToDate: string ;
        DriverID: number;
        VehicleID: number;
        EmployeeID:number;
        PageIndex:number;
        Status:number;
        PageSize:number;
        Name:string

    }