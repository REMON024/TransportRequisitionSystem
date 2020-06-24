import { Component, OnInit, OnDestroy } from '@angular/core';
import { LoaderService } from './modules/loader/loader.service';
import { 
  Router, 
  RouterEvent, 
  NavigationStart, 
  NavigationEnd, 
  NavigationCancel, 
  NavigationError, 
  RouteConfigLoadStart, 
  RouteConfigLoadEnd 
} from '@angular/router';
import { UserIdleService } from 'angular-user-idle';
import { AuthService } from './services/auth.service';
import { NotificationService } from './services/notification.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {
  
  title = 'NybSys-Client';

  private subscription1 =  new Subscription();
  private subscription12 =  new Subscription();


  constructor(
    private loaderService: LoaderService,
    private router : Router,
    private userIdleService: UserIdleService,
    private authService: AuthService,
    private notification: NotificationService
  ) {
    router.events.subscribe((event: RouterEvent) => {
      this.NavigationInterceptor(event);
    })
   }

   ngOnInit() {
    //Start watching for user inactivity.
    this.userIdleService.stopWatching();
    
    // Start watching when user idle is starting.
    this.subscription1 = this.userIdleService.onTimerStart().subscribe(count => {});
    
    // Start watch when time is up.
    this.subscription12 = this.userIdleService.onTimeout().subscribe(() => {
      if(this.authService.checkLogged()) {
        this.authService.logout();
        this.notification.infoWithoutTimeLimit('Your Session is expired for your inactivity');
      }

      this.restart();
    });
  }

  
 
  stop() {
    this.userIdleService.stopTimer();
  }
 
  stopWatching() {
    this.userIdleService.stopWatching();
  }
 
  startWatching() {
    this.userIdleService.startWatching();
  }
 
  restart() {
    this.userIdleService.resetTimer();
  }

  private NavigationInterceptor(event : RouterEvent) {
    switch (true) {
      case event instanceof NavigationStart:
      case event instanceof RouteConfigLoadStart: {
        this.loaderService.show();
        break;
      }


      case event instanceof NavigationEnd:
      case event instanceof NavigationCancel:
      case event instanceof NavigationError:
      case event instanceof RouteConfigLoadEnd: {
        this.loaderService.hide();
        break;
      }
      default: {
        break;
      }
    }
  }

  ngOnDestroy(): void {
    this.subscription1.unsubscribe();
    this.subscription12.unsubscribe();
  }

}
