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
  dropdownVisible = false;
  selectedSubtypeIndices: number[] = [];

  ngOnChanges(): void {
    this.selectedCodeBlockIndex = 0;
    if (this.structure && this.structure.subTypes && this.structure.subTypes.length) {
      this.selectedSubtypeIndices = this.structure.subTypes.map(() => 0);
    }
  }

  selectLanguage(index: number): void {
    this.selectedCodeBlockIndex = index;
  }

  toggleDropdown(): void {
    this.dropdownVisible = !this.dropdownVisible;
  }

  selectSubtypeLanguage(subtypeIndex: number, languageIndex: number): void {
    this.selectedSubtypeIndices[subtypeIndex] = languageIndex;
  }

  getComplexityNotation(complexity: Complexity): string {
    switch (complexity) {
      case Complexity.Constant:
        return 'O(1)';
      case Complexity.Logarithmic:
        return 'O(log n)';
      case Complexity.Linear:
        return 'O(n)';
      case Complexity.Linearithmic:
        return 'O(n log n)';
      case Complexity.Quadratic:
        return 'O(n²)';
      case Complexity.Cubic:
        return 'O(n³)';
      case Complexity.Exponential:
        return 'O(2^n)';
      case Complexity.Factorial:
        return 'O(n!)';
      case Complexity.Unknown:
        return 'O(?)';
      default:
        return '';
    }
  }
}