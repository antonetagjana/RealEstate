import { Component, OnInit } from '@angular/core';
import { ReservationService } from '../../services/reservationService/reservation.service';

@Component({
  selector: 'app-seller-reservation',
  standalone: true,
  imports: [],
  templateUrl: './seller-reservation.component.html',
  styleUrl: './seller-reservation.component.scss'
})
export class SellerReservationComponent implements OnInit {
  reservations: any[] = [];
  loading: boolean = true;
  
  constructor(private reservationService: ReservationService) {}
  
  ngOnInit() {
    this.loadReservations();
  }
  
  loadReservations() {
    this.reservationService.getAllReservations().subscribe(
      (data) => {
        this.reservations = data;
        this.loading = false;
      },
      (error) => {
        console.error('Error fetching reservations:', error);
        this.loading = false;
      }
    );
  }

  getStatusClass(status: string): string {
    switch (status.toLowerCase()) {
      case 'approved': return 'status-approved';
      case 'pending': return 'status-pending';
      case 'declined': return 'status-declined';
      default: return '';
    }
  }
}
