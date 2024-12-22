import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5000/api'; // Ndrysho këtë URL sipas backend-it tënd

  constructor(private http: HttpClient) { }

  // Funksioni për login
  login(credentials: { email: string; password: string }): Observable<any> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/Auth/login`, credentials).pipe(
      map(response => {
        const token = response.token;
        this.setToken(token);
        this.fetchAndStoreUserById();
       return response;
      })
    );
  }
  
  private fetchAndStoreUserById(): void {
    const token = this.getToken();
    if (token) {
      const decodedToken: any = jwtDecode(token);
      const id = decodedToken.nameid; // Log decoded token to check correctness
      console.log("Decoded Token:", decodedToken);
  
      this.http.get(`${this.apiUrl}/User/${id}`).subscribe(
        (user) => {
          console.log("User data fetched:", user); // Log the fetched user data
          localStorage.setItem('user', JSON.stringify(user));
          console.log("User data saved to localStorage.");
        },
        (error) => {
          console.error('Error fetching user data:', error);
        }
      );
    }
  }
  

  // Funksioni për regjistrim
  register(userData: { fullName: string; email: string; password: string; phoneNumber: string; role:string }): Observable<any> {
    return this.http.post(`${this.apiUrl}/User`, userData);
  }

  // Funksioni për logout
  logout(): void {
    localStorage.clear();
  }

  // Kontrollon nëse përdoruesi është i autentikuar
  isLoggedIn(): boolean {
    return !!localStorage.getItem('authToken'); // Kthen true nëse token ekziston
  }

  // Merr token-in e ruajtur nga localStorage
  getToken(): string | null {
    return localStorage.getItem('authToken');
  }

  // Ruaj token-in në localStorage
  setToken(token: string): void {
    localStorage.setItem('authToken', token);
  }

  getRole(): string | null {
    const user = JSON.parse(localStorage.getItem('user') || '{}');
    return user?.profile || null;
  }
}
