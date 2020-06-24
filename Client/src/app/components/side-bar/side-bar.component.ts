import { Component, OnInit } from '@angular/core';
import { RoutingService } from 'src/app/services/routing.service';
import { Routing } from 'src/app/Models/Routing';
import { TokenService } from 'src/app/services/token.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.scss']
})
export class SideBarComponent implements OnInit {

  public routeList : Routing[];

  constructor(
    private routeService: RoutingService,
    private authService: AuthService) { }

  ngOnInit() {
    this.routeService.GetRoutingByAccessRight(this.authService.getLoggedAccessRight())
        .then(res => {
          this.routeList = res;
        });
  }

}
