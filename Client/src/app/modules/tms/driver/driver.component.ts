import { Component, OnInit } from '@angular/core';
import { DriverInfo } from 'src/app/Models/DriverInfo';
import { AmazingTimePickerService } from 'amazing-time-picker';
import { EmployeeService } from 'src/app/api-services/employeeService';
import { Employee } from 'src/app/Models/Employee';
import { DriverService } from 'src/app/api-services/driverService';
import { NotificationService } from 'src/app/services/notification.service';
import { Constants } from 'src/app/common/Constant/AppConstant';
import { ActivatedRoute, Router } from '@angular/router';
import { ThrowStmt } from '@angular/compiler';


@Component({
  selector: 'app-driver',
  templateUrl: './driver.component.html',
  styleUrls: ['./driver.component.scss'],
  providers: [EmployeeService]

})
export class DriverComponent implements OnInit {

  public objDriver: DriverInfo = new DriverInfo();
  public lstEmployee: Employee[] = new Array<Employee>();
  selectedTime: any;
  driverID: number = 0;
  constructor(private atp: AmazingTimePickerService, private employeeService: EmployeeService,
    private driverService: DriverService, private notificationService: NotificationService,
    private activatedRoute: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.getAllEmployee();
    this.driverID = this.activatedRoute.snapshot.params["id"];
  }

  getAllEmployee() {
    this.employeeService.getAllEmployee().subscribe((res: any) => {
      this.lstEmployee = res;
     this.objDriver.EmployeeId=this.lstEmployee[0].EmployeeId;
      this.lstEmployee.forEach(employee => {
        employee.EmployeeName = employee.FirstName + ' ' + employee.Lastname;
      })
      this.objDriver.EmployeeId=this.lstEmployee[0].EmployeeId;
      if (this.driverID > 0) {
        this.editDriver();
      }
    });
  }

  editDriver() {
    this.objDriver.DriverInfoId = this.driverID;
    this.driverService.getDriverInfoByID(this.objDriver).subscribe((res: any) => {
      this.objDriver = <DriverInfo>res[0];
    });
  }

  saveDriverInfo() {

    console.log(this.objDriver);
    this.driverService.saveDriver(this.objDriver).subscribe((res: any) => {
      this.notificationService.success(Constants.SuccessMessage.DriverInfoSavedSuccess);
      this.objDriver = new DriverInfo();

      if (this.driverID > 0) {
        this.router.navigate(['/dashboard/tms/driverList']);
        this.driverID = 0;
      }
    });
  }


  openDutyEnd() {
    const amazingTimePicker = this.atp.open({
      time: this.selectedTime,
      theme: 'dark',
      arrowStyle: {
        background: 'red',
        color: 'white'
      }
    });

    amazingTimePicker.afterClose().subscribe(time => {
      this.objDriver.DutyEnd = time;
    });
  }

  openDutyStart() {
    const amazingTimePicker = this.atp.open({
      time: this.selectedTime,


      theme: 'dark',
      arrowStyle: {
        background: 'red',
        color: 'white'
      }
    });

    amazingTimePicker.afterClose().subscribe(time => {
      this.objDriver.DutyStart = time;
    });
  }

}
