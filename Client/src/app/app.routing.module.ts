import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './services/auth-guard.service';
import { TemplateComponent } from './components/template/template.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { ApiConstant } from './common/Constant/APIConstant';


const routes: Routes = [
    {
        path: 'login',
        loadChildren: './modules/Auth/auth.module#AuthModule'
    },
    {
        path: 'dashboard',
        component: TemplateComponent,
        data: {
            breadcrumb: 'Home'
        },
        canActivate: [AuthGuard],
        children: [
            {
                path: '',
               
                loadChildren: './modules/Dashboard/dashboard.module#DashboardModule',
                
            }          
        ],


    },
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    { path: '**', component: NotFoundComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, {useHash: true})],
    exports: [RouterModule]
})
export class AppRoutingModule { }
