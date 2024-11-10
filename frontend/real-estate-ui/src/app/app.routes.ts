import { Routes } from '@angular/router';
import { PropertyListComponent } from './buyer/property-list/property-list.component';
import { ProfileComponent } from './components/profile/profile.component';
import { AuthGuard } from './guard/auth.guard'; // Importo AuthGuard
import { LandingPageComponent } from './components/landing-page/landing-page.component';

import { BuyerDashboardComponent } from './buyer/buyer-dashboard/buyer-dashboard.component';
import { PropertyDetailComponent } from './buyer/property-details/property-details.component';

export const routes: Routes = [
  // Rrugë publike, e aksesueshme nga të gjithë
  { path: '', component: LandingPageComponent }, 
  { path: 'properties', component: PropertyListComponent }, 
  {path: 'property/:id',component: PropertyDetailComponent},
  
  // Rruga `profile`, e aksesueshme nga të gjithë përdoruesit e autentikuar
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [AuthGuard]
  },
  
  // Moduli `admin`, vetëm për përdoruesit me rolin `admin`
  {
    path: 'admin',
    loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule),
   // canActivate: [AuthGuard], // Vetëm për admin
    data: { role: 'admin' } // Kalo të dhënat për rolin që do kontrolluar nga AuthGuard
  },
  
  // Moduli `seller`, vetëm për përdoruesit me rolin `seller`
  {
    path: 'seller',
    loadChildren: () => import('./seller/seller.module').then(m => m.SellerModule),
   // canActivate: [AuthGuard], // Vetëm për seller
    data: { role: 'seller' }
  },

  // Moduli `buyer`, vetëm për përdoruesit me rolin `buyer`
  {
    path: 'buyer',
    loadChildren: () => import('./buyer/buyer.module').then(m => m.BuyerModule),
  //  canActivate: [AuthGuard], // Vetëm për buyer
    data: { role: 'buyer' }
  },

  // Rruga për gabime 404
  { path: '**', redirectTo: '' } 
];
