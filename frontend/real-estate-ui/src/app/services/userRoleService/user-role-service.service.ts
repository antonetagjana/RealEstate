import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { jwtDecode } from 'jwt-decode'; 


@Injectable({
  providedIn: 'root'
})
export class UserRoleService {
  private apiUrl = 'https://localhost:5000/api/userrole'; // URL për UserRoleController

  constructor(private http: HttpClient) {}

  setToken(token: string): void {
    localStorage.setItem('authToken', token);
  }

  // Retrieve token from localStorage
  getToken(): string | null {
    return localStorage.getItem('authToken');
  }

  // Decode and get user info from token
  getUserFromToken(): any {
    const token = this.getToken();
    if (token) {
      try {
        return jwtDecode(token);
      } catch (error) {
        console.error('Error decoding token:', error);
        return null;
      }
    }
    return null;
  }

  logout(): void {
    localStorage.removeItem('authToken');
  }

  // Merr një user-role specifik sipas userId dhe roleId
  getUserRoleById(userId: string, roleId: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${userId}/${roleId}`);
  }

  // Merr të gjitha user-role lidhjet
  getAllUserRoles(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}`);
  }

  // Krijo një lidhje user-role
  createUserRole(userRole: any): Observable<any> {
    return this.http.post(`${this.apiUrl}`, userRole);
  }

  // Fshi një lidhje user-role sipas userId dhe roleId
  deleteUserRole(userId: string, roleId: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${userId}/${roleId}`);
  }
}
