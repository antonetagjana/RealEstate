import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { UserService } from '../../services/userService/user.service';
import { Router } from '@angular/router';

interface User {
  id: string;
  name: string;
  email: string;
  status: string;
  createdAt: string;
}

@Component({
  selector: 'app-manage-users',
  templateUrl: './manage-users.component.html',
  standalone: true,
  imports: [CommonModule],  
  styleUrls: ['./manage-users.component.scss']
})
export class ManageUsersComponent implements OnInit {
  users: User[] = [];
  loading: boolean = true;

  constructor(private userService: UserService, private router: Router) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.loading = true;
    this.userService.getAllUsers().subscribe({
      next: (response: any) => {
        console.log('Users received:', response);
  
        // Access $values in case it is wrapped inside the response
        const usersArray = response.$values || [];
        
        // Now map the users
        if (Array.isArray(usersArray)) {
          this.users = usersArray.map((user: any) => ({
            id: user.userId,  // Make sure you're using the correct ID field
            name: user.fullName,
            email: user.email,
            status: user.status,
            createdAt: user.createdAt,
          }));
        } else {
          this.users = [];
        }
  
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading users:', err);
        this.loading = false;
      }
    });
  }
  

  // Helper function to get CSS classes for user status
  getStatusClass(status: string): string {
    switch (status?.toLowerCase()) {
      case 'active': return 'status-active';
      case 'inactive': return 'status-inactive';
      default: return '';
    }
  }

  // Navigate to user details page
  viewUserDetails(userId: string): void {
    this.router.navigate(['/admin/user-details', userId]);
  }
}
