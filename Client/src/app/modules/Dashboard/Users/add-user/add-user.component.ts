import { Component, OnInit } from '@angular/core';
import { EnumEx } from 'src/app/common/Utility/Enums';
import { UserStatus } from 'src/app/common/Enums/Enums';
import { LoaderService } from 'src/app/modules/loader/loader.service';
import { Users } from 'src/app/Models/User';
import { UserService } from 'src/app/api-services/user.service';
import { NotificationService } from 'src/app/services/notification.service';
import { Constants } from 'src/app/common/Constant/AppConstant';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.scss']
})
export class AddUserComponent implements OnInit {

  public StatusList = EnumEx.getNamesAndValues(UserStatus);
  public isButtonDisable: boolean = false;

  constructor(
    private userService: UserService,
    private notificationService: NotificationService,
    private router: Router
  ) { }

  ngOnInit() {
  }

  public SubmitUserForm(user: Users)
  {
      this.userService.AddUser(user)
          .subscribe(res => {
            this.notificationService.success(Constants.SuccessMessage.UserAddSuccess);
            this.isButtonDisable = true;
          });
  }

}
