import { Injectable } from '@angular/core';
import {IWeatherArchiveDto} from "../types/interfaces/IWeatherArchiveDto";
import {BehaviorSubject, firstValueFrom} from "rxjs";
import {WeatherArchiveApiService} from "../services/api/weather-archive-api.service";

@Injectable({
  providedIn: 'root'
})
export class WeatherArchiveStateService {
  public weatherArchives$: BehaviorSubject<IWeatherArchiveDto[]> = new BehaviorSubject<IWeatherArchiveDto[]>([]);

  constructor(
    private weatherArchiveApiService: WeatherArchiveApiService
  ) { }

  // common
  public clearWeatherArchives() {
    this.weatherArchives$.next([]);
  }

  // requests
  public async getWeatherArchives(offset: number, limit: number) {
    const getWeatherArchiveList$ = this.weatherArchiveApiService.getWeatherArchiveList(offset, limit);
    const getWeatherArchiveListResult = await firstValueFrom(getWeatherArchiveList$);

    const archives = this.weatherArchives$.getValue();
    archives.push(...getWeatherArchiveListResult.response);

    this.weatherArchives$.next(archives);

    return getWeatherArchiveListResult;
  }
}
