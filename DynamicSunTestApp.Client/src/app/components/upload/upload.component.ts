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

  constructor(
    private weatherArchiveApiService: WeatherArchiveApiService
  ) {
  }

  //events
  public async onFilesDroppedHandler(files: File[]) {
    const request = new UploadWeatherArchiveRequest(files);
    const postWeatherArchives$ = this.weatherArchiveApiService.postWeatherArchives(request);
    await firstValueFrom(postWeatherArchives$);
  }
}
