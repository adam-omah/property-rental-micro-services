import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {catchError, Observable, throwError} from 'rxjs';
import {Property} from "../Models/Property.model";


@Injectable({ providedIn: 'root' }) // Make the service available globally
export class PropertiesService {
  private apiUrl = 'http://localhost:8084/properties'; // Update with your Spring Boot backend URL

  constructor(private http: HttpClient) {}

  getProperties(): Observable<Property[]> {
    return this.http.get<Property[]>(this.apiUrl);
  }

  getProperty(id: number): Observable<Property> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<Property>(url);
  }

  updateProperty(property: Property | null): Observable<Property> {
    if (property === null || property.propertiesId === undefined) {
      return throwError(() => new Error('Cannot update property: Invalid property data'));
    }

    const url = `${this.apiUrl}/${property.propertiesId}`;
    return this.http.put<Property>(url, property).pipe(
      catchError(this.handleError) // Add error handling
    );
  }

  deleteProperty(id: number): Observable<string> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete(url, { responseType: 'text' });
  }

  createProperty(property: Property): Observable<Property> {
    return this.http.post<Property>(this.apiUrl, property);
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

  getAvailableProperties(): Observable<Property[]> {
    return this.http.get<Property[]>(`${this.apiUrl}?status=AVAILABLE`);
  }
}


