import { InterceptorService } from './interceptor.service';
import { AuthGuard } from './auth.guard';
import { AuthService } from './auth.service';
import { MainService } from './main.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FileUploadModule } from 'primeng/fileupload';
import { CalendarModule } from 'primeng/calendar';
import { ChipsModule } from 'primeng/chips';
import { ProgressSpinnerModule } from 'primeng/progressspinner';


import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { APP_ROUTING } from './routing';
import { LoginComponent } from './login/login.component';
import { NavbarComponent } from './navbar/navbar.component';
import { NotfoundComponent } from './notfound/notfound.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RegisterComponent } from './register/register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { ProductformComponent } from './productform/productform.component';
import { ComputervisionService } from './computervision.service';
import { ProductlistComponent } from './productlist/productlist.component';


@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    LoginComponent,
    NavbarComponent,
    NotfoundComponent,
    RegisterComponent,
    FrontpageComponent,
    ProductformComponent,
    ProductlistComponent
  ],
  imports: [
    BrowserModule,
    NgbModule.forRoot(),
    APP_ROUTING,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    CalendarModule,
    BrowserAnimationsModule,
    FileUploadModule,
    ChipsModule,
    ProgressSpinnerModule
  ],
  providers: [MainService, AuthService, AuthGuard, ComputervisionService, {
    provide: HTTP_INTERCEPTORS,
    useClass: InterceptorService,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
