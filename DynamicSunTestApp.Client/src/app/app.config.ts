import {APP_INITIALIZER, ApplicationConfig, importProvidersFrom} from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import {provideAngularSvgIcon} from "angular-svg-icon";
import {HttpClient, provideHttpClient} from "@angular/common/http";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {IConfig} from "./types/config/IConfig";
import {ConfigService} from "./services/common/config.service";

function initializeAppFactory(httpClient: HttpClient, configService: ConfigService): () => Promise<any> {
  const configPath = 'assets/config/config.json';

  return () => {
    const result = fetch(configPath)
      .then(r => r.json())
      .then((j: IConfig) => {
        configService.setServerUrl(j.serverUrl)
      });

    return result;
  };
}

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideAngularSvgIcon(),
    provideHttpClient(),
    importProvidersFrom(BrowserAnimationsModule),
    {
      provide: APP_INITIALIZER,
      useFactory: initializeAppFactory,
      deps: [HttpClient, ConfigService],
      multi: true
    }
  ]
};
