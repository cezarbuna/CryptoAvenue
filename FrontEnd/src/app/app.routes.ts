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
import {CoinDetailComponent} from "./components/coin-detail/coin-detail.component";
import {ChatbotComponent} from "./components/chatbot/chatbot.component";
import {PortfolioComponent} from "./components/portfolio/portfolio.component";

export const routes: Routes = [
  {path: 'home', component: HomeComponent},
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent, },
  { path: 'markets', component: MarketsComponent },
  { path: 'deposit', component: DepositComponent , canActivate: [AuthGuard] },
  { path: 'payment', component: PaymentComponent, canActivate: [AuthGuard]  },
  { path: 'withdraw', component: WithdrawComponent, canActivate: [AuthGuard] },
  { path: 'trade', component: TradeComponent, canActivate: [AuthGuard] },
  { path: 'transactions', component: TransactionsComponent, canActivate: [AuthGuard] },
  { path: 'coin-detail/:coinId', component: CoinDetailComponent },
  { path: 'chatbot', component: ChatbotComponent, canActivate: [AuthGuard] },
  { path: 'portfolio', component: PortfolioComponent, canActivate: [AuthGuard] },
];
