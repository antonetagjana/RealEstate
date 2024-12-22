import { Component, OnInit } from '@angular/core';
import { ReservationService } from '../../services/reservationService/reservation.service';
import { PropertyService } from '../../services/propertyService/property.service';
import { FavoriteService } from '../../services/favoriteService/favorite.service';
import { UserService } from '../../services/userService/user.service';
import { HeaderComponent } from '../../components/header/header.component';
import { AuthService } from '../../services/authService/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-buyer-dashboard',
  templateUrl: './buyer-dashboard.component.html',
  styleUrls: ['./buyer-dashboard.component.scss'],
})
export class BuyerDashboardComponent implements OnInit {
  reservationsCount: number = 0;
  favoritesCount: number = 0;
  propertiesCount: number = 0;
  userName: string = '';

  constructor(
    private reservationService: ReservationService,
    private propertyService: PropertyService,
    private favoriteService: FavoriteService,
    private userService: UserService,
    private authService:AuthService,
    private router:Router
  ) {}

  ngOnInit(): void {
    console.log("BuyerDashboardComponent ngarkohet");

    this.loadDashboardData();
  }

  loadDashboardData(): void {
    this.reservationService.getReservationCount().subscribe(
      (count) => this.reservationsCount = count,
      (error) => console.error('Gabim gjatë ngarkimit të numrit të rezervimeve', error)
    );

    this.propertyService.getPropertyCount().subscribe(
      (count) => this.propertiesCount = count,
      (error) => console.error('Gabim gjatë ngarkimit të numrit të pronave', error)
    );

    this.favoriteService.getFavorites().subscribe(
      (count) => this.favoritesCount = count,
      (error) => console.error('Gabim gjatë ngarkimit të numrit të preferencave', error)
    );

    this.userService.getUserProfile().subscribe(
      (user) => this.userName = user.fullName,
      (error) => console.error('Gabim gjatë ngarkimit të profilit të përdoruesit', error)
    );
  }

  logout(): void{
    this.authService.logout();
    this.router.navigate(['/']);
  }
}
