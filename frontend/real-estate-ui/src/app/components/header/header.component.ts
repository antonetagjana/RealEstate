import { Component } from '@angular/core';
import { ProfileComponent } from '../profile/profile.component';
import { CommonModule } from '@angular/common';
import { LoginComponent } from '../login/login.component';
import { RegistrationComponent } from '../registration/registration.component';

@Component({
  selector: 'app-header',
  standalone:true,
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  imports:[ProfileComponent,CommonModule,LoginComponent,RegistrationComponent]
})
export class HeaderComponent {
  showProfileMenu = false;
  showLogin =true;//fillimisht shfaqet login

  ngOnInit(): void {
    console.log('HeaderComponent initialized');
  }
  

  toggleProfileMenu() {
    this.showProfileMenu = !this.showProfileMenu;
    console.log('Profile menu toggled:', this.showProfileMenu); 
  }

  closeModal() {
    this.showProfileMenu = false;
  }
   // Funksion për të ndërruar midis Login dhe Register
   toggleForm(): void {
    this.showLogin = !this.showLogin;
  }
}
