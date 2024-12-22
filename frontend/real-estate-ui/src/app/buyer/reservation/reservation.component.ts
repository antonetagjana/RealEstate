import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ReservationService } from '../../services/reservationService/reservation.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.scss']
})
export class ReservationComponent implements OnInit {
  @Input() propertyId: string = ''; // Receives propertyId from parent component
  @Input() userId: string = ''; // Receives userId from parent component
  @Output() reservationComplete = new EventEmitter<boolean>(); // To notify the parent component of completion

  newReservation: any = {
    checkIn: '',
    checkOut: '',
  };

  constructor(
    private reservationService: ReservationService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    // Retrieve the user object from localStorage
    const user = localStorage.getItem('user');
    
    if (user) {
      const parsedUser = JSON.parse(user); // Parse the JSON string to an object
      this.userId = parsedUser.userId;     // Access the userId property within the object
      
      if (!this.userId) {
        console.error('User ID is missing in the user object');
      }
    } else {
      console.error('User not logged in');
    }
  }
  

  createReservation(): void {
    if (!this.userId || !this.propertyId) {
      console.error('Missing user or property ID');
      return;
    }

    //send the reservation data to the backend 
    this.reservationService.createReservation(this.userId, this.propertyId, this.newReservation).subscribe({
      next: () => {
        alert('Reservation created successfully!');
        this.reservationComplete.emit(true); // Notify parent of successful reservation
      },
      error: (err) => {
        console.error('Error creating reservation:', err);
        alert('Failed to create reservation. Please try again.');

      }
    });
  }
}
