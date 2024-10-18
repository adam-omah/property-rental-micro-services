import { formatDate } from '@angular/common';
import {catchError, map, Observable, throwError} from "rxjs";
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

private parseDate(dateString: string): Date
{
  const parts = dateString.split('/').map(part => parseInt(part, 10));
  return new Date(parts[0], parts[1] - 1, parts[2]);
}

getRentals(): Observable<Rental[]> {
  return this.http.get<Rental[]>(this.apiUrl).pipe(
    map(rentals => rentals.map(rental => ({
      ...rental,
      startDate: this.parseDate(rental.startDate as unknown as string),
      endDate: this.parseDate(rental.endDate as unknown as string)
    }))),
    catchError(this.handleError)
  );
}

getRental(id: number): Observable<Rental> {
  const url = `${this.apiUrl}/${id}`;
  return this.http.get<Rental>(url).pipe(
    map(rental => ({
      ...rental,
      startDate: this.parseDate(rental.startDate as unknown as string),
      endDate: this.parseDate(rental.endDate as unknown as string)
    })),
    catchError(this.handleError)
  );
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
