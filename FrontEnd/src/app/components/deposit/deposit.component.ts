import {Component, OnInit} from '@angular/core';
import {ButtonModule} from "primeng/button";
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {CardModule} from "primeng/card";
import {DropdownModule} from "primeng/dropdown";
import {CustomCurrency} from "../../interfaces/CustomCurrency";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {CommonModule} from "@angular/common";
import {InputTextModule} from "primeng/inputtext";
import {MessageModule} from "primeng/message";
import {HttpClient} from "@angular/common/http";
import {WalletService} from "../../services/wallet.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-deposit',
  standalone: true,
  imports: [
    ButtonModule,
    ReactiveFormsModule,
    CardModule,
    DropdownModule,
    CommonModule,
    InputTextModule,
    MessageModule,
  ],
  templateUrl: './deposit.component.html',
  styleUrl: './deposit.component.css'
})
export class DepositComponent implements OnInit{

  constructor(private http: HttpClient,
              private walletService: WalletService,
              private router: Router) {}

  depositForm: FormGroup =  new FormGroup({
    // currency: new FormControl('', [Validators.required]),
    quantity: new FormControl('', [Validators.required, Validators.min(200)]),
    userId: new FormControl(localStorage.getItem('userId')),
  });
  currencies: CustomCurrency[] | undefined;
  deposit() : void {

  }
  getErrorMessage(controlName: string): string | null {
    const control = this.depositForm.get(controlName);

    if(control && control.touched){
      if(control.errors?.['required'])
        return 'This field is required';
      if (control.errors?.['min']) {
        return 'The minimum deposit is 200 Euros.';
      }
    }
    return null;
  }

  ngOnInit(): void {
    this.currencies = [
      { name: 'Euro'},
      { name: 'US Dollar'},
    ]
  }
  onSubmit(): void {
    if(this.depositForm.valid){
      this.walletService.deposit(this.depositForm.value).subscribe({
        next: (user) => {
          console.log(this.depositForm.value);
          console.log(user.balance);
          window.alert("Deposit done successfully");
          this.router.navigate(['home']);
        }
      })
    }
    else{
      this.depositForm.markAsTouched();
    }
  }
}
