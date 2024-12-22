import { Component } from '@angular/core';
import { AuthService } from '../../services/authService/auth.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  standalone: true,
  styleUrls: ['./registration.component.scss'],
  imports: [CommonModule, FormsModule]
})
export class RegistrationComponent {
  fullName: string = '';
  email: string = '';
  password: string = '';
  confirmPassword: string = '';
  phoneNumber: string = '';
  role: string = 'Buyer'; // Default role
  availableRoles: string[] = ['Buyer', 'Seller', 'Admin'];
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  register(): void {
    if (this.password !== this.confirmPassword) {
      this.errorMessage = 'Passwords do not match';
      return;
    }

    const userData = {
      fullName: this.fullName,
      email: this.email,
      password: this.password,
      phoneNumber: this.phoneNumber,
      role: this.role
    };

    this.authService.register(userData).subscribe({
      next: (response) => {
        this.authService.setToken(response.token);
        this.router.navigate(['/landing-page']); // Redirect to profile after registration
      },
      error: (err) => {
        if (err.status === 400 && err.error?.message) {
          // Display the specific error message from the backend
          this.errorMessage = err.error.message;
        } else {
          // Generic error message for other types of errors
          this.errorMessage = 'Registration failed. Please try again.';
        }
        console.error('Error during registration:', err);
      }
    });
  }
}
