import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders} from '@angular/common/http';
import { map, Observable } from 'rxjs';

interface Reservation {
  id: string;
  checkIn: Date;
  checkOut: Date;
  status: string;
  property: {
    name: string;
  };
  user: {
    firstName: string;
    lastName: string;
  };
}

@Injectable({
  providedIn: 'root',
})
export class ReservationService {
  private apiUrl = 'http://localhost:5000/api/reservation';  // Zëvendësoje me URL-në reale të API-së tënde

  constructor(private http: HttpClient) {}

  // Merr rezervimet për një pronë specifike
  getReservationsByPropertyId(propertyId: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/property/${propertyId}`);
  }

  getAllReservations(): Observable<Reservation[]> {
    return this.http.get<any>(this.apiUrl).pipe(
      map(response => {
        console.log(response);  // Log the entire response
        // Check if the $values property exists and is an array
        return response && Array.isArray(response.$values) ? response.$values : [];
      })
    );
  }
  

  // Krijon një rezervim të ri
  createReservation(userId: string, propertyId: string, reservation: any): Observable<any> {
  const headers = new HttpHeaders().set('Content-Type', 'application/json');
  const newreservation = {
    checkIn: reservation.checkIn,
    checkOut: reservation.checkOut
  };
    return this.http.post<any>(`${this.apiUrl}/${userId}/${propertyId}`, reservation, { headers });
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
    return this.http.get<number>(`${this.apiUrl}/count`);
}


  // Merr numrin e rezervimeve të konfirmuara për seller-in e loguar
  getConfirmedReservationsCount(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/count`);
  }

}
