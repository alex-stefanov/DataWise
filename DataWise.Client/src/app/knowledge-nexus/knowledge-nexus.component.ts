import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DataStructure } from '../structure-card/structure-card.component';

@Component({
  selector: 'app-knowledge-nexus',
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
}