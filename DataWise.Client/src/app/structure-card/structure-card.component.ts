import { Component, Input, OnChanges } from '@angular/core';
import { CommonModule } from '@angular/common';

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
  imports: [CommonModule],
  templateUrl: './structure-card.component.html',
  styleUrl: './structure-card.component.css'
})

export class StructureCardComponent implements OnChanges {
  @Input() structure!: DataStructure;
  
  selectedCodeBlockIndex: number = 0;

  ngOnChanges(): void {
    this.selectedCodeBlockIndex = 0;
  }

  selectLanguage(index: number): void {
    this.selectedCodeBlockIndex = index;
  }
}