import { formatDate } from '@angular/common';
import {catchError, Observable, throwError} from "rxjs";
import {Rental} from "../Models/Rental.model";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {Injectable} from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class RentalService {
  private apiUrl = 'http://localhost:8085/rentals'; // Update with your Rental API endpoint

  constructor(private http: HttpClient) {}

  formatDates(rental: Rental): any {
    return {
      ...rental,
      startDate: formatDate(rental.startDate, 'dd/MM/yyyy', 'en'),
      endDate: formatDate(rental.endDate, 'dd/MM/yyyy', 'en')
    };
  }

  getRentals(): Observable<Rental[]> {
    return this.http.get<Rental[]>(this.apiUrl);
  }

  getRental(id: number): Observable<Rental> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<Rental>(url);
  }

  createRental(rental: Rental): Observable<Rental> {
    const formattedRental = this.formatDates(rental);
    return this.http.post<Rental>(this.apiUrl, formattedRental);
  }

  updateRental(rental: Rental | null): Observable<Rental> {
    if (rental === null || rental.rentalsId === undefined) {
      return throwError(() => new Error('Cannot update rental: Invalid rental data'));
    }
    const url = `${this.apiUrl}/${rental.rentalsId}`;
    const formattedRental = this.formatDates(rental);
    return this.http.put<Rental>(url, formattedRental).pipe(
      catchError(this.handleError)
    );
  }

  deleteRental(id: number): Observable<string> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete(url, { responseType: 'text' });
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, body was: `, error.error);
    }
    // Return an observable with a user-facing error message.
    return throwError(() => new Error('Something bad happened; please try again later.'));
  }
}
