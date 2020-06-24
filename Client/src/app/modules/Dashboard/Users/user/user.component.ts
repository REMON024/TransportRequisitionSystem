import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Users } from 'src/app/Models/User';
import { AccessRight } from 'src/app/Models/AccessRight';

import { FormGroup, FormControl } from '@angular/forms';
import { UserStatus, AccessRights } from 'src/app/common/Enums/Enums';
import { EnumEx } from 'src/app/common/Utility/Enums';
import { NotificationService } from 'src/app/services/notification.service';
import {AccessRightService} from 'src/app/api-services/access-right.service'
import { VMAccessRightForDropDwon } from 'src/app/Models/VMAccessRightForDropDwon';

@Component({
  selector: 'app-user-add-update',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  @Input() public User: Users = null;
  @Input() public ButtonText : string = 'Create';
  @Input() public IsUpdate: string;
  @Input() public isDisableButton : boolean = false;
  @Input() public Title: string;
  @Input() public CancelButtonUrl: string = '/dashboard/user/user-list';
  @Output() AfterSubmit: EventEmitter<Users> = new EventEmitter();

  public lstAccessRight:VMAccessRightForDropDwon[]=[];
  

  public UserForm : FormGroup;
  public AccessRightList = EnumEx.getNamesAndValues(AccessRights);
  public StatusList = EnumEx.getNamesAndValues(UserStatus);

  constructor(
    private notificationService: NotificationService,
    private accessRightService:AccessRightService
  ) { }

  ngOnInit() {
    this. getAccessRightName();
    this.InitialForm();
  }

  private InitialForm() {
    this.UserForm = new FormGroup({
      Username : new FormControl( { value: this.User ? this.User.Username : '', disabled: this.IsUpdate}),
      Name : new FormControl(this.User ? this.User.Name : ''),
      Surname : new FormControl(this.User ? this.User.Surname : ''),
      Email : new FormControl(this.User ? this.User.Email : ''),
      Password : new FormControl( { value : this.User ? this.User.Password : '', disabled: this.IsUpdate}),
      ConfirmPassword : new FormControl({value: '', disabled: this.IsUpdate}),
      AccessRightId : new FormControl(this.User ? this.User.AccessRightId : AccessRights.User),
      PhoneNumber : new FormControl(this.User ? this.User.PhoneNumber : ''),
      Status : new FormControl(this.User ? this.User.Status : UserStatus.Active),
    });
  }

  public ButtonSubmit(user : Users) {
    if(this.ValidateUser(user)) {
      this.AfterSubmit.next(user);
    }
  }

  private ValidateUser(user: Users): boolean {
    
    if(user.Username === '')
    {
      this.notificationService.warn('Username is required', 'Validation Failed');
      return false;
    }

    if(user.Password === '' && (!this.IsUpdate))
    {
      this.notificationService.warn('Password is required', 'Validation Failed');
      return false;
    }

    if(user.Password !== user.ConfirmPassword && (!this.IsUpdate))
    {
      this.notificationService.warn('Password and Confirm Password not match', 'Validation Failed');
      return false;
    }

    return true;
  }

  getAccessRightName(){
    this.accessRightService.GetAllRoleName().subscribe((res:any) => {
      this.lstAccessRight=res;
      console.log(this.lstAccessRight);
    })
  }

}
