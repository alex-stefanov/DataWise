import { Routes } from '@angular/router';
import { KnowledgeNexusComponent } from './knowledge-nexus/knowledge-nexus.component';
import { VisionaryVaultComponent } from './visionary-vault/visionary-vault.component';
import { SolutionBeaconComponent } from './solution-beacon/solution-beacon.component';
import { SettingsComponent } from './settings/settings.component';
import { StructureCardComponent } from './structure-card/structure-card.component';

export const routes: Routes = [
  { path: '', redirectTo: 'knowledge-nexus', pathMatch: 'full' },
  { path: 'knowledge-nexus', component: KnowledgeNexusComponent },
  { path: 'visionary-vault', component: VisionaryVaultComponent },
  { path: 'solution-beacon', component: SolutionBeaconComponent },
  { path: 'settings', component: SettingsComponent },
  { path: 'structure-card', component: StructureCardComponent }
];
