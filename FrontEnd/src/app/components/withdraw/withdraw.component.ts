import {Component, OnInit} from '@angular/core';
import {WalletService} from "../../services/wallet.service";
import {Router} from "@angular/router";
import {Wallet} from "../../models/wallet";
import {ButtonModule} from "primeng/button";
import {CardModule} from "primeng/card";
import {InputTextModule} from "primeng/inputtext";
import {MessageModule} from "primeng/message";
import {NgIf} from "@angular/common";
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {DropdownModule} from "primeng/dropdown";
import {CustomCurrency} from "../../interfaces/CustomCurrency";

@Component({
  selector: 'app-withdraw',
  standalone: true,
  imports: [
    ButtonModule,
    CardModule,
    InputTextModule,
    MessageModule,
    NgIf,
    ReactiveFormsModule,
    DropdownModule
  ],
  templateUrl: './withdraw.component.html',
  styleUrl: './withdraw.component.css'
})
export class WithdrawComponent implements OnInit{
  constructor(public walletService: WalletService,
              private router: Router,) { }
  private wallet: Wallet | undefined;
  currencies: CustomCurrency[] | undefined;
  withdrawForm: FormGroup =  new FormGroup({
    // currency: new FormControl('', [Validators.required]),
    quantity: new FormControl('', [Validators.required]),
    userId: new FormControl(localStorage.getItem('userId')),
  });

  ngOnInit(): void {
    this.currencies = [
      { name: 'Euro'},
      { name: 'US Dollar'},
    ]

    const userId = localStorage.getItem('userId');
    this.walletService.getWalletByUserId(userId).subscribe(
      res => {
        console.log(res);
        this.wallet = res;
        this.withdrawForm.get('quantity')?.setValidators(Validators.max(this.wallet.balance));
      }
    )
  }

  onSubmit(): void{
    if(this.withdrawForm.valid) {
      this.walletService.withdraw(this.withdrawForm.value.userId, this.withdrawForm.value.quantity).subscribe(
        res => {
          console.log(res);
          window.alert("Withdrawal done successfully");
          this.router.navigate(['home']);
        }
      )
    }
  }
  getErrorMessage(controlName: string): string | null {
    const control = this.withdrawForm.get(controlName);

    if(control && control.touched){
      if(control.errors?.['required'])
        return 'This field is required';
      if (control.errors?.['max']) {
        if(this.wallet)
        {
          return `Maximum amount available is ${this.wallet.balance}`;
        }
      }
    }
    return null;
  }

}
