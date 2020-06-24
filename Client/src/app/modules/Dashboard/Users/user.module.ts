import { ShareModule } from '../../Share/share.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { UserListComponent } from './user-list/user-list.component';

import {
  MatCardModule,
  MatButtonModule,
  MatDividerModule,
  MatButtonToggleModule,
  MatMenuModule,
  MatDialogModule
} from '@angular/material';
import { PaginationModule } from '../../pagination/pagination.module';
import { AddUserComponent } from './add-user/add-user.component';
import { UserComponent } from './user/user.component';
import { UpdateUserComponent } from './update-user/update-user.component';
import { ResetPasswordComponent } from '../../Dashboard/Users/reset-password/reset-password.component';
import { UserDashBoardComponent } from '../user-dash-board/user-dash-board.component';

const routes: Routes = [
  {
    path: 'user-list',
    component: UserListComponent,
    data: {
      titile2: 'Users'
    }
  },
  {
    path: 'add-user',
    component: AddUserComponent,
    data: {
      titile2: 'Users'
    }
  },
  {
    path: 'update-user/:username',
    component: UpdateUserComponent,
    data: {
      titile2: 'Users'
    }
  },
  
  {
    path: '',
    redirectTo: 'user-list',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormsModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatDividerModule,
    MatButtonToggleModule,
    FlexLayoutModule,
    PaginationModule,
    MatMenuModule,
    MatDialogModule,
    ShareModule
  ],
  declarations: [
    UserListComponent,
    AddUserComponent,
    UserComponent,
    UpdateUserComponent,
    ResetPasswordComponent
  ],
  exports: [
    UserComponent
  ],
  providers: [

  ],
  entryComponents: [
    ResetPasswordComponent
  ]
})
export class UserModule { }
