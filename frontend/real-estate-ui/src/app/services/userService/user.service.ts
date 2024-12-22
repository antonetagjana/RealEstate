import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'http://localhost:5000/api/User'; // Replace with your actual API URL

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*'
    }),
    withCredentials: false  // Set this to false since we're allowing any origin
  };
  
  //e ben te disponueshem per te gjitha metodat e klases userservice dhe 
//mundeson qe te behen kerkesa http ne backend
  constructor(private http: HttpClient) {}

  // Get user profile
  getUserProfile(): Observable<any> {
    return this.http.get(`${this.apiUrl}`);
  }

  getUserCount(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/count`);
}


  // Update user profile
  updateUserProfile(userData: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/profile`, userData);
  }

  // Change password
  changePassword(passwordData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/change-password`, passwordData);
  }
  getAllUsers(): Observable<any> {
    return this.http.get(`${this.apiUrl}`);
  }

  deleteUser(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
  
    // Get user by id
    getUserById(id: string): Observable<any> {
      return this.http.get<any>(`${this.apiUrl}/${id}`);
    }

}
