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
  imports: [CommonModule],
  templateUrl: './structure-card.component.html',
  styleUrl: './structure-card.component.css'
})

export class StructureCardComponent implements OnChanges {
  @Input() structure!: DataStructure;
  
  // Track the index of the selected code block
  selectedCodeBlockIndex: number = 0;

  // Reset the selected code block when input changes
  ngOnChanges(): void {
    this.selectedCodeBlockIndex = 0;
  }

  // Change the selected code block by index
  selectLanguage(index: number): void {
    this.selectedCodeBlockIndex = index;
  }
}