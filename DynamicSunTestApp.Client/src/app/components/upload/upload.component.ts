import { Component } from '@angular/core';
import {SvgIconComponent} from "angular-svg-icon";
import {DragDropDirective} from "../../directives/drag-drop.directive";
import {WeatherArchiveApiService} from "../../services/api/weather-archive-api.service";
import {UploadWeatherArchiveRequest} from "../../types/requests/UploadWeatherArchiveRequest";
import {firstValueFrom} from "rxjs";

@Component({
  selector: 'app-upload',
  standalone: true,
  imports: [
    SvgIconComponent,
    DragDropDirective
  ],
  templateUrl: './upload.component.html',
  styleUrl: './upload.component.scss'
})
export class UploadComponent {
  //state
  public showLoader: boolean = false;

  constructor(
    private weatherArchiveApiService: WeatherArchiveApiService
  ) {
  }

  //events
  public async onFilesDroppedHandler(files: File[]) {
    try {
      const request = new UploadWeatherArchiveRequest(files);
      const postWeatherArchives$ = this.weatherArchiveApiService.postWeatherArchives(request);
      this.showLoader = true;
      await firstValueFrom(postWeatherArchives$);
      this.showLoader = false;
    } catch (e: any) {
      this.showLoader = false;
      alert(e.error.message);
    }
  }
}
