import {Component, OnInit, ViewChild} from '@angular/core';
import {TabMenuModule} from "primeng/tabmenu";
import {MenuItem} from "primeng/api";
import {MenubarModule} from "primeng/menubar";
import {NgClass, NgForOf, NgIf, NgOptimizedImage} from "@angular/common";
import {CustomMenuItem} from "../../interfaces/CustomMenuItem";
import {ButtonModule} from "primeng/button";
import {UserServiceService} from "../../services/user-service.service";
import {Router} from "@angular/router";
import {OverlayPanel, OverlayPanelModule} from "primeng/overlaypanel";
import {MenuModule} from "primeng/menu";

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    TabMenuModule,
    MenubarModule,
    NgOptimizedImage,
    NgClass,
    NgForOf,
    ButtonModule,
    OverlayPanelModule,
    MenuModule,
    NgIf
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
  isLoggedInVariable: boolean = false;
  constructor(private router: Router) {}
  items: CustomMenuItem[] | undefined;
  activeItem: CustomMenuItem | undefined;
  itemsWhenLoggedIn: CustomMenuItem[] | undefined;
  activeItemWhenLoggedIn: CustomMenuItem | undefined;

  logOut(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
    this.router.navigate(['/']);
  }
  isLoggedIn(): boolean{
    return localStorage.getItem('token') != null && localStorage.getItem('userId') != null;
  }

  ngOnInit() {
    this.isLoggedInVariable = this.isLoggedIn();
    this.itemsWhenLoggedIn = [
      { label: 'Home', routerLink: 'home'},
      { label: 'Markets', routerLink: 'markets'},
      { label: 'Portfolio', routerLink: 'portfolio'},
      { label: 'Deposit', routerLink: 'deposit'},
      { label: 'Withdraw', routerLink: 'withdraw'},
      { label: 'Trade', routerLink: 'trade'},
      { label: 'Transactions', routerLink: 'transactions'}
    ];
    this.activeItemWhenLoggedIn = this.itemsWhenLoggedIn[0];
    this.items = [
      { label: 'Home', routerLink: 'home'},
      { label: 'Markets', routerLink: 'markets'},
      { label: 'Edit', },
      { label: 'Login', routerLink: 'login' },
      { label: 'Register', routerLink: 'register', rightAligned: true },
      { label: 'Portfolio', routerLink: 'portfolio'},
      { label: 'Deposit', routerLink: 'deposit'},
      { label: 'Withdraw', routerLink: 'withdraw'},
      { label: 'Trade', routerLink: 'trade'},
    ];
    this.activeItem = this.items[0];
  }
}
