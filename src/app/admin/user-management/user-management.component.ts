import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common'; // Import CommonModule here
import { UserService } from '../../services/userService/user.service';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.scss'],
  standalone: true,
  imports: [CommonModule], // Add CommonModule to imports array
})
export class UserManagementComponent implements OnInit {
  users: any[] = []; // Lista e përdoruesve

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  // Funksioni për të ngarkuar përdoruesit
  loadUsers() {
    this.userService.getAllUsers().subscribe((data) => {
      this.users = data;
    });
  }

  // Funksioni për të fshirë një përdorues
  deleteUser(id: string) {
    this.userService.deleteUser(id).subscribe(() => {
      this.loadUsers(); // Rifreskon listën pas fshirjes
    });
  }
}
