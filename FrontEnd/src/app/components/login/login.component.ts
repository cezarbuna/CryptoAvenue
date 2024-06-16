import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { UserServiceService } from '../../services/user-service.service';
import { Router } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { NgClass, NgIf } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';

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
    ToastModule,
    ReactiveFormsModule,
    NgClass,
  ],
  providers: [MessageService], // Register the MessageService
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  constructor(
    private userService: UserServiceService,
    private authService: AuthService,
    protected router: Router,
    private cdr: ChangeDetectorRef,
    private messageService: MessageService // Inject the MessageService
  ) {}

  invalidCredentials: boolean = false;
  isEmailCorrect: boolean = false;
  invalidEmail: boolean = false;

  loginForm: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required])
  });

  ngOnInit(): void {}

  validateEmail(): void {
    this.userService.validateEmail(this.loginForm?.value.email).subscribe(
      res => {
        if (res === false) {
          this.invalidEmail = true;
          this.showError('Invalid Email');
        } else {
          this.invalidEmail = false;
          this.isEmailCorrect = true;
          this.cdr.detectChanges();
        }
      },
      error => {
        this.showError('Email validation failed.');
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
          this.showSuccess('User logged in successfully');
          localStorage.setItem('token', loginResponse.token);
          localStorage.setItem('userId', loginResponse.userId);
          this.authService.login(loginResponse.userId, loginResponse.token);

          setTimeout(() => {
            this.router.navigate(['/home']);
          }, 2000);
        },
        error: error => {
          this.invalidCredentials = true;
          this.showError('Invalid credentials. Please try again.');
          console.log(error);
        }
      });
    }
  }

  // Function to show success messages
  showSuccess(message: string) {
    this.messageService.add({ severity: 'success', summary: 'Success', detail: message });
  }

  // Function to show error messages
  showError(message: string) {
    this.messageService.add({ severity: 'error', summary: 'Error', detail: message });
  }
}
