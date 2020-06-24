import { Component, OnInit, ResolvedReflectiveFactory, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { NotificationService } from 'src/app/services/notification.service';
import { UserService } from 'src/app/api-services/user.service';
import { FormGroup, FormControl } from '@angular/forms';
import { VmResetPassword } from 'src/app/Models/VmResetPassword';
import { CryptoService } from 'src/app/services/crypto.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {

  public ResetForm : FormGroup;

  constructor(
    public dialogRef: MatDialogRef<ResetPasswordComponent>,
    @Inject(MAT_DIALOG_DATA) public userName: string,
    private notification: NotificationService,
    private userService: UserService,
    private cryptoService: CryptoService
  ) { }

  ngOnInit() {
    this.InitialForm();
  }

  private InitialForm() {
    this.ResetForm = new FormGroup({
      Password: new FormControl(''),
      ConfirmPassword: new FormControl('')
    });
  }


  public CloseDialog() {
    this.dialogRef.close();
  }

  public SubmitDialog(resetPassword: VmResetPassword) {
    if(this.IsValidResetForm(resetPassword)) {
      resetPassword.Username = this.userName;
      this.cryptoService.encrypt(resetPassword.Password).then((cipherText : string) => {
        resetPassword.Password = cipherText;
        resetPassword.ConfirmPassword = cipherText;
        this.userService.ResetPassword(resetPassword).subscribe((res : any) => {
          this.notification.success(res);
          this.dialogRef.close();
        }, () => {
          });
      });
    }
  }

  private IsValidResetForm(resetPassword: VmResetPassword) : boolean {
    if(resetPassword.Password === '') {
      this.notification.warn('Password is required');
      return false;
    }

    if(resetPassword.Password !== resetPassword.ConfirmPassword) {
      this.notification.warn('Password and Confirm Password is not match');
      return false;
    }

    return true;
  }

}
