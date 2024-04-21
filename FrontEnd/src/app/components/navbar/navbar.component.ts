import {Component, OnInit} from '@angular/core';
import {TabMenuModule} from "primeng/tabmenu";
import {MenuItem} from "primeng/api";
import {MenubarModule} from "primeng/menubar";
import {NgClass, NgForOf, NgOptimizedImage} from "@angular/common";
import {CustomMenuItem} from "../../interfaces/CustomMenuItem";

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    TabMenuModule,
    MenubarModule,
    NgOptimizedImage,
    NgClass,
    NgForOf
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
  items: CustomMenuItem[] | undefined;

  activeItem: CustomMenuItem | undefined;

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
      { label: 'Register', rightAligned: true }
    ];

    this.activeItem = this.items[0];
  }
}
