import { Injectable } from '@angular/core';
import { HttpClient,HttpParams,HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PropertyService {
  private apiUrl = 'http://localhost:5000/api'; // Replace with your actual backend API URL

  constructor(private http: HttpClient) {}

  // Method to get all properties
  getAllProperties(): Observable<any[]> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    return this.http.get<any[]>(`${this.apiUrl}/Property`,{headers});
    
  }

  getAllProperties2(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/Property`);
    
  }


  createProperty(propertyData: FormData): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    return this.http.post<any>(`${this.apiUrl}/Property`, propertyData, { headers });
  }

  // Method to get a property by its ID
  getPropertyById(id: string){
    return this.http.get<any>(`http://localhost:5000/api/property/${id}`);
  }
  
  getPropertyCount(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/property/count`);
}

getPropertiesByLocation(location: string): Observable<any> {
  return this.http.get(`${this.apiUrl}/property/search?location=${location}`);
}

getFilteredProperties(
  minPrice?: number | null,
  maxPrice?: number | null,
  category?: string,
  location?: string,
  floors?: number | null
): Observable<any[]> {
  let params = new HttpParams();

  if (minPrice) params = params.set('minPrice', minPrice.toString());
  if (maxPrice) params = params.set('maxPrice', maxPrice.toString());
  if (category) params = params.set('category', category);
  if (location) params = params.set('location', location);
  if (floors) params = params.set('floors', floors.toString());

  return this.http.get<any[]>(`${this.apiUrl}/property/filter`, { params });
}

  // Method to update an existing property
  updateProperty(id: string, propertyData: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, propertyData);
  }


  // Method to delete a property
  deleteProperty(id: string): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
    // Merr numrin e pronave të listuara për seller-in e loguar
    getListedPropertiesCount(): Observable<number> {
      return this.http.get<number>(`${this.apiUrl}/property/count`);
    }

  
}
