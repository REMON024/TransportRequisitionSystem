import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ErrorCode } from '../common/Enums/Enums';


@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  private timeOut = 3000;
  constructor(private toastService: ToastrService) {
  }

  public success(text: string, title: string = 'Success', timeout?: number) {
    this.toastService.success(text, title, {
      timeOut: this.timeOut
    });
  }

  public warn(text: string, title: string = 'Warning', timeout?: number) {
    this.toastService.warning(text, title, {
      timeOut: this.timeOut
    });
  }

  public error(text: string, title: string = 'Error', timeout?: number) {
    this.toastService.error(text, title, {
      timeOut: this.timeOut
    });
  }

  public info(text: string, title: string = 'Info', timeout?: number) {
    this.toastService.info(text, title, {
      timeOut: this.timeOut
    });
  }

  public infoWithoutTimeLimit(text: string, title: string = 'Info') {
    this.toastService.info(text, title, {
      disableTimeOut: true,
      closeButton: true
    });
  }

  public dynamic(error: any) {
    if (error.status === ErrorCode.BAD_REQUEST) {
      this.warn(error.error, 'Bad Request');
    }

    if (error.status === ErrorCode.NOT_FOUND) {
      this.info(error.error, 'Not Found');
    }

    if (error.status === ErrorCode.INTERNAL_SERVER_ERROR) {
      this.error(error.error, 'Failed');
    }

    if (error.status === ErrorCode.UNAUTHENTICATE) {
      this.error(error.error, 'Unauthorize');
    }

    if (error.status === ErrorCode.FORBIDDEN) {
      this.error(error.error, 'Forbidden');
    }
  }
}
