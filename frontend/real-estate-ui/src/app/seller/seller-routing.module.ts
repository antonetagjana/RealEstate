import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PropertyManagementComponent } from './property-management/property-management.component'; // Për komponentët që ke
import { SellerDashboardComponent } from './seller-dashboard/seller-dashboard.component';

const routes: Routes = [
  {path: 'property-add',component: PropertyManagementComponent},
  {path: 'dashboard',component:SellerDashboardComponent},
  { path: 'property-management', component: PropertyManagementComponent }, // Rrugë për menaxhimin e pronave
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SellerRoutingModule { }
