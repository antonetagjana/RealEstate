import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PropertyService } from '../../services/propertyService/property.service';
import { ReservationService } from '../../services/reservationService/reservation.service';

@Component({
  selector: 'app-property-detail',
  templateUrl: './property-details.component.html',
  styleUrls: ['./property-details.component.scss']
})
export class PropertyDetailComponent implements OnInit {
  property: any;
  image: string;
  isLoading = true;
  propertyId: string = '';
  selectedPropertyId:string='';
  showReservationForm: boolean = false; // Controls the display of the reservation form
  loggedInUserId: string = ''; // User ID to pass to the reservation component

  constructor(
    private propertyService: PropertyService,
    private route: ActivatedRoute,
    private router: Router,
  ) {}

  ngOnInit(): void {
    // Retrieve the property ID from the route parameters
    this.route.paramMap.subscribe(params => {
      this.propertyId = params.get('id') || '';
      this.selectedPropertyId = this.propertyId;
      console.log('Retrieved Property ID:', this.propertyId);
      if (this.propertyId) {
        this.loadPropertyDetails(this.propertyId);
      } else {
        console.error('Property ID is missing');
        this.isLoading = false;
      }
    });

    // Retrieve logged-in user ID from localStorage (assuming it's stored there on login)
    this.loggedInUserId = localStorage.getItem('userId') || '';
    if (!this.loggedInUserId) {
      console.error('User ID is missing or user not logged in');
    }
  }

  // Toggle the reservation form display
  openReservationForm() {
    console.log("Opening reservation form for property ID:", this.propertyId);
    this.showReservationForm = true;
  }

  // Handle the reservation completion event from the reservation component
  handleReservationComplete(success: boolean) {
    if (success) {
      this.showReservationForm = false;
      console.log('Reservation completed successfully');
      // Additional actions, such as refreshing the property details or view, can be added here
    }
  }

  // Load property details based on the property ID
  loadPropertyDetails(id: string): void {
    this.isLoading = true;
    console.log('Calling getPropertyById with ID:', id);
    this.propertyService.getPropertyById(id).subscribe(
      (property) => {
        console.log('Property data:', property);
        this.property = property;
        this.image = this.property.photos && this.property.photos.$values && this.property.photos.$values.length > 0
          ? `http://localhost:5000${this.property.photos.$values[0].photoUrl}`
          : 'assets/default.jpg';
        this.isLoading = false;
      },
      (error) => {
        console.error('Error loading property details:', error);
        this.isLoading = false;
      }
    );
  }
}
