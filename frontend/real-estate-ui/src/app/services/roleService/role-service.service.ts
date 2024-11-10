import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RolesService {
  private apiUrl = 'https://localhost:5000/api/roles'; // Ndrysho URL-në sipas konfigurimit të backend-it

  constructor(private http: HttpClient) {}

  // Merr një rol sipas ID-së
  getRoleById(id: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  // Merr të gjitha rolet
  getAllRoles(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}`);
  }

  // Krijo një rol të ri
  createRole(role: any): Observable<any> {
    return this.http.post(`${this.apiUrl}`, role);
  }

  // Përditëso një rol ekzistues
  updateRole(id: string, role: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, role);
  }

  // Fshi një rol sipas ID-së
  deleteRole(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
