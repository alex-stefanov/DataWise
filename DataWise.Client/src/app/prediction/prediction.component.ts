import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { PredictionService } from '../prediction.service';

@Component({
  selector: 'app-prediction',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatInputModule,
    MatButtonModule
  ],
  template: `
    <div class="container">
      <mat-card>
        <mat-card-title>Exercise Prediction</mat-card-title>
        <mat-card-content>
          <div class="input-container">
            <mat-form-field appearance="outline" class="full-width">
              <mat-label>Exercise Text</mat-label>
              <textarea matInput class="centered-input" [(ngModel)]="exerciseText"></textarea>
            </mat-form-field>
          </div>
          <button mat-raised-button color="primary" (click)="onPredict()" class="predict-button">
            Predict
          </button>
        </mat-card-content>
        <mat-card-content *ngIf="predictionResult" class="result-container">
          <h3>Result:</h3>
          <p>{{ predictionResult.prediction }}</p>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styleUrls: ['./prediction.component.css']
})
export class PredictionComponent {
  exerciseText: string = '';
  predictionResult: any = null;

  constructor(private predictionService: PredictionService) {}

  onPredict(): void {
    if (this.exerciseText.trim()) {
      this.predictionService.predictExercise(this.exerciseText).subscribe({
        next: (result) => {
          this.predictionResult = result;
        },
        error: (error) => {
          console.error('Prediction error:', error);
          this.predictionResult = { prediction: 'Prediction failed. Please try again.' };
        }
      });
    }
  }
}
