import { Injectable } from '@angular/core';
import {BehaviorSubject, firstValueFrom} from "rxjs";
import {IWeatherArchiveRecordEntity} from "../types/interfaces/IWeatherArchiveRecordEntity";
import {WeatherArchiveApiService} from "../services/api/weather-archive-api.service";
import {IWeatherArchiveDto} from "../types/interfaces/IWeatherArchiveDto";

@Injectable({
  providedIn: 'root'
})
export class WeatherArchiveRecordsStateService {
  public weatherArchiveRecords$: BehaviorSubject<IWeatherArchiveRecordEntity[]> = new BehaviorSubject<IWeatherArchiveRecordEntity[]>([]);
  public currentWeatherArchive$: BehaviorSubject<IWeatherArchiveDto | null> = new BehaviorSubject<IWeatherArchiveDto | null>(null);

  constructor(
    private weatherArchiveApiService: WeatherArchiveApiService
  ) { }

  // common
  public setWeatherArchive(archive: IWeatherArchiveDto) {
    this.currentWeatherArchive$.next(archive);
  }

  public clearWeatherArchiveRecords() {
    this.weatherArchiveRecords$.next([]);
  }

  // requests
  public async getWeatherArchiveRecords(weatherArchiveId: string, offset: number, limit: number) {
    const getWeatherArchiveRecordList$ = this.weatherArchiveApiService.getWeatherArchiveRecordList(weatherArchiveId, offset, limit);
    const getWeatherArchiveRecordListResult = await firstValueFrom(getWeatherArchiveRecordList$);

    const records = this.weatherArchiveRecords$.getValue();
    records.push(...getWeatherArchiveRecordListResult.response);

    this.weatherArchiveRecords$.next(records);

    return getWeatherArchiveRecordListResult;
  }
}
