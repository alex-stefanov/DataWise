import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PredictionService {
  private apiUrl = 'http://127.0.0.1:5000/predict';

  constructor(private http: HttpClient) {}

  predictExercise(exercise: string): Observable<any> {
    return this.http.post<any>(this.apiUrl, { exercise });
  }
}
