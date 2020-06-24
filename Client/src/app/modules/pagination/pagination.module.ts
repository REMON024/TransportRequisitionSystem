import { NgModule } from '@angular/core';
import { NgxPaginationModule } from 'ngx-pagination';
import { CommonModule } from '@angular/common';
import { CustomPaginationComponent } from './custom-template/custom-template.component';
import { MatButtonModule, MatIconModule } from '@angular/material';


@NgModule({
    declarations: [
        CustomPaginationComponent
    ],
    imports: [
        NgxPaginationModule,
        CommonModule,
        MatButtonModule
    ],
    exports: [
        CustomPaginationComponent,
        NgxPaginationModule,
        MatIconModule
    ]
})

export class PaginationModule {

}
