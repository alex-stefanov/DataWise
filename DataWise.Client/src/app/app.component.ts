import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { PredictionComponent } from './prediction/prediction.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, PredictionComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'DataWise.Client';
}