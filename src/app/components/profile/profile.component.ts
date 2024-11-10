import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/userService/user.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  imports: [FormsModule, CommonModule],
  standalone: true,
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  profile = {
    fullName: '',
    email: '',
    phone: '',
    address: ''
  };
  errorMessage = '';
  successMessage = '';

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.loadUserProfile();
  }

  loadUserProfile(): void {
    // Attempt to fetch fresh data from backend
    this.userService.getUserProfile().subscribe({
      next: (data) => {
        console.log("Fetched fresh user data:", data);
        this.profile = data;
        localStorage.setItem('user', JSON.stringify(data)); // Update localStorage with fresh data
      },
      error: (err) => {
        console.error('Error fetching fresh user data:', err);
        // Fallback to localStorage data if fresh data cannot be fetched
        const userData = localStorage.getItem('user');
        if (userData) {
          console.log("Using cached user data:", userData);
          this.profile = JSON.parse(userData);
        } else {
          this.errorMessage = 'No user data found';
        }
      }
    });
  }
  

  updateProfile(): void {
    this.userService.updateUserProfile(this.profile).subscribe({
      next: () => {
        this.successMessage = 'Profile updated successfully!';
      },
      error: (err) => {
        this.errorMessage = 'Error updating profile';
        console.error('Error updating profile:', err);
      }
    });
  }

  changePassword(newPassword: string): void {
    this.userService.changePassword(newPassword).subscribe({
      next: () => {
        this.successMessage = 'Password changed successfully!';
      },
      error: (err) => {
        this.errorMessage = 'Error changing password';
        console.error('Error changing password:', err);
      }
    });
  }
}
