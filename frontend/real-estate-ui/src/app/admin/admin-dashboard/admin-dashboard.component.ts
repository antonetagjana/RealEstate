import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/userService/user.service';
import { PropertyService } from '../../services/propertyService/property.service';
import { ReservationService } from '../../services/reservationService/reservation.service';
import { AuthService } from '../../services/authService/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.scss']
})
export class AdminDashboardComponent implements OnInit {

  usersCount: number = 0;
  propertiesCount: number = 0;
  reservationsCount: number = 0;

  constructor(
    private userService:UserService,
    private propertyService:PropertyService,
    private  reservationService:ReservationService,
    private authService:AuthService,
    private router:Router
  
  ) {}

  ngOnInit(): void {
    this.loadStatistics();
  }
  
  loadStatistics(): void {
    console.log('Starting to load statistics...');
    
    this.userService.getUserCount().subscribe(
      (count) => {
        console.log('Received user count:', count);
        this.usersCount = count;
        console.log('Updated usersCount value:', this.usersCount);
      },
      (error) => {
        console.error('Error loading user count', error);
        this.usersCount = 0;
      }
    );

    this.propertyService.getPropertyCount().subscribe(
      (count) => {
        console.log('Received property count:', count);
        this.propertiesCount = count;
      },
      (error) => {
        console.error('Error loading property count', error);
        this.propertiesCount = 0;
      }
    );

    this.reservationService.getReservationCount().subscribe(
      (count) => {
        console.log('Received reservation count:', count);
        this.reservationsCount = count;
      },
      (error) => {
        console.error('Error loading reservation count', error);
        this.reservationsCount = 0;
      }
    );
}

  logout(): void {
    this.authService.logout(); // Fshin të dhënat nga localStorage
    this.router.navigate(['/']); // Ridrejton në landing page
  }
}
