import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { StructureCardComponent, DataStructure } from '../structure-card/structure-card.component';
import { AlgorithmCardComponent, Algorithm } from '../algorithm-card/algorithm-card.component';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-knowledge-nexus',
  standalone: true,
  imports: [CommonModule, HttpClientModule, StructureCardComponent, AlgorithmCardComponent],
  templateUrl: './knowledge-nexus.component.html',
  styleUrls: ['./knowledge-nexus.component.css']
})
export class KnowledgeNexusComponent {
  selectedStructure?: DataStructure;
  selectedAlgorithm?: Algorithm;

  constructor(private http: HttpClient) {}

  fetchStructure(name: string): void {
    const url = `${environment.apiUrl}/api/structure/byname/${name}`;
    this.http.get<DataStructure>(url).subscribe({
      next: (data) => {
        this.selectedStructure = data;
        console.log(`Received data for ${name}:`, data);
      },
      error: (err) => {
        console.error(`Error fetching ${name}:`, err);
      }
    });
  }

  reset(): void {
    this.selectedStructure = undefined;
  }

  fetchAlgorithm(name: string): void {
    const url = `${environment.apiUrl}/api/algorithms/byname/${name}`;
    this.http.get<Algorithm>(url).subscribe({
      next: (data) => {
        this.selectedAlgorithm = data;
        console.log(`Received data for algorithm ${name}:`, data);
      },
      error: (err) => {
        console.error(`Error fetching algorithm ${name}:`, err);
      }
    });
  }

  resetAlgorithm(): void {
    this.selectedAlgorithm = undefined;
  }
}