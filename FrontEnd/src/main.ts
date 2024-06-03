import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import {provideAnimations} from "@angular/platform-browser/animations";
import {provideHttpClient} from "@angular/common/http";
import {provideRouter, withDebugTracing} from "@angular/router";
import {routes} from "./app/app.routes";

bootstrapApplication(AppComponent, {
  providers: [
    provideAnimations(),
    provideRouter(routes), // Ensure routes are provided
    provideHttpClient(), // Ensure HttpClientModule is provided
    // Any other providers
  ]
}).catch(err => console.error(err));
