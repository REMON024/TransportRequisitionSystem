import { NotFoundComponent } from './components/not-found/not-found.component';
import { HTTPInterceptorProviders } from './services/Interceptors';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { ToastrModule } from 'ngx-toastr';
import { AppRoutingModule } from './app.routing.module';
import { HttpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';
import { TopBarComponent } from './components/top-bar/top-bar.component';
import { TemplateComponent } from './components/template/template.component';
import { SideBarComponent } from './components/side-bar/side-bar.component';
import { Ng2AutoBreadCrumb } from 'ng2-auto-breadcrumb';
import { LoaderModule } from './modules/loader/loader.module';
import { UserIdleModule } from 'angular-user-idle';
import { BreadcrumbsModule } from 'ng6-breadcrumbs';
import { OwlDateTimeModule, OwlNativeDateTimeModule } from 'ng-pick-datetime';
import { AmazingTimePickerModule } from 'amazing-time-picker';
import {ExcelService} from './services/excel.service';

@NgModule({
  declarations: [
    AppComponent,
    TopBarComponent,
    TemplateComponent,
    SideBarComponent,
    NotFoundComponent,
    // AutoFocusDirective
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
    }),
    OwlDateTimeModule,
    OwlNativeDateTimeModule,
    AppRoutingModule,
    HttpModule,
    HttpClientModule,
    Ng2AutoBreadCrumb,
    BreadcrumbsModule,
    LoaderModule,
    AmazingTimePickerModule,
    UserIdleModule.forRoot({ idle: 600, timeout: 10, ping: 500 })
  ],
  providers: [
    HTTPInterceptorProviders,
    ExcelService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
