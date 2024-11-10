import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class FavoriteService {
  private apiUrl = 'https://your-api-url.com/api/favorites'; // Replace with your actual API URL

  constructor(private http: HttpClient) {}

  getFavorites(): Observable<any> {
    return this.http.get(`${this.apiUrl}`);
  }

  addFavorite(propertyId: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/add`, { propertyId });
  }

  removeFavorite(propertyId: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/remove/${propertyId}`);
  }
}
