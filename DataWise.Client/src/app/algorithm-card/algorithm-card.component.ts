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
  GraphTraversal = 8,
  PseudoPolynomial = 9,
  Unknown = 10
}

export interface CodeBlock {
  language: string;
  code: string;
}

export interface AlgorithmCategory {
  name: string;
  description: string;
}

export interface Algorithm {
  id: string;
  name: string;
  description: string;
  useCases: string[];
  complexity: Complexity;
  codeBlocks: CodeBlock[];
  category: AlgorithmCategory;
}

@Component({
  selector: 'app-algorithm-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './algorithm-card.component.html',
  styleUrls: ['./algorithm-card.component.css']
})

export class AlgorithmCardComponent implements OnChanges {
  @Input() algorithm!: Algorithm;
  
  selectedCodeBlockIndex: number = 0;
  dropdownVisible = false;

  ngOnChanges(): void {
    this.selectedCodeBlockIndex = 0;
  }

  selectLanguage(index: number): void {
    this.selectedCodeBlockIndex = index;
  }

  toggleDropdown(): void {
    this.dropdownVisible = !this.dropdownVisible;
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
      case Complexity.GraphTraversal:
        return 'O(V+E)';
      case Complexity.PseudoPolynomial:
        return 'O(n * capacity)';
      case Complexity.Unknown:
        return 'O(?)';
      default:
        return '';
    }
  }
}