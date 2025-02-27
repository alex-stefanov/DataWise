import { Component, Input } from '@angular/core';

export interface DataStructure {
  _id: string;
  Name: string;
  Description: string;
  ImageUrl: string;
  Example: string;
  // Include other fields if needed
}

@Component({
  selector: 'app-structure-card',
  templateUrl: './structure-card.component.html',
  styleUrls: ['./structure-card.component.css']
})
export class StructureCardComponent {
  @Input() structure!: DataStructure;
}