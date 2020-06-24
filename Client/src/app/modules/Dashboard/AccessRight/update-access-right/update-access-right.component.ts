import { Component, OnInit } from '@angular/core';
import { AccessRightService } from 'src/app/api-services/access-right.service';
import { ChecklistDatabase } from '../access.service';
import { AccessRight } from 'src/app/Models/AccessRight';

@Component({
  selector: 'app-update-access-right',
  templateUrl: './update-access-right.component.html',
  styleUrls: ['./update-access-right.component.scss']
})
export class UpdateAccessRightComponent implements OnInit {

  constructor(
    private accessRightService: AccessRightService,
    private database: ChecklistDatabase
  ) { }

  ngOnInit() {
    this.accessRightService.GetAllAccessRight().subscribe((res: any) => {
      const data = this.database.buildTree(res , 0);
      this.database.dataChange.next(data);
    });
  }

  setRole(role) {
    console.log("Update",role);

    
    this.LoadExistingAccessRight(role);
  }
  private LoadExistingAccessRight(roleName: string) {
    this.accessRightService.GetAccessRightByRole(roleName).subscribe((res: any) => {
      this.database.selectDataChange.next(res);
    });
  }

  public AfterSubmitButton(accessRight: AccessRight)
  {
    console.log(accessRight);
    this.accessRightService.SaveOrUpdateAccessRightByRole(accessRight).subscribe((res:any)=>{
      console.log(res);
    })
  }

}
