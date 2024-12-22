import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PropertyManagementComponent } from './property-management/property-management.component'; // Për komponentët që ke
import { SellerDashboardComponent } from './seller-dashboard/seller-dashboard.component';
import { SellerReservationComponent } from './seller-reservation/seller-reservation.component';

const routes: Routes = [
  {path: 'properties',component: PropertyManagementComponent},
  {path: 'dashboard',component:SellerDashboardComponent},
  {path: 'properties/add', component: PropertyManagementComponent }, 
  {path: 'seller/reservations', component: SellerReservationComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SellerRoutingModule { }
