import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoaderComponent } from './component/loader.component';
import { MatProgressBarModule } from '@angular/material';

@NgModule({
  imports: [
    CommonModule,
    MatProgressBarModule
  ],
  declarations: [
    LoaderComponent
  ],
  exports: [
    LoaderComponent
  ]
})
export class LoaderModule { }
