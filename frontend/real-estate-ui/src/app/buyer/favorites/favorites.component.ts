import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HeaderComponent } from '../../components/header/header.component';
import { CommonModule } from '@angular/common';

interface Property {
  id: number;
  title: string;
  location: string;
  price: number;
  bedrooms: number;
  bathrooms: number;
  area: number;
  image: string;
}

@Component({
  selector: 'app-favorites',
  standalone:true,
  imports:[HeaderComponent,CommonModule],
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.scss']
})
export class FavoritesComponent implements OnInit {
  favorites: Property[] = [];

  constructor(private router: Router) { }

  ngOnInit(): void {
    // In a real application, you would fetch the user's favorites from a service
    this.favorites = [
      {
        id: 1,
        title: 'Modern Downtown Apartment',
        location: 'New York, NY',
        price: 500000,
        bedrooms: 2,
        bathrooms: 2,
        area: 1000,
        image: 'assets/apartment1.jpg'
      },
      {
        id: 2,
        title: 'Suburban Family Home',
        location: 'Los Angeles, CA',
        price: 750000,
        bedrooms: 4,
        bathrooms: 3,
        area: 2500,
        image: 'assets/house1.jpg'
      },
      {
        id: 3,
        title: 'Beachfront Condo',
        location: 'Miami, FL',
        price: 600000,
        bedrooms: 3,
        bathrooms: 2,
        area: 1500,
        image: 'assets/condo1.jpg'
      }
    ];
  }

  viewDetails(propertyId: number): void {
    // Navigate to property details page
    this.router.navigate(['/property', propertyId]);
  }

  removeFavorite(propertyId: number): void {
    this.favorites = this.favorites.filter(property => property.id !== propertyId);
    // In a real application, you would also update this on the server
    console.log(`Removed property ${propertyId} from favorites`);
  }
}