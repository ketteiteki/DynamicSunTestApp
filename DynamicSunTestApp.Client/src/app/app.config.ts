import {ApplicationConfig, importProvidersFrom} from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import {provideAngularSvgIcon} from "angular-svg-icon";
import {provideHttpClient} from "@angular/common/http";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideAngularSvgIcon(),
    provideHttpClient(),
    importProvidersFrom(BrowserAnimationsModule)]
};
