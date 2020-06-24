import { AutoFocusDirective } from './../../Directives/auto-focus.directive';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ShareModule } from '../Share/share.module';

const routes: Routes = [
  { path: '', component: LoginComponent }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormsModule,
    ReactiveFormsModule,
    ShareModule
  ],
  declarations: [
    LoginComponent
  ],
  providers: [

  ]
})
export class AuthModule { }
