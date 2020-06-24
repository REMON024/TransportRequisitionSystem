import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-top-bar',
  templateUrl: './top-bar.component.html',
  styleUrls: ['./top-bar.component.scss']
})
export class TopBarComponent implements OnInit {

  public toggleBarIcon : boolean = true;

  constructor(public authService: AuthService) { }

  ngOnInit() {
  }

  public toggle(): void{
    let self=this;
    setTimeout(()=>{
      self.toggleBarIcon=!self.toggleBarIcon;
    },500)
  }

}
