import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {IWeatherArchiveDto} from "../../types/interfaces/IWeatherArchiveDto";
import {IWeatherArchiveRecordEntity} from "../../types/interfaces/IWeatherArchiveRecordEntity";
import {IResult} from "../../types/interfaces/IResult";
import {UploadWeatherArchiveRequest} from "../../types/requests/UploadWeatherArchiveRequest";

@Injectable({
  providedIn: 'root'
})
export class WeatherArchiveApiService {
  private readonly baseUrl: string = "https://localhost:7242/";

  constructor(
    private _httpClient: HttpClient
  ) {
  }

  // GET /WeatherArchive
  public getWeatherArchiveList(offset: number, limit: number) {
    return this._httpClient.get<IResult<IWeatherArchiveDto[]>>(
      this.baseUrl + `WeatherArchive?offset=${offset}&limit=${limit}`
    );
  }

  // GET /WeatherArchive/records/{weatherArchiveId}
  public getWeatherArchiveRecordList(weatherArchiveId: string, offset: number, limit: number) {
    return this._httpClient.get<IResult<IWeatherArchiveRecordEntity[]>>(
      this.baseUrl + `WeatherArchive/records/${weatherArchiveId}?offset=${offset}&limit=${limit}`
    );
  }

  // POST /WeatherArchive
  public postWeatherArchives(request: UploadWeatherArchiveRequest) {
    const formData = new FormData();
    request.files.forEach(x => formData.append("files", x))

    return this._httpClient.post<IResult<IWeatherArchiveDto[]>>(
      this.baseUrl + `WeatherArchive`, formData
    );
  }
}