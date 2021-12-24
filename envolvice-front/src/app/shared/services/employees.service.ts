import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Employee } from '../models/employee';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class EmployeesService {
  private readonly baseUrl;
  constructor(private http: HttpClient) {
    this.baseUrl = environment.remoteServiceBaseUrl;
  }

  getFilter(payload: FormData): Observable<Employee[]> {
    return this.http
      .post(`${this.baseUrl}/employee`, payload)
      .pipe(map((p) => p as Employee[]));
  }
}
