import { Component } from '@angular/core';
import { AuthService } from '../../services/authService/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  email: string = '';     // Add individual email property
  password: string = '';  // Add individual password property
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  login(): void {
    if (this.email && this.password) {  // Check if email and password are provided
      this.authService.login({ email: this.email, password: this.password }).subscribe({
        next: (response) => {
          this.authService.setToken(response.token);
          this.router.navigate(['/profile']);  // Redirect to profile after login
        },
        error: (err) => {
          this.errorMessage = 'Invalid email or password';
        }
      });
    } else {
      this.errorMessage = 'Email and password are required';
    }
  }
}
