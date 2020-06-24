import { Component, OnInit } from '@angular/core';
import { Requisition } from 'src/app/Models/Requisition';
import { RequisitionService } from 'src/app/services/requisitionService';
import { NotificationService } from 'src/app/services/notification.service';
import { Constants } from 'src/app/common/Constant/AppConstant';
import { VehicleTypeService } from '../../../api-services/VehicleType.service';
import { VehicleType } from '../../../Models/VehicleType';
import { ActivatedRoute, Router } from '@angular/router';
// import { LocalStorage } from '@ngx-pwa/local-storage';
import { VMQueryObject } from '../../../Models/VMQueryObject';
import { Vehicle } from '../../../Models/Vehicle';
import { empty } from 'rxjs';
@Component({
  selector: 'app-requisition',
  templateUrl: './requisition.component.html',
  styleUrls: ['./requisition.component.scss'],
  providers: [RequisitionService, VehicleTypeService]
})
export class RequisitionComponent implements OnInit {
  IsSelfDrive: boolean;
  Vehicles: string[];
  selected = null;
  public objRequisition: Requisition = new Requisition();
  public vehicleType: VehicleType[];
  public lstVehicle: Vehicle[];
  public requisitionID: number;
  public QueryObject: VMQueryObject;

  constructor(private requisiitonService: RequisitionService, private activatedRoute: ActivatedRoute,
              private notificationService: NotificationService, private vehicleservice: VehicleTypeService,
              private router: Router) {


      // if (localStorage.getItem('queryObj')) {
      //     this.QueryObject = JSON.parse(localStorage.getItem('queryObj'));
      //     localStorage.removeItem('queryObj');
      // }
  }

  ngOnInit() {

this.objRequisition.ProjectName = 'TMS System';

this.getVehicleType();
    // this.getRequisition()
this.requisitionID = this.activatedRoute.snapshot.params['id'];
console.log(this.requisitionID);
if (this.requisitionID > 0) {
        this.editRequisition(this.requisitionID);
      }
  }


  Check(DateTime){
    console.log(DateTime, Date.now);
    if (DateTime < Date.now()){
  this.notificationService.error('From Date Time Never Less than current Date time');

  }
}


CheckToDateWithFromDate(ToDateTime, FromDateTime){

  console.log(FromDateTime, ToDateTime)

  if (FromDateTime > ToDateTime){
    this.notificationService.error('To Time Must be greter than From Time')
  }
}


  saveRequisition() {
    if (this.IsSelfDrive) {
      this.objRequisition.IsSelfDrive = 1;
    }
    else if (!this.IsSelfDrive) {
      this.objRequisition.IsSelfDrive = 0;
         }
    this.requisiitonService.SaveRequisition(this.objRequisition)
      .subscribe(res => {
        this.notificationService.success(Constants.SuccessMessage.RequisitionSavedSuccess);
        this.objRequisition = new Requisition();
        this.router.navigate(['/dashboard/Userdashboard']);

        if (this.requisitionID > 0) {
          localStorage.setItem('queryObj', JSON.stringify(this.QueryObject));

          this.requisitionID = 0;



        }


      });

  }

  getVehicleType() {
    this.vehicleservice.getVehicleType().subscribe((res: any) => {
      this.vehicleType = res;
    });
  }

  editRequisition(requisitionID) {

    this.objRequisition.RequisitionId = requisitionID;
    this.requisiitonService.getRequisitionByID(this.objRequisition)
      .subscribe((res: any) => {
        this.objRequisition = <Requisition> res[0];
        this.getVehicleByType();

        console.log(res);
        if (this.objRequisition.IsSelfDrive == 1)
          this.IsSelfDrive = true;
        else if (this.objRequisition.IsSelfDrive == 0)
          this.IsSelfDrive = false;
      });
  }

  getRequisition() {
            this.requisiitonService.getRequisitionByID(this.objRequisition)
            .subscribe(res => {

      });
  }

  getVehicleByType(){
            this.vehicleservice.getVehicleByType(this.objRequisition.VehicleTypeID).subscribe((res: any) => {
            this.lstVehicle = <Vehicle[]> res;

            console.log(this.lstVehicle);
    })
  }



}
