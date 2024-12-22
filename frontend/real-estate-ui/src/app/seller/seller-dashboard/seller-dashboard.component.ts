import { Component, OnInit } from '@angular/core';
import { PropertyService } from '../../services/propertyService/property.service';
import { ReservationService } from '../../services/reservationService/reservation.service';
import { HeaderComponent } from '../../components/header/header.component';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/authService/auth.service';
import { Route, Router } from '@angular/router';
import { count } from 'node:console';

@Component({
  selector: 'app-seller-dashboard',
  templateUrl: './seller-dashboard.component.html',
  styleUrls: ['./seller-dashboard.component.scss'],
})
export class SellerDashboardComponent implements OnInit {
  sellerName: string = "Emri i Shitësit"; // Mund ta vendosësh këtë nga të dhënat e përdoruesit të loguar
  listedPropertiesCount: number = 0;
  pendingReservationsCount: number = 0;
  confirmedReservationsCount: number = 0;


  showForm: boolean = false;

  constructor(
    private propertyService: PropertyService,
    private reservationService: ReservationService,
    private authService: AuthService,
    private router: Router
    
  ) {}

  ngOnInit(): void {
    this.loadSellerStatistics();

  }

  // Shto metodën `toggleForm`
  toggleForm() {
    this.showForm = !this.showForm; // Ndërron gjendjen e `showForm`
  }


  logout(): void {
    this.authService.logout(); // Thirr funksionin për të pastruar localStorage
    this.router.navigate(['/']); // Ridrejto te landing page
  }

  loadSellerStatistics(): void {
    // Merr numrin e pronave të listuara
    this.propertyService.getListedPropertiesCount().subscribe(
      (count) => this.listedPropertiesCount = count,
      (error) => console.error('Error loading listed properties count', error)
    );

    // Merr numrin e rezervimeve të konfirmuara
    this.reservationService.getConfirmedReservationsCount().subscribe(
      (count) => this.confirmedReservationsCount = count,
      (error) => console.error('Error loading confirmed reservations count', error)
    );
  }
}
