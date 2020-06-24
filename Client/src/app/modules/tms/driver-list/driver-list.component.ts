import { Component, OnInit } from '@angular/core';
import { DriverService } from 'src/app/api-services/driverService';
import { DriverInfo } from 'src/app/Models/DriverInfo';
import { EmployeeService } from 'src/app/api-services/employeeService';
import { Employee } from 'src/app/Models/Employee';
import { Router } from '@angular/router';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-driver-list',
  templateUrl: './driver-list.component.html',
  styleUrls: ['./driver-list.component.scss'],
  providers: [DriverService, EmployeeService]
})
export class DriverListComponent implements OnInit {

  public lstEmployee: Employee[] = new Array<Employee>();
  public lstDriverInfo: DriverInfo[] = new Array<DriverInfo>();
  constructor(private driverService: DriverService, private employeeService: EmployeeService
    , private router: Router
      , private notificationService: NotificationService) { }

  ngOnInit() {
    this.getAllEmployee();
  }

  getAllEmployee() {
    this.employeeService.getAllEmployee().subscribe(res => {
      this.lstEmployee = <Employee[]>res;
      console.log(this.lstEmployee);
      this.getAllDriverInfo();
    });

  }


  getAllDriverInfo() {

    this.driverService.getDriverInfo().subscribe((res: any) => {
      this.lstDriverInfo = res;
      if(this.lstDriverInfo.length==0)
      {
        this.notificationService.info("Driver info Not Found");
      }
      this.lstDriverInfo.forEach(driver => {      
        var emp = this.lstEmployee.filter(e => e.EmployeeId == driver.EmployeeId)[0];
        if (emp.EmployeeId == driver.EmployeeId) {
          driver.EmployeeName = emp.FirstName + " " + emp.Lastname;
        }
      });

    });
  }

  editDriver(driverInfo) {
    this.router.navigate(['/dashboard/tms/driver', driverInfo.DriverInfoId]);
  }



}
