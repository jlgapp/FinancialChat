import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { FinancialChatComponent } from './Chat/financial-chat/financial-chat.component';
import { LoginComponent } from './Seguridad/login/login.component';
import { RegisterComponent } from './Seguridad/register/register.component';
import { SecurityRouter } from './services/Security/security.router';

const routes: Routes = [
  { path: '', component: AppComponent, canActivate:[SecurityRouter] },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'financialChat', component: FinancialChatComponent, canActivate:[SecurityRouter] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers:[SecurityRouter]
})
export class AppRoutingModule { }
