export class DriverInfo {
    constructor()
    {
        this.LicenceExpireDate=new  Date().toISOString().slice(0,10);
    }

    DriverInfoId: number;
    EmployeeId: number;
    EmployeeName: string;
    DrivingLicenceNo: number;
    LicenceExpireDate: string;
    DutyStart: string;
    DutyEnd: string;
    OtherInfo: string;

}
