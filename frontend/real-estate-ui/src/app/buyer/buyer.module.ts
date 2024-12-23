import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BuyerRoutingModule } from './buyer-routing.module';
import { BuyerDashboardComponent } from './buyer-dashboard/buyer-dashboard.component';
import { ReservationService } from '../services/reservationService/reservation.service';
import { PropertyService } from '../services/propertyService/property.service';
import { FavoriteService } from '../services/favoriteService/favorite.service';
import { UserService } from '../services/userService/user.service';
import { PropertyDetailComponent } from './property-details/property-details.component';
import { PropertyListComponent } from './property-list/property-list.component';
import { FormsModule } from '@angular/forms';
import { ReservationComponent } from './reservation/reservation.component';
import { PropertyFilterComponent } from './property-filter/property-filter.component';



@NgModule({
  declarations: [
    BuyerDashboardComponent,
    PropertyDetailComponent,
    PropertyListComponent,
    ReservationComponent,
    PropertyFilterComponent
  ],
  imports: [
    FormsModule,
    CommonModule,
    BuyerRoutingModule
  ],
  providers:[
    ReservationService,
    PropertyService,
    FavoriteService,
    UserService,
    PropertyFilterComponent
  ]
})
export class BuyerModule { }
