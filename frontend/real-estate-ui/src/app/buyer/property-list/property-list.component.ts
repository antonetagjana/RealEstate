import { Component, OnInit } from '@angular/core';
import { PropertyService } from '../../services/propertyService/property.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';


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

  constructor(private propertyService: PropertyService,private router: Router) {}

  ngOnInit(): void {
    this.loadProperties();
  }

  loadProperties(): void {
    this.isLoading = true;
    this.propertyService.getAllProperties().subscribe({
      next: (properties) => {
        console.log('Properties received:', properties); // Kontrollo të dhënat e marra nga serveri
        this.properties = properties;
        this.isLoading= false;
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
