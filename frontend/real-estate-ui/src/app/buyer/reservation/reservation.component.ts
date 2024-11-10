import { Component, OnInit } from '@angular/core';
import { ReservationService } from '../../services/reservationService/reservation.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.scss']
})
export class ReservationComponent implements OnInit {
  propertyId!: string;
  reservations: any[] = [];
  newReservation: any = {};
  isLoading = false;

  constructor(
    private reservationService: ReservationService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.propertyId = this.route.snapshot.paramMap.get('propertyId') || '';
    this.loadReservations();
  }

  loadReservations(): void {
    this.isLoading = true;
    this.reservationService.getReservationsByPropertyId(this.propertyId).subscribe({
      next: (reservations) => {
        this.reservations = reservations;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading reservations:', err);
        this.isLoading = false;
      }
    });
  }

  createReservation(): void {
    this.newReservation.propertyId = this.propertyId;
    this.reservationService.createReservation(this.newReservation).subscribe({
      next: () => {
        this.loadReservations();
        this.newReservation = {};
      },
      error: (err) => {
        console.error('Error creating reservation:', err);
      }
    });
  }
}
