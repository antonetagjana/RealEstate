import { CommonModule } from '@angular/common'; 
import { Router, RouterModule } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { PropertyService } from '../../services/propertyService/property.service'; 

interface Property {
  id: string;
  title: string;
  location: string;
  price: number;
  status: string;
  updatedAt: string;
  image: string;
}

@Component({
  selector: 'app-admin-property',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule
  ],
  templateUrl: './admin-property.component.html',
  styleUrls: ['./admin-property.component.scss']
})
export class AdminPropertyComponent implements OnInit {
  properties: Property[] = []; 
  loading: boolean = true;

  constructor(private propertyService: PropertyService, private router: Router) {}

  ngOnInit(): void {
    this.loadProperties();
  }

  loadProperties(): void {
    this.loading = true;
    this.propertyService.getAllProperties().subscribe({
      next: (properties: any) => {
        console.log('Properties received:', properties);

        if (Array.isArray(properties.$values)) {
          this.properties = properties.$values.map((prop: any) => ({
            id: prop.propertyId,
            title: prop.title,
            location: prop.location,
            price: prop.price,
            status: prop.status,
            updatedAt: prop.updatedAt,
            image: prop.photos && prop.photos.$values && prop.photos.$values.length > 0
              ? `http://localhost:5000${prop.photos.$values[0].photoUrl}`
              : 'assets/default.jpg'
          }));
          console.log('Processed properties:', this.properties);
        } else {
          this.properties = [];
        }

        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading properties:', err);
        this.loading = false;
      }
    });
  }

  getStatusClass(status: string): string {
    switch (status?.toLowerCase()) {
      case 'active': return 'status-active';
      case 'inactive': return 'status-inactive';
      default: return '';
    }
  }

  viewDetails(propertyId: string): void {
    this.router.navigate(['/property', propertyId]);
  }

  // Helper method to format the date manually
  formatDate(dateString: string): string {
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', {
      month: 'short',
      day: 'numeric',
      year: 'numeric'
    });
  }
}
