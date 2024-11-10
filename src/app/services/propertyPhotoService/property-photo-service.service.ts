import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PropertyPhotohotoService {
  private apiUrl = 'https://localhost:5000/propertyphotos'; // URL e API për fotot

  constructor(private http: HttpClient) {}

  // Metoda për të ngarkuar një foto
  uploadPhoto(propertyId: string, photo: File): Observable<any> {
    const formData = new FormData();
    formData.append('photo', photo);
    return this.http.post(`${this.apiUrl}/upload/${propertyId}`, formData);
  }

  // Metoda për të marrë të gjitha fotot për një pronë
  getPhotos(propertyId: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/property/${propertyId}`);
  }

  // Metoda për të fshirë një foto
  deletePhoto(photoId: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${photoId}`);
  }
}
