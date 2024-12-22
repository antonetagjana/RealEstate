import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserManagementComponent } from './user-management/user-management.component'; // Për komponentët që ke
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { AdminReservationComponent } from './admin-reservation/admin-reservation.component';
import { AdminPropertyComponent } from './admin-property/admin-property.component';
import { ManageUsersComponent } from './manage-users/manage-users.component';

const routes: Routes = [
  {path: 'dashboard',component: AdminDashboardComponent},
  { path: 'user-management', component: UserManagementComponent }, // Rrugë për menaxhimin e përdoruesve
  { path: 'admin/reservations', component: AdminReservationComponent},
  {path:'admin/property',component: AdminPropertyComponent},
 {path:'admin/manage-users',component: ManageUsersComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
