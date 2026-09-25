import { provideZoneChangeDetection } from '@angular/core';
import { platformBrowser } from '@angular/platform-browser';

import { AppModule } from './app/app.module';

// Angular is zoneless by default; NgModule apps must opt into zone.js change detection here,
// providing it in AppModule.providers has no effect.
platformBrowser().bootstrapModule(AppModule, { applicationProviders: [provideZoneChangeDetection()] })
  .catch(err => console.error(err));
