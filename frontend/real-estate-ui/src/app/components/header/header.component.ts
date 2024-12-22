import { Component } from '@angular/core';
import { ProfileComponent } from '../profile/profile.component';
import { CommonModule } from '@angular/common';
import { LoginComponent } from '../login/login.component';
import { RegistrationComponent } from '../registration/registration.component';
import { AuthService } from '../../services/authService/auth.service'; // Importo AuthService
import { Router } from '@angular/router'; // Importo Router

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  imports: [ProfileComponent, CommonModule, LoginComponent, RegistrationComponent]
})
export class HeaderComponent {
  showProfileMenu = false;
  showLogin = true; // fillimisht shfaqet login
  isLoggedIn=false;

  constructor(
    private authService: AuthService, // Injeksioni i AuthService
    private router: Router // Injeksioni i Router
  ) {}

  ngOnInit(): void {

    this.isLoggedIn = this.authService.isLoggedIn();

  }

  // Ndërron shfaqjen e modalit
  toggleProfileMenu() {
    this.showProfileMenu = !this.showProfileMenu;
  }

  // Funksion për të ndërruar midis Login dhe Register
  toggleForm(): void {
    this.showLogin = !this.showLogin;
  }

  // Mbyll modalin
  closeModal(): void {
    this.showProfileMenu = false;
  }


}
