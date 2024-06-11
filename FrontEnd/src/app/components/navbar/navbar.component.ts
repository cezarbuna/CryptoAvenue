import {ChangeDetectorRef, Component, Injectable, OnInit, ViewChild} from '@angular/core';
import {TabMenuModule} from "primeng/tabmenu";
import {MenuItem, MessageService} from "primeng/api";
import {MenubarModule} from "primeng/menubar";
import {NgClass, NgForOf, NgIf, NgOptimizedImage} from "@angular/common";
import {CustomMenuItem} from "../../interfaces/CustomMenuItem";
import {ButtonModule} from "primeng/button";
import {UserServiceService} from "../../services/user-service.service";
import {Router} from "@angular/router";
import {OverlayPanel, OverlayPanelModule} from "primeng/overlaypanel";
import {MenuModule} from "primeng/menu";
import {AuthService} from "../../services/auth.service";
import {JwtHelperService} from "@auth0/angular-jwt";
import {RippleModule} from "primeng/ripple";

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
    NgIf,
    RippleModule
  ],
  providers: [MessageService],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
  isLoggedInVariable: boolean = false;

  constructor(
    private router: Router,
    private authService: AuthService,
    private messageService: MessageService
  ) {}

  ngOnInit() {
    this.authService.loginStatus$.subscribe(isLoggedIn => {
      this.isLoggedInVariable = isLoggedIn;
    });

    this.isLoggedInVariable = this.authService.isLoggedIn();
  }

  logOut(): void {
    this.authService.logout();
    this.router.navigate(['/home']);
  }

  showMessage(): void {
    this.messageService.add({ severity: 'success', summary: 'Logged out successfully!' });
  }
}
