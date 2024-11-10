import { Component, OnInit } from '@angular/core';
import { PropertyService } from '../../services/propertyService/property.service';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-property-management',
  templateUrl: './property-management.component.html',
  styleUrls: ['./property-management.component.scss'],
})
export class PropertyManagementComponent implements OnInit {
  properties: any[] = []; // Lista e pronave
  isLoading = true;
  private apiUrl = 'http://localhost:5000/api'; // Replace with your actual backend API URL


  newProperty: any = {
    title: '',
    description: '',
    category: '',
    location: '',
    price: 0,
    surfaceArea: 0,
    floors: 0,
    isPromoted: false,
    isAvailable: true
  };
  selectedFile: File | null = null;

  constructor(private propertyService: PropertyService, private http: HttpClient) {}

  ngOnInit(): void {
    this.loadProperties();
  }

  // Funksioni për të ngarkuar pronat
  loadProperties() {
    this.isLoading = true;
    this.propertyService.getAllProperties().subscribe({
      next: (data) => {
        this.properties = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading properties:', err);
        this.isLoading = false;
      }
    });
  }

  addProperty() {
    if (!this.selectedFile) {
      console.error('Please select an image file');
      return;
    }
  
    const formData = new FormData();
    formData.append('title', this.newProperty.title);
    formData.append('description', this.newProperty.description);
    formData.append('category', "test");
    formData.append('location', this.newProperty.location);
    formData.append('price', this.newProperty.price.toString());
    formData.append('surfaceArea', this.newProperty.surfaceArea.toString());
    formData.append('floors', this.newProperty.floors.toString());
    formData.append('isPromoted', this.newProperty.isPromoted.toString());
    formData.append('isAvailable', this.newProperty.isAvailable.toString());
    console.log('Selected file details:', {
      name: this.selectedFile.name,
      size: this.selectedFile.size,
      type: this.selectedFile.type,
  });
    formData.append('photo', this.selectedFile);
  
    // Log the formData values to verify before sending
    console.log('Form data to be sent:');
    formData.forEach((value, key) => {
      console.log(`${key}: ${value}`);
    });
  
    this.isLoading = true;
    this.propertyService.createProperty(formData).subscribe({
      next: (response) => {
        console.log('Property added successfully:', response);
        this.loadProperties();
        this.resetForm();
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error adding property:', err);
        
        // Log detailed validation errors if available
        if (err.error && err.error.errors) {
          console.log('Validation errors:', err.error.errors);
        } else {
          console.log('General error:', err);
        }
  
        this.isLoading = false;
      }
    });
  }
  

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  resetForm() {
    this.newProperty = {
      title: '',
      description: '',
      category: '',
      location: '',
      price: 0,
      surfaceArea: 0,
      floors: 0,
      isPromoted: false,
      isAvailable: true
    };
    this.selectedFile = null;
  }
  
  // Funksioni për të fshirë një pronë
  deleteProperty(id: string) {
    this.isLoading = true;
    this.propertyService.deleteProperty(id).subscribe({
      next: () => {
        console.log('Property deleted successfully');
        this.loadProperties(); // Rifresko listën e pronave pas fshirjes
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error deleting property:', err);
        this.isLoading = false;
      }
    });
  }
}
