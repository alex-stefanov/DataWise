import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { trigger, transition, style, animate } from '@angular/animations';

@Component({
  selector: 'app-solution-beacon',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule],
  templateUrl: './solution-beacon.component.html',
  styleUrls: ['./solution-beacon.component.css'],
  animations: [
    trigger('fadeIn', [
      transition('* => *', [
        style({ opacity: 0 }),
        animate('500ms ease-in', style({ opacity: 1 }))
      ])
    ])
  ]
})
export class SolutionBeaconComponent {
  inputString: string = '';
  prediction: string = '';
  animationKey: number = 0;

  constructor(private http: HttpClient) {}

  submitExercise() {
    const url = 'https://api.datawise.notfranko.com/predict';
    const body = { exercise: this.inputString };

    this.http.post(url, body).subscribe({
      next: (res: any) => {
        this.prediction = res.prediction;
        this.animationKey++;
      },
      error: (err) => {
        console.error('Error making POST request:', err);
      }
    });
  }
}
