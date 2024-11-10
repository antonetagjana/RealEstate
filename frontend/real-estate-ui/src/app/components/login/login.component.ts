import { Component } from '@angular/core';
import { AuthService } from '../../services/authService/auth.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  standalone:true,
  styleUrls: ['./login.component.scss'],
  imports:[FormsModule,CommonModule]
})
export class LoginComponent {
  credentials = {
    email: '',
    password: ''
  };
  errorMessage = '';

  constructor(private authService: AuthService, private router: Router) {}

  login(): void {
    this.authService.login(this.credentials).subscribe({
      next: (response) => {
        this.authService.setToken(response.token);
        this.router.navigate(['/profile']); // Redirection pas login
      },
      error: (err) => {
        this.errorMessage = 'Invalid email or password';
      }
    });
  }
}
