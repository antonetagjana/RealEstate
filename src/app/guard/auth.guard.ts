import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from '../services/authService/auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const expectedRole = route.data['role']; // Merr rolin e pritur nga rruga
    const userRole = this.authService.getRole(); // Merr rolin aktual nga AuthService
    
    // Kontrollo nëse përdoruesi është autentikuar dhe ka rolin e duhur
    if (this.authService.isLoggedIn() && (!expectedRole || userRole === expectedRole)) {
      return true;
    } else {
      // Ridrejto te faqja e login nëse nuk është autentikuar ose nuk ka rolin e duhur
      this.router.navigate(['/login']);
      return false;
    }
  }
}
