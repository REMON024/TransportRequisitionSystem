import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Page1Component } from './page1/page1.component';

const routes: Routes = [
    { 
      path: "Page1", 
      component: Page1Component,
      data : {
          titile2: 'Others'
      }
    },
    {
        path: '',
        redirectTo: 'Page1',
        pathMatch: 'full'
    }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormsModule,
    ReactiveFormsModule
  ],
  declarations: [
    Page1Component
  ],
  providers: [
    
  ]
})
export class OthersModule { }