import { ProductformComponent } from './productform/productform.component';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { AuthGuard } from './auth.guard';
import { RegisterComponent } from './register/register.component';
import { NotfoundComponent } from './notfound/notfound.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';
import { LoginComponent } from './login/login.component';
import { ProductlistComponent } from './productlist/productlist.component';


export const APP_ROUTING: ModuleWithProviders = RouterModule.forRoot([
    {
        path: 'dashboard',
        component: DashboardComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'newproduct',
        component: ProductformComponent,
        canActivate: [AuthGuard]

    },
    {
        path: '',
        component: FrontpageComponent,
    },
    {
        path: 'register',
        component: RegisterComponent
    },
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'products',
        component: ProductlistComponent
    },
    {
        path: '**',
        component: NotfoundComponent
    },
   

]);