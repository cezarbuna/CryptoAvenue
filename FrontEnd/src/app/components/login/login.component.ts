import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {CardModule} from "primeng/card";
import {ButtonModule} from "primeng/button";
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import { InputTextModule } from 'primeng/inputtext';
import {UserServiceService} from "../../services/user-service.service";
import {Router} from "@angular/router";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {NgClass, NgIf} from "@angular/common";
import {MessageModule} from "primeng/message";
import {AuthService} from "../../services/auth.service";

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
    ReactiveFormsModule,
    NgClass
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  constructor(
    private userService: UserServiceService,
    private authService: AuthService,
    protected router: Router,
    private cdr: ChangeDetectorRef,) {
  }

  invalidCredentials: boolean = false;
  isEmailCorrect: boolean = false;
  invalidEmail: boolean = false;

  loginForm: FormGroup =  new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required])
  });

  ngOnInit(): void {
  }

  validateEmail(): void {
    this.userService.validateEmail(this.loginForm?.value.email).subscribe(
      res => {
        if (res === false) {
          this.invalidEmail = true;
        } else {
          this.invalidEmail = false;
          this.isEmailCorrect = true;
          this.cdr.detectChanges();
        }
      },
      error => {
        console.log(error);
      }
    );
  }

  onEmailInput(): void {
    if (this.invalidEmail) {
      this.invalidEmail = false;
    }
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      this.userService.loginUser(this.loginForm.value).subscribe({
        next: (loginResponse) => {
          window.alert("User logged in successfully");
          localStorage.setItem('token', loginResponse.token);
          localStorage.setItem('userId', loginResponse.userId);
          this.authService.login(loginResponse.userId, loginResponse.token);
          this.router.navigate(['/']);
        },
        error: error => {
          this.invalidCredentials = true;
          console.log(error);
        }
      });
    }
  }
}
