import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  private serverUrl: string = "";

  getServerUrl() {
    return this.serverUrl;
  }

  setServerUrl(value: string) {
    this.serverUrl = value;
  }
}
