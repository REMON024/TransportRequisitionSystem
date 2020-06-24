import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuditLogComponent } from './audit-log/audit-log.component';
import { SessionLogComponent } from './session-log/session-log.component';
import { PaginationModule } from '../../pagination/pagination.module';
import { MatDatepickerModule, MatNativeDateModule, DateAdapter, MatButtonModule } from '@angular/material';
import { ShareModule } from '../../Share/share.module';
import { FlexLayoutModule } from '@angular/flex-layout';

const routes: Routes = [
    {
        path: "audit-log",
        component: AuditLogComponent,
        data: {
            titile2: 'Report'
        }
    },
    {
        path: "session-log",
        component: SessionLogComponent,
        data: {
            titile2: 'Report'
        }
    },
    {
        path: '',
        redirectTo: 'audit-log',
        pathMatch: 'full'
    }
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        FormsModule,
        ReactiveFormsModule,
        PaginationModule,
        MatDatepickerModule,
        MatNativeDateModule,
        ShareModule,
        MatButtonModule
    ],
    declarations: [
        AuditLogComponent,
        SessionLogComponent
    ],
    providers: []
})
export class ReportModule {
    constructor(private dateAdapter: DateAdapter<Date>)
    {
        this.dateAdapter.setLocale("en-in");
    }
 }