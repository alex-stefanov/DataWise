import { Routes } from '@angular/router';
import { KnowledgeNexusComponent } from './knowledge-nexus/knowledge-nexus.component';
import { VisionaryVaultComponent } from './visionary-vault/visionary-vault.component';
import { SolutionBeaconComponent } from './solution-beacon/solution-beacon.component';
import { ProfileComponent } from './profile/profile.component';
import { StructureCardComponent } from './structure-card/structure-card.component';
import { InterviewHelperComponent } from './interview-helper/interview-helper.component';

export const routes: Routes = [
  { path: '', redirectTo: 'knowledge-nexus', pathMatch: 'full' },
  { path: 'knowledge-nexus', component: KnowledgeNexusComponent },
  { path: 'visionary-vault', component: VisionaryVaultComponent },
  { path: 'solution-beacon', component: SolutionBeaconComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'structure-card', component: StructureCardComponent },
  { path: 'interview-helper', component: InterviewHelperComponent },
];