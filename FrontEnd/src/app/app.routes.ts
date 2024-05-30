import { Routes } from '@angular/router';
import {HomeComponent} from "./components/home/home.component";
import {LoginComponent} from "./components/login/login.component";
import {RegisterComponent} from "./components/register/register.component";
import {AuthGuard} from "./guards/auth-guard";
import {MarketsComponent} from "./components/markets/markets.component";
import {PortfolioComponent} from "./components/portfolio/portfolio.component";

export const routes: Routes = [
  {path: 'home', component: HomeComponent},
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent, canActivate: [AuthGuard] },
  { path: 'markets', component: MarketsComponent },
  { path: 'portfolio', component: PortfolioComponent },
];
