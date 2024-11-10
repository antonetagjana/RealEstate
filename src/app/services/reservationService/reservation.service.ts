import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ReservationService {
  private apiUrl = 'https://localhost:5000/api';  // Zëvendësoje me URL-në reale të API-së tënde

  constructor(private http: HttpClient) {}

  // Merr rezervimet për një pronë specifike
  getReservationsByPropertyId(propertyId: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/property/${propertyId}`);
  }

  // Krijon një rezervim të ri
  createReservation(reservation: any): Observable<any> {
    return this.http.post(`${this.apiUrl}`, reservation);
  }

  // Përditëson një rezervim ekzistues
  updateReservation(reservationId: string, reservation: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${reservationId}`, reservation);
  }

  // Fshin një rezervim
  deleteReservation(reservationId: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${reservationId}`);
  }
  getReservationCount(): Observable<number> {
    return this.http.get<number>('/reservation/count');
}
  // Merr numrin e rezervimeve të konfirmuara për seller-in e loguar
  getConfirmedReservationsCount(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/count-confirmed`);
  }

}
