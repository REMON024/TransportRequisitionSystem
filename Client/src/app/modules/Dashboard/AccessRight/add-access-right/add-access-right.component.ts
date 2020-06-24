import { Component, OnInit } from '@angular/core';
import { Controller } from 'src/app/Models/VmAccessControl';
import { AccessRightService } from 'src/app/api-services/access-right.service';
import {  NotificationService} from 'src/app/services/notification.service';

import { ChecklistDatabase } from '../access.service';
import { AccessRight } from 'src/app/Models/AccessRight';
import { Constants } from 'src/app/common/Constant/AppConstant';

@Component({
  selector: 'app-add-access-right',
  templateUrl: './add-access-right.component.html',
  styleUrls: ['./add-access-right.component.scss']
})
export class AddAccessRightComponent implements OnInit {

  Isupdate = false;

  constructor(
    private accessRightService: AccessRightService,
    private notificationService:NotificationService,
    private database: ChecklistDatabase) {
    
  }

  ngOnInit() {
    this.accessRightService.GetAllAccessRight().subscribe((res: any) => {
      
      const data = this.database.buildTree(res , 0);
      this.database.dataChange.next(data);
    });
  }

  public AfterSubmitButton(accessRight: AccessRight)
  {
    console.log(accessRight);
    this.accessRightService.SaveOrUpdateAccessRightByRole(accessRight).subscribe((res:any)=>{
      this.notificationService.success(Constants.SuccessMessage.AccessRightCeateSuccessfully);
      console.log(res);
    })
  }

}
