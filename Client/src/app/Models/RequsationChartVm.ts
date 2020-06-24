export class GanttChartVM
{
    Date: Date | string;
    lstSchedule: RequsationChartView[];
}

export class RequsationChartView
{
    FromTime: Date | string;
    ToTime: Date | string;
    VehicleID: number;
    DriverID: number | null;
    DriverName:string;
    VehicleName:string;
}