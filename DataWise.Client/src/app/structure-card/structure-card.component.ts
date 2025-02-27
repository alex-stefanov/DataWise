import { Component, Input } from '@angular/core';

export interface DataStructure {
  _id: string;
  Name: string;
  Description: string;
  ImageUrl: string;
  Example: string;
}

@Component({
  selector: 'app-structure-card',
  standalone: true,
  imports: [],
  templateUrl: './structure-card.component.html',
  styleUrl: './structure-card.component.css'
})

export class StructureCardComponent {
  @Input() structure!: DataStructure;
}