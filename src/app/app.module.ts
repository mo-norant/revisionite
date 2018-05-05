import { AuthGuard } from './auth.guard';
import { AuthService } from './auth.service';
import { MainService } from './main.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';


import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { APP_ROUTING } from './routing';
import { LoginComponent } from './login/login.component';
import { NavbarComponent } from './navbar/navbar.component';
import { NotfoundComponent } from './notfound/notfound.component';
import { HttpClientModule } from '@angular/common/http';
import { RegisterComponent } from './register/register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { ProductformComponent } from './productform/productform.component';
import {CalendarModule} from 'primeng/calendar';


@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    LoginComponent,
    NavbarComponent,
    NotfoundComponent,
    RegisterComponent,
    FrontpageComponent,
    ProductformComponent
  ],
  imports: [
    BrowserModule,
    NgbModule.forRoot(),
    APP_ROUTING,
    HttpClientModule,
    FormsModule,        
    ReactiveFormsModule,
    CalendarModule,
    BrowserAnimationsModule
  ],
  providers: [MainService, AuthService, AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
