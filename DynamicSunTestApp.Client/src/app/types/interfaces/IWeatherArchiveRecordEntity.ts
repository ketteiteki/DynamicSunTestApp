
export interface IWeatherArchiveRecordEntity {
  id: string;
  date: string;
  time: string;
  temperature: number;
  humidity: number;
  dewPoint: number;
  atmosphericPressure: number;
  windDirection: string | null;
  windSpeed: number | null;
  cloudiness: number | null;
  lowerBoundaryOfCloudiness: number | null;
  horizontalVisibility: number | null;
  weatherEvents: string | null;
  weatherArchiveId: string;
}