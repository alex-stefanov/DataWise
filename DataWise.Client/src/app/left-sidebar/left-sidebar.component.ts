import { CommonModule } from '@angular/common';
import { Component, input, output } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-left-sidebar',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './left-sidebar.component.html',
  styleUrl: './left-sidebar.component.css',
})

export class LeftSidebarComponent {
  isLeftSidebarCollapsed = input.required<boolean>();
  changeIsLeftSidebarCollapsed = output<boolean>();
  items = [
    {
      routeLink: 'knowledge-nexus',
      icon: 'fal fa-home',
      label: 'Knowledge Nexus',
    },
    {
      routeLink: 'visionary-vault',
      icon: 'fal fa-box-open',
      label: 'Visionary Vault',
    },
    {
      routeLink: 'solution-beacon',
      icon: 'fal fa-file',
      label: 'Solution Beacon',
    },
    {
      routeLink: 'profile',
      icon: 'fal fa-cog',
      label: 'Profile',
    },
    {
      routeLink: 'interview-helper',
      icon: 'fal fa-cog',
      label: 'Interview Helper',
    }
  ];

  toggleCollapse(): void {
    this.changeIsLeftSidebarCollapsed.emit(!this.isLeftSidebarCollapsed());
  }

  closeSidenav(): void {
    this.changeIsLeftSidebarCollapsed.emit(true);
  }
}
