import { Routes } from '@angular/router';
import {HomeComponent} from "./components/home/home.component";
import {LoginComponent} from "./components/login/login.component";
import {RegisterComponent} from "./components/register/register.component";
import {AuthGuard} from "./guards/auth-guard";
import {MarketsComponent} from "./components/markets/markets.component";
import {DepositComponent} from "./components/deposit/deposit.component";
import {PaymentComponent} from "./components/payment/payment.component";
import {WithdrawComponent} from "./components/withdraw/withdraw.component";
import {TradeComponent} from "./components/trade/trade.component";
import {TransactionsComponent} from "./components/transactions/transactions.component";

export const routes: Routes = [
  {path: 'home', component: HomeComponent},
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent, },
  { path: 'markets', component: MarketsComponent },
  { path: 'deposit', component: DepositComponent },
  { path: 'payment', component: PaymentComponent },
  { path: 'withdraw', component: WithdrawComponent },
  { path: 'trade', component: TradeComponent },
  { path: 'transactions', component: TransactionsComponent},
  { path: 'coin-detail/:coinId'}
];
