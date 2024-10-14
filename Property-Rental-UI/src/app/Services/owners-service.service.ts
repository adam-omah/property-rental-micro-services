import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import {Owner} from "../Models/Owner.model";

@Injectable({ providedIn: 'root' })
export class OwnerService {
  private apiUrl = 'http://localhost:8084/owners'; // Update with your Owner API endpoint

  constructor(private http: HttpClient) {}

  getOwners(): Observable<Owner[]> {
    return this.http.get<Owner[]>(this.apiUrl);
  }

  getOwner(id: number): Observable<Owner> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<Owner>(url);
  }

  createOwner(owner: Owner): Observable<Owner> {
    return this.http.post<Owner>(this.apiUrl, owner);
  }

  updateOwner(owner: Owner | null): Observable<Owner> {
    if (owner === null || owner.ownerId === undefined) {
      return throwError(() => new Error('Cannot update owner: Invalid owner data'));
    }
    const url = `${this.apiUrl}/${owner.ownerId}`;
    return this.http.put<Owner>(url, owner).pipe(
      catchError(this.handleError)
    );
  }

  deleteOwner(id: number): Observable<string> {
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
