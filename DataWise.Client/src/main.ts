import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import 'zone.js';

import { registerLicense } from '@syncfusion/ej2-base';

import { environment } from './environments/environment';

registerLicense(environment.syncfusionLicense);

bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));
