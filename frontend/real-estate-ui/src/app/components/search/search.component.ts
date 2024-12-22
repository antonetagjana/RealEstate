import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { HeaderComponent } from '../header/header.component';
import { PropertyService } from '../../services/propertyService/property.service';

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

interface SearchFilters {
  location: string;
  propertyType: string;
  minPrice: number | null;
  maxPrice: number | null;
  bedrooms: string;
  bathrooms: string;
}

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [FormsModule, CommonModule, HeaderComponent],
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {
  properties: Property[] = [];
  filters: SearchFilters = {
    location: '',
    propertyType: '',
    minPrice: null,
    maxPrice: null,
    bedrooms: '',
    bathrooms: ''
  };
   
  isLoading = true;
  constructor(private router: Router, private propertyService: PropertyService) { }

  ngOnInit(): void {
    this.loadProperties();
  }

  loadProperties(): void {
    this.isLoading = true;
    this.propertyService.getAllProperties().subscribe({
      next: (properties) => {
        this.properties = properties;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading properties:', err);
        this.isLoading = false;
      }
    });
  }
  
  applyFilters(): void {
    console.log('Applying filters:', this.filters);
    // In a real application, you would send these filters to a service
    // and update the properties based on the filtered results
  }

  viewDetails(propertyId: number): void {
    this.router.navigate(['/property', propertyId]);
  }
}
