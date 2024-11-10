import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PropertyListComponent } from './property-list/property-list.component';
import { FavoritesComponent } from './favorites/favorites.component';
import { PropertyDetailComponent } from './property-details/property-details.component';
import { BuyerDashboardComponent } from './buyer-dashboard/buyer-dashboard.component';


const routes: Routes = [
  { path: 'dashboard', component: BuyerDashboardComponent }, // Default route within buyer module
    { path: 'property/:propertyId', component: PropertyDetailComponent },
    {path: 'favorites', component: FavoritesComponent},
  { path: 'property-list', component: PropertyListComponent }, // Rrugë për listën e pronave
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BuyerRoutingModule { }
