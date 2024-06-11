import {Component, OnInit} from '@angular/core';
import {CardModule} from "primeng/card";
import {ButtonModule} from "primeng/button";
import {InputTextModule} from "primeng/inputtext";
import {MessageModule} from "primeng/message";
import {NgClass, NgIf} from "@angular/common";
import {FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {UserServiceService} from "../../services/user-service.service";

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    CardModule,
    ButtonModule,
    InputTextModule,
    MessageModule,
    NgIf,
    ReactiveFormsModule,
    NgClass
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit{

  constructor(private formBuilder: FormBuilder,
              protected router: Router,
              private userService: UserServiceService) {}

  registerForm: FormGroup =  new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email, Validators.minLength(10)]),
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required, Validators.minLength(10)]),
    age: new FormControl(null, [Validators.required, Validators.min(18)])
  });

  ngOnInit(): void {
  }
  onSubmit(): void {
    if(this.registerForm.valid){
      this.userService.registerUser(this.registerForm.value).subscribe({
        next: (user) => {
          window.alert("User registered successfully");
          this.router.navigate(['login']);
        },
        error: error => {
          console.log('Error:');
          console.log(error);
        }
      })
    }
    else{
      this.registerForm.markAsTouched();
    }
  }
  getErrorMessage(controlName: string): string | null {
    const control = this.registerForm.get(controlName);

    if(control && control.touched){
      if(control.errors?.['required'])
        return 'This field is required';
      if (control.errors?.['min']) {
        return 'The minimum allowed age is 18.';
      }
      if (control.errors?.['minlength']) {
        return `Minimum length should be ${control.errors?.['minlength'].requiredLength}`;
      }
      if (control.errors?.['email']) {
        return 'Please enter a valid email address.';
      }
    }
    return null;
  }
}
