import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { StructureCardComponent } from '../structure-card/structure-card.component';
import { DataStructure } from '../structure-card/structure-card.component';

@Component({
  selector: 'app-knowledge-nexus',
  standalone: true,
  imports: [CommonModule, HttpClientModule, StructureCardComponent],
  templateUrl: './knowledge-nexus.component.html',
  styleUrls: ['./knowledge-nexus.component.css']
})
export class KnowledgeNexusComponent {
  selectedStructure?: DataStructure;

  constructor(private http: HttpClient) {}

  fetchStructure(name: string): void {
    const url = `https://localhost:7085/api/structure/byname/${name}`;
    
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
}