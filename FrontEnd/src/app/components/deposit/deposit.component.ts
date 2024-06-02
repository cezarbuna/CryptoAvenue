import { Component } from '@angular/core';
import {Router} from "@angular/router";
import {ButtonModule} from "primeng/button";

@Component({
  selector: 'app-deposit',
  standalone: true,
  imports: [
    ButtonModule
  ],
  templateUrl: './deposit.component.html',
  styleUrl: './deposit.component.css'
})
export class DepositComponent {
  constructor(private router: Router) {}

  deposit(): void {
    this.router.navigate(['payment']);
  }
}
