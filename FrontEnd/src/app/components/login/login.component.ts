import {Component, OnInit} from '@angular/core';
import {CardModule} from "primeng/card";
import {ButtonModule} from "primeng/button";
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import { InputTextModule } from 'primeng/inputtext';
import {UserServiceService} from "../../services/user-service.service";
import {Router} from "@angular/router";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {NgIf} from "@angular/common";
import {MessageModule} from "primeng/message";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CardModule,
    ButtonModule,
    FormsModule,
    InputTextModule,
    HttpClientModule,
    NgIf,
    MessageModule,
    ReactiveFormsModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  constructor(
    private userService: UserServiceService,
    private router: Router,) {
  }
  value: any;
  emailValue: string = '';
  invalidEmail: boolean = false;
  loginForm: FormGroup =  new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required])
  });

  ngOnInit(): void {
  }

  validateEmail(): void {
    this.userService.validateEmail(this.loginForm?.value.email).subscribe(res => {
      console.log(res);
      if(res == false)
        this.invalidEmail = true;
    }, error => {
      console.log(error);
      }
    )
  }

  onSubmit(): void{

  }

}
