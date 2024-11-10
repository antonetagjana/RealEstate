import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PropertyService } from '../../services/propertyService/property.service';

@Component({
  selector: 'app-property-detail',
  templateUrl: './property-details.component.html',
  styleUrls: ['./property-details.component.scss']
})
export class PropertyDetailComponent implements OnInit {
  property: any;
  image: string;
  isLoading = true;

  constructor(
    private propertyService: PropertyService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
  
    // Log paramMap për të parë të gjithë parametrat në mënyrë të detajuar
    this.route.paramMap.subscribe(params => {
  
      const propertyId = params.get('id');
      console.log("Retrieved Property ID:", propertyId); // Ky log tregon vlerën e propertyId
  
      if (propertyId) {
        this.loadPropertyDetails(propertyId);
      } else {
        console.error("Property ID is missing");
        this.isLoading = false;
      }
    });
  
    // Log për të parë vlerat e fragmentit dhe të tjerë (opsionale)
    console.log("Route fragment:", this.route.snapshot.fragment);
    console.log("Route queryParams:", this.route.snapshot.queryParams);
  }
  

  loadPropertyDetails(id: string): void {
    this.isLoading = true;
    this.propertyService.getPropertyById(id).subscribe(
      (property) => {
        console.log("Property data:", property);
        this.property = property;
        this.image= this.property.photos && this.property.photos.$values && this.property.photos.$values.length > 0
        ? `http://localhost:5000${this.property.photos.$values[0].photoUrl}`
        : 'assets/default.jpg'
        console.log("iMAGE:", this.image);

        this.isLoading = false;
      },
      (error) => {
        console.error("Error loading property details:", error);
        this.isLoading = false;
      }
    );
  }
}
