import {Component, OnInit} from '@angular/core';
import {SvgIconComponent} from "angular-svg-icon";
import {RouterLink} from "@angular/router";
import {WeatherArchiveStateService} from "../../state/weather-archive-state.service";
import {AsyncPipe, NgIf} from "@angular/common";
import {WeatherArchiveRecordsStateService} from "../../state/weather-archive-records-state.service";

@Component({
  selector: 'app-archives',
  standalone: true,
  imports: [
    SvgIconComponent,
    RouterLink,
    AsyncPipe,
    NgIf
  ],
  templateUrl: './archives.component.html',
  styleUrl: './archives.component.scss'
})
export class ArchivesComponent implements OnInit {
  public weatherArchives$ = this.weatherArchiveState.weatherArchives$;

  constructor(
    private weatherArchiveState: WeatherArchiveStateService,
    protected weatherArchiveRecordsState: WeatherArchiveRecordsStateService
  ) {}

  async ngOnInit() {
    if (this.weatherArchives$.getValue().length > 0) {
      this.weatherArchiveState.clearWeatherArchives();
    }
    await this.weatherArchiveState.getWeatherArchives(0, 30);
  }
}
