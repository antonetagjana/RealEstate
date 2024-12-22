import { Component, EventEmitter, Output } from '@angular/core';
import { AuthService } from '../../services/authService/auth.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  standalone: true,
  styleUrls: ['./login.component.scss'],
  imports: [FormsModule, CommonModule]
})
export class LoginComponent {
  @Output() close = new EventEmitter<void>(); // Event për të mbyllur modalin
  credentials = {
    email: '',
    password: ''
  };
  errorMessage = '';

  constructor(private authService: AuthService, private router: Router) {}

  login(): void {
    this.authService.login(this.credentials).subscribe({
      next: (response) => {
        // Ruaj token dhe userId
        this.authService.setToken(response.token);
        localStorage.setItem('role', response.role);
        localStorage.setItem('userId', response.userId);
        const role = response.role;

        // Ridrejtim sipas rolit
        if (role === 'Seller') {
          this.router.navigate(['/seller/dashboard']);
        } else if (role === 'Buyer') {
          this.router.navigate(['/buyer/dashboard']);
        } else if (role === 'Admin') {
          this.router.navigate(['/admin/dashboard']);
        } else {
          this.router.navigate(['/profile']);
        }

        // Njofto komponentin prind që të mbyllet modali
        this.close.emit();
      },
      error: (err) => {
        this.errorMessage = 'Invalid email or password';
      }
    });
  }

  closeLoginModal(): void {
    this.close.emit(); // Mbyll modalin manualisht
  }
}
