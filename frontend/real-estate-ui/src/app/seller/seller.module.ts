import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SellerDashboardComponent } from './seller-dashboard/seller-dashboard.component';
import { SellerRoutingModule } from './seller-routing.module';
import { PropertyManagementComponent } from './property-management/property-management.component';
import { PropertyService } from '../services/propertyService/property.service';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    SellerDashboardComponent,
    PropertyManagementComponent
  ],
  imports: [
    CommonModule,
    SellerRoutingModule,
    FormsModule,
  ],
  providers: [PropertyService]
})
export class SellerModule { }
