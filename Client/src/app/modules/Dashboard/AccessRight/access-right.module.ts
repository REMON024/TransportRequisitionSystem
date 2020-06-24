import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTreeModule, MatIconModule, MatButtonModule, MatCheckboxModule, MatFormFieldModule, MatCardModule } from '@angular/material';
import { AccessRightListComponent } from './access-right-list/access-right-list.component';
import { Routes, RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AccessRightComponent } from './access-right/access-right.component';
import { AddAccessRightComponent } from './add-access-right/add-access-right.component';
import { UpdateAccessRightComponent } from './update-access-right/update-access-right.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { ShareModule } from '../../Share/share.module';


const routes: Routes = [
  {
    path: 'access-right-list',
    component: AccessRightListComponent,
    data: {
      title2: 'Access Right'
    }
  },
  {
    path: 'add',
    component: AddAccessRightComponent,
    data: {
      title2: 'Add Access Right'
    }
  },
  {
    path: 'update',
    component: UpdateAccessRightComponent,
    data: {
      title2: 'Update Access Right'
    }
  },
  {
    path: '',
    redirectTo: 'access-right-list',
    pathMatch: 'full'
  }
];

@NgModule({
  declarations: [
    AccessRightListComponent,
    AccessRightComponent,
    AddAccessRightComponent,
    UpdateAccessRightComponent
  ],
  imports: [
    CommonModule,
    MatTreeModule,
    MatIconModule,
    MatButtonModule,
    MatCheckboxModule,
    MatCardModule,
    FormsModule, 
    ReactiveFormsModule, 
    FlexLayoutModule,
    RouterModule.forChild(routes),
    ShareModule
  ]
})
export class AccessRightModule { }
