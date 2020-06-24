import { NgModule, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RequisitionComponent } from './requisition/requisition.component';
import { TravelDetailComponent } from './travel-detail/travel-detail.component';
import { VehicleComponent } from './vehicle/vehicle.component';
import { DriverComponent } from './driver/driver.component';
import { Routes, RouterModule } from '@angular/router';
import { RequisitionListComponent } from './requisition-list/requisition-list.component';
import { FormsModule } from '@angular/forms';
import { DriverListComponent } from './driver-list/driver-list.component';
import { VehicleListComponent } from './vehicle-list/vehicle-list.component';
import { AdvancedReportComponent } from './advanced-report/advanced-report.component';
import { RequisitionDetailsComponent } from './requisition-details/requisition-details.component';
import { RequisitionApprovalComponent } from './requisition-approval/requisition-approval.component';
import { TraveldetailsComponent } from './traveldetails/traveldetails.component';
import { PaginationModule } from '../pagination/pagination.module';



const routes: Routes = [
  {
    path: 'requisition',
    component: RequisitionComponent,
    data: {
      titile2: 'Requisition'
    }
  },
  {
    path: 'requisition/:id',
    component: RequisitionComponent,
    data: {
      titile2: 'Requisition'
    }
  },
  {
    path: 'requisition-list',
    component: RequisitionListComponent,
    data: {
      titile2: 'Requisition List'
    }
  },
  {
    path: 'driver',
    component: DriverComponent,
    data: {
      titile2: 'Drivers'
    }
  },
  {
    path: 'driver/:id',
    component: DriverComponent,
    data: {
      titile2: 'Drivers'
    }
  },
  {
    path: 'vehicle',
    component: VehicleComponent,
    data: {
      titile2: 'Vehicles'
    }
  },
  {
    path: 'vehicle/:id',
    component: VehicleComponent,
    data: {
      titile2: 'Vehicles'
    }
  },
  {
    path: 'vehicleList',
    component: VehicleListComponent,
    data: {
      titile2: 'Vehicles'
    }
  },

  {
    path: 'AdvancedReport',
    component: AdvancedReportComponent,
    data: {
      titile2: 'Advanced Report'
    }
  },
  {
    path: 'travelDetail/:id',
    component: TravelDetailComponent,
    data: {
      titile2: 'Travel Detail'
    }

  },
  {
    path: 'travelDetail',
    component: TravelDetailComponent,
    data: {
      titile2: 'Travel Detail'
    }


  },
  {
    path: 'driverList',
    component: DriverListComponent,
    data: {
      titile2: 'Driver List'
    }


  },

  {
    path: 'requisitiondetails/:id',
    component: RequisitionDetailsComponent,
    data: {
      titile2: 'Requisition Details'
    }


  },

  {
    path: 'requisitionapproval/:id',
    component: RequisitionApprovalComponent,
    data: {
      titile2: 'Requisition Approval'
    }


  },

  {
    path:'traveldetails/:id',
    component: TraveldetailsComponent,
    data: {
      titile2: 'Travel Details'
    }

  },

  {
    path: '',
    redirectTo: 'requisition-list',
    pathMatch: 'full'
  }
];



@NgModule({
  declarations: [RequisitionComponent, TravelDetailComponent, VehicleComponent, DriverComponent,
    RequisitionListComponent,
    DriverListComponent,
    VehicleListComponent,
    AdvancedReportComponent,
    RequisitionDetailsComponent,
    RequisitionApprovalComponent,
    TraveldetailsComponent],
  imports: [
    CommonModule,
    FormsModule,
    PaginationModule,
    RouterModule.forChild(routes)
  ]
})


export class TMSModule { }
