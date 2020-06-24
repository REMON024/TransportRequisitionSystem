import { ForbiddenComponent } from './../../components/forbidden/forbidden.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { UserDashBoardComponent } from './user-dash-board/user-dash-board.component';
import { DriverDashboardComponent } from './driver-dashboard/driver-dashboard.component';


const routes: Routes = [
    {
        path: '',
        component: DashboardComponent,
        data: {
            breadcrumb: 'Dashboard'
        }
    },

    {
        path: 'Driverdashboard',
        component: DriverDashboardComponent,
        data: {
            breadcrumb: 'DriverDashboard'
        }
    },

    {
        path: 'Userdashboard',
        component: UserDashBoardComponent,
        data: {
            breadcrumb: 'UserDashboard'
        }
    },
    {
        path: 'Others',
        data: {
            title: 'Dashboard'
        },
        children: [
            {
                path: '',
                loadChildren: './Others/others.module#OthersModule'
            },
            {
                path: '',
                loadChildren: '../tms/tms.module#TMSModule'
            }
        ]
    },
    {
        path: 'user',
        data: {
            title: 'Dashboard'
        },
        children: [
            {
                path: '',
                loadChildren: './Users/user.module#UserModule'
            }
        ]
    },
    {
        path: 'report',
        data: {
            title: 'Dashboard'
        },
        children: [
            {
                path: '',
                loadChildren: './Report/report.module#ReportModule'
            }
        ]
    },
    {
        path: 'access-right',
        data: {
            title: 'Dashboard'
        },
        children: [
            {
                path: '',
                loadChildren: './AccessRight/access-right.module#AccessRightModule'
            }
        ]
    },
    {
        path: 'tms',
        data: {
            title: 'TMS'
        },
        children: [
            {
                path: '',
                loadChildren: '../tms/tms.module#TMSModule'
            }
        ]
    },
    {
        path: 'forbidden',
        component: ForbiddenComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class DashboardRoutingModule { }
