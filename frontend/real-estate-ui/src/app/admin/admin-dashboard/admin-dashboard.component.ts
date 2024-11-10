import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/userService/user.service';
import { PropertyService } from '../../services/propertyService/property.service';
import { ReservationService } from '../../services/reservationService/reservation.service';

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
    private  reservationService:ReservationService
  ) {}

  ngOnInit(): void {
    // Mund të vendosësh thirrje API këtu për të marrë të dhëna mbi statistikat
    this.loadStatistics();
  }

  loadStatistics(): void {
    this.userService.getUserCount().subscribe(
      (count) => this.usersCount = count,
      (error) => console.error('Error loading user count', error)
    );

    this.propertyService.getPropertyCount().subscribe(
      (count) => this.propertiesCount = count,
      (error) => console.error('Error loading property count', error)
    );

    this.reservationService.getReservationCount().subscribe(
      (count) => this.reservationsCount = count,
      (error) => console.error('Error loading reservation count', error)
    );
  }
}
