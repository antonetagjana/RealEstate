import { Component, OnInit } from '@angular/core';
import { PropertyService } from '../../services/propertyService/property.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { PropertyFilterComponent } from '../property-filter/property-filter.component';

@Component({
  selector: 'app-property-list',
  templateUrl: './property-list.component.html',
  styleUrls: ['./property-list.component.scss']
})
export class PropertyListComponent implements OnInit {
  properties: any[] = [];
  isLoading = true;
  currentPage = 1;
  pageSize = 10;


  filters = {
    minPrice: null,
    maxPrice: null,
    category: '',
    location: '',
    floors: null,
  };

  constructor(private propertyService: PropertyService,private router: Router) {}

  ngOnInit(): void {
    // Nëse ka filtra të vendosur (default), përdor filtrat
    if (this.filters.minPrice || this.filters.maxPrice || this.filters.category || this.filters.location || this.filters.category) {
      
      this.loadFilteredProperties(this.filters);
    } else {
      // Nëse nuk ka filtra, ngarko të gjitha pronat
      this.loadProperties();
    }
  }

    // Metoda që do të thirret kur ndryshojnë filtrat
    onFiltersChanged(newFilters: any): void {
      console.log('Filters changed:', newFilters); // Kontrollo filtrat e rinj
      this.filters = newFilters; // Përditëso filtrat
      this.loadFilteredProperties(this.filters); // Rifresko listën e pronave me filtrat e rinj
    }
  
    loadFilteredProperties(filters: any): void {
      this.isLoading = true;
    
      this.propertyService
        .getFilteredProperties(
          filters.minPrice,
          filters.maxPrice,
          filters.category,
          filters.location,
          filters.floors
        )
        .subscribe({
          next: (response: any) => {  // Type assertion to `any`
            console.log('Filtered properties received:', response);
            this.properties = response["$values"] || [];
            this.isLoading = false;
          },
          error: (err) => {
            console.error('Error loading filtered properties:', err);
            this.isLoading = false;
          },
        });
    }
    
    loadProperties(): void {
      this.isLoading = true;
      this.propertyService.getAllProperties().subscribe({
        next: (response: any) => {  // Type assertion to `any`
          console.log('Properties received:', response);
          this.properties = response["$values"] || [];
          this.isLoading = false;
        },
        error: (err) => {
          console.error('Error loading properties:', err);
          this.isLoading = false;
        }
      });
    }
    
   // Metoda për të marrë pronat për faqen aktuale
   getPagedProperties(): any[] {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    return this.properties.slice(startIndex, endIndex);
  }

  viewDetails(propertyId: string): void {
    this.router.navigate(['/property', propertyId]);
  }

  // Metoda për të kaluar te faqja tjetër
  nextPage(): void {
    if ((this.currentPage * this.pageSize) < this.properties.length) {
      this.currentPage++;
    }
  }
  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }
}
