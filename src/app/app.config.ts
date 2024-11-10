import { ApplicationConfig } from '@angular/core';
import { provideHttpClient, withInterceptors, withFetch } from '@angular/common/http'; // Ndrysho @angular/core në @angular/common/http
import { inject } from '@angular/core';
import { AuthService } from './services/authService/auth.service';

// Krijo një HttpInterceptorFn për të përdorur AuthService
export const authInterceptorFn = (req: any, next: any) => {
  const authService = inject(AuthService);
  const authReq = req.clone({
    setHeaders: {
      Authorization: `Bearer ${authService.getToken()}`
    }
  });
  return next(authReq);
};

// Krijo appConfig duke përdorur ApplicationConfig
export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(withFetch(), withInterceptors([authInterceptorFn]))
  ]
};
