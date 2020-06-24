import { Component, OnInit } from '@angular/core';
import { Vehicle } from 'src/app/Models/Vehicle';
import { VmRequsationFilter } from 'src/app/Models/VMChartSearch';
import { RequisitionService } from 'src/app/services/requisitionService';
import { NotificationService } from 'src/app/services/notification.service';
import { DriverService } from 'src/app/api-services/driverService';



import { VMQueryObject } from '../../../Models/VMQueryObject';
import { GanttChartVM } from '../../../Models/RequsationChartVm';
import { DriverInfo } from '../../../Models/DriverInfo';
@Component({
  selector: 'app-driver-dashboard',
  templateUrl: './driver-dashboard.component.html',
  styleUrls: ['./driver-dashboard.component.scss']
})
export class DriverDashboardComponent implements OnInit {

  queryObj: VmRequsationFilter = new VmRequsationFilter();
  lstTwentyNumber = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23];
  lst: DriverInfo[]=[];
   lstRequisition : Array<GanttChartVM> = new Array<GanttChartVM>();
  constructor(
    private reqservice:RequisitionService,
    private driverservice:DriverService,
    private notificationService:NotificationService
  ) { }

  ngOnInit() {
    this.getDriver();
  }

  getDriver(){
    this.driverservice.getDriverInfo().subscribe((res:any) => {this.lst=res;});
  }

  SearchByDateRange(filter:VMQueryObject) {
    this.lstRequisition=new Array<GanttChartVM>(); 
        this.reqservice.GetDriverSchedule(filter).subscribe((res:any) => {
          this.lstRequisition =res;
          console.log(res);
          if(this.lstRequisition.length==0){
         

            this.notificationService.error("Item couldn't found")
          }
          
    });
  }

  CheckToDateWithFromDate(ToDateTime,FromDateTime){

    console.log(FromDateTime,ToDateTime)
  
    if(FromDateTime>ToDateTime){
      this.notificationService.error("To Time Must be greter than From Time")
    }
  }

  getPercentage(i) {
    return i * 4.17;
  }

  getColor(i) {
    if(i % 2) {
      return '#b54747'
    }
    return '#0d5092';
  }


  GetInfo(from , to) {
    const text =  `From: ${ from}, To: ${to}`;
    return text;
  }

  GetLeft(from) {
    const unixtime = new Date(from).getTime() / 1000;
    const unixtimestart = new Date(new Date(from).toDateString()).getTime() /1000;
    const minute = (unixtime - unixtimestart) / 60;

    const final = (100 * minute) / 1440;
    return final;
  }

  GetSize(from, to) {
    console.log("from", from);
    const fromUnix = new Date(from).getTime() / 1000;
    const toUnix = new Date(to).getTime() / 1000;
    console.log("to", to );
    const minute = (toUnix - fromUnix) / 60;

    const final = (100 * minute) / 1440;
    return final;
  }

  GetHeight() {
    return 180 * this.lstRequisition.length;
  }

}
