import { AutoFocusDirective } from './../../Directives/auto-focus.directive';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxJsonViewerModule } from 'ngx-json-viewer';
import { JsonViewerComponent } from 'src/app/components/json-viewer/json-viewer.component';
import { MatDialogModule, MatButtonModule, MatIconModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';

@NgModule({
  imports: [
    CommonModule,
    NgxJsonViewerModule,
    MatDialogModule,
    MatButtonModule,
    MatIconModule,
    FlexLayoutModule
  ],
  declarations: [
    AutoFocusDirective,
    JsonViewerComponent
  ],
  exports: [
    AutoFocusDirective,
    JsonViewerComponent
  ],
  entryComponents: [
    JsonViewerComponent
  ]
})
export class ShareModule { }
