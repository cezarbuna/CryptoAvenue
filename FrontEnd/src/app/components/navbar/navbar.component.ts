import {Component, OnInit} from '@angular/core';
import {TabMenuModule} from "primeng/tabmenu";
import {MenuItem} from "primeng/api";
import {MenubarModule} from "primeng/menubar";
import {NgClass, NgForOf, NgOptimizedImage} from "@angular/common";
import {CustomMenuItem} from "../../interfaces/CustomMenuItem";
import {ButtonModule} from "primeng/button";
import {UserServiceService} from "../../services/user-service.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    TabMenuModule,
    MenubarModule,
    NgOptimizedImage,
    NgClass,
    NgForOf,
    ButtonModule
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {

  constructor(private router: Router) {}
  items: CustomMenuItem[] | undefined;

  activeItem: CustomMenuItem | undefined;

  logOut(): void {
    localStorage.removeItem('token');
    this.router.navigate(['/']);
  }

  ngOnInit() {
    // this.items = [
    //   { label: 'Home', icon: 'pi pi-fw pi-home' },
    //   { label: 'Calendar', icon: 'pi pi-fw pi-calendar' },
    //   { label: 'Edit', icon: 'pi pi-fw pi-pencil' },
    //   { label: 'Documentation', icon: 'pi pi-fw pi-file' },
    //   { label: 'Settings', icon: 'pi pi-fw pi-cog' }
    // ];
    this.items = [
      { label: 'Home', routerLink: 'home'},
      { label: 'Markets', },
      { label: 'Edit', },
      { label: 'Login', routerLink: 'login' },
      { label: 'Register', routerLink: 'register', rightAligned: true }
    ];

    this.activeItem = this.items[0];
  }
}
