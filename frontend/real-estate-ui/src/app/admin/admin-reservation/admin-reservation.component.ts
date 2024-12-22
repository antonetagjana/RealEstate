import { Component, OnInit } from '@angular/core';
import { ReservationService } from '../../services/reservationService/reservation.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { DatePipe } from '@angular/common';

// Define a model for your reservation data
interface Reservation {
  id: string;
  checkIn: string;
  checkOut: string;
  status: string;
  property: {
    name: string;
  };
  user: {
    fullName: string;  // Only using fullName for the user
  };
}

@Component({
  selector: 'app-admin-reservation',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    DatePipe
  ],
  templateUrl: './admin-reservation.component.html',
  styleUrl: './admin-reservation.component.scss'
})
export class AdminReservationComponent implements OnInit {
  reservations: Reservation[] = []; // Specify the type as Reservation
  loading: boolean = true;

  constructor(private reservationService: ReservationService) {}

  ngOnInit() {
    this.loadReservations();
  }

  loadReservations() {
    this.reservationService.getAllReservations().subscribe({
      next: (data: any) => {
        console.log('Raw API Response:', data);
  
        // Ensure data is an array and contains items
        if (Array.isArray(data) && data.length > 0) {
          this.reservations = data.map((reservation: any) => ({
            id: reservation.reservationId,
            checkIn: reservation.checkIn,
            checkOut: reservation.checkOut,
            status: reservation.status,
            // Correctly map property title and user fullName
            property: { name: reservation.property?.title || 'N/A' },  // Use title for property name
            user: {
              fullName: reservation.user?.fullName || 'N/A'  // Use fullName for user
            }
          }));

          console.log('Processed reservations:', this.reservations);
        } else {
          console.error('Invalid or empty response structure:', data);
          this.reservations = [];
        }
        this.loading = false;
      },
      error: (error) => {
        console.error('Error fetching reservations:', error);
        this.loading = false;
      }
    });
  }

  getStatusClass(status: string): string {
    switch (status?.toLowerCase()) {
      case 'approved': return 'status-approved';
      case 'pending': return 'status-pending';
      case 'declined': return 'status-declined';
      default: return '';
    }
  }
}
