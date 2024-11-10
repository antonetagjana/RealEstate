import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserManagementComponent } from './user-management/user-management.component'; // Për komponentët që ke
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';

const routes: Routes = [
  {path: 'dashboard',component: AdminDashboardComponent},
  { path: 'user-management', component: UserManagementComponent }, // Rrugë për menaxhimin e përdoruesve
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
