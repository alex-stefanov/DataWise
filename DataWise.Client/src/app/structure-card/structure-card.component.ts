import { Component, Input } from '@angular/core';

export enum Complexity {
  Constant = 0,
  Logarithmic = 1,
  Linear = 2,
  Linearithmic = 3,
  Quadratic = 4,
  Cubic = 5,
  Exponential = 6,
  Factorial = 7,
  Unknown = 8
}

export interface CodeBlock {
  title: string;
  language: string;
  code: string;
}

export interface DataStructureSubType {
  title: string;
  description: string;
  differences: string;
  codeBlocks: CodeBlock[];
}

export interface DataStructure {
  id: string;
  name: string;
  description: string;
  imageUrl: string;
  example: string;
  accessTimeComplexity: Complexity;
  insertionTimeComplexity: Complexity;
  deletionTimeComplexity: Complexity;
  codeBlocks: CodeBlock[];
  subTypes: DataStructureSubType[];
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