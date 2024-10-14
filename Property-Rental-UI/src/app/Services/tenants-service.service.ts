import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import {Tenant} from "../Models/Tenant.model";

@Injectable({ providedIn: 'root' })
export class TenantService {
  private apiUrl = 'http://localhost:8085/tenants'; // Update with your Tenant API endpoint

  constructor(private http: HttpClient) {}

  getTenants(): Observable<Tenant[]> {
    return this.http.get<Tenant[]>(this.apiUrl);
  }

  getTenant(id: number): Observable<Tenant> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<Tenant>(url);
  }

  createTenant(tenant: Tenant): Observable<Tenant> {
    return this.http.post<Tenant>(this.apiUrl, tenant);
  }

  updateTenant(tenant: Tenant | null): Observable<Tenant> {
    if (tenant === null || tenant.tenantId === undefined) {
      return throwError(() => new Error('Cannot update tenant: Invalid tenant data'));
    }
    const url = `${this.apiUrl}/${tenant.tenantId}`;
    return this.http.put<Tenant>(url, tenant).pipe(
      catchError(this.handleError)
    );
  }

  deleteTenant(id: number): Observable<string> {
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
