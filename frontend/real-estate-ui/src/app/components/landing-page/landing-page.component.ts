import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HeaderComponent } from '../header/header.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PropertyService } from '../../services/propertyService/property.service';
import { UserService } from '../../services/userService/user.service';
import { AuthService } from '../../services/authService/auth.service';

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
  selector: 'app-landing-page',
  standalone: true,
  imports: [HeaderComponent, FormsModule, CommonModule],
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.scss']
})
export class LandingPageComponent implements OnInit {
  searchTerm: string = '';
  featuredProperties: Property[] = [];
  isLoading: boolean = false;

  constructor(private router: Router, private propertyService: PropertyService) { }

  ngOnInit(): void {
    this.loadFeaturedProperties();
  }

  loadFeaturedProperties(): void {
    this.isLoading = true;
    this.propertyService.getAllProperties().subscribe({
      next: (properties: any) => {
        console.log('Properties received:', properties);
  
        // Use `$values` from `photos` if it exists
        if (Array.isArray(properties.$values)) {
          this.featuredProperties = properties.$values.map((prop: any) => ({
            id: prop.propertyId,
            title: prop.title,
            location: prop.location,
            price: prop.price,
            bedrooms: prop.bedrooms || 0,
            bathrooms: prop.bathrooms || 0,
            area: prop.surfaceArea || 0,
            // Add the base URL to the image path if it exists, otherwise use a default image
            image: prop.photos && prop.photos.$values && prop.photos.$values.length > 0
              ? `http://localhost:5000${prop.photos.$values[0].photoUrl}`
              : 'assets/default.jpg'
          }));
          console.log("Featured properties with images:", this.featuredProperties);
        } else {
          console.error("Expected an array of properties but got:", properties);
          this.featuredProperties = [];
        }
  
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading properties:', err);
        this.isLoading = false;
      }
    });
  }

  search(): void {
    if (!this.searchTerm.trim()) {
      console.log("load features properties")
      // If no search term is provided, load all properties
      this.loadFeaturedProperties();
      return;
    }
  
    console.log('Searching for:', this.searchTerm);
  
    // Fetch properties by location
    this.propertyService.getPropertiesByLocation(this.searchTerm).subscribe({
      next: (filteredProperties: any) => {
        console.log("properties", filteredProperties)
        if (Array.isArray(filteredProperties)) {
          this.featuredProperties = filteredProperties.map((prop: any) => ({
            id: prop.propertyId,
            title: prop.title,
            location: prop.location,
            price: prop.price,
            bedrooms: prop.bedrooms || 0,
            bathrooms: prop.bathrooms || 0,
            area: prop.surfaceArea || 0,
            image: prop.photos && prop.photos.length > 0
              ? `http://localhost:5000${prop.photos[0].photoUrl}`
              : 'assets/default.jpg',
          }));
        } else {
          console.log("test");
          this.featuredProperties = [];
        }
      },
      error: (err) => {
        console.error('Error fetching properties:', err);
        this.featuredProperties = [];
      },
    });
  }
  
  viewDetails(propertyId: number): void {
    this.router.navigate(['/property', propertyId]);
  }

  startSearch(): void {
    this.router.navigate(['/search']);
  }

  goToProperties(): void {
    this.router.navigate(['/properties']);
  }
}
