import {Component, OnDestroy, OnInit} from '@angular/core';
import {WeatherArchiveRecordsStateService} from "../../state/weather-archive-records-state.service";
import {Subscription} from "rxjs";
import {ActivatedRoute, RouterLink} from "@angular/router";
import {SvgIconComponent} from "angular-svg-icon";
import {AsyncPipe, NgIf} from "@angular/common";
import {IWeatherArchiveRecordEntity} from "../../types/interfaces/IWeatherArchiveRecordEntity";
import {WeatherArchiveStateService} from "../../state/weather-archive-state.service";

@Component({
  selector: 'app-archive-records',
  standalone: true,
  imports: [
    RouterLink,
    SvgIconComponent,
    AsyncPipe,
    NgIf
  ],
  templateUrl: './archive-records.component.html',
  styleUrl: './archive-records.component.scss'
})
export class ArchiveRecordsComponent implements OnInit, OnDestroy {
  // observable
  public weatherArchiveRecords$ = this.weatherArchiveRecordsState.weatherArchiveRecords$;
  public currentWeatherArchive$ = this.weatherArchiveRecordsState.currentWeatherArchive$;

  // subscription
  private routeSub: Subscription;

  // state
  private activeArchiveId: string | undefined;
  public currentPage: number = 1;
  public maxPage: number = 1;

  // const
  public readonly limit = 200

  constructor(
    private weatherArchivesState: WeatherArchiveStateService,
    private weatherArchiveRecordsState: WeatherArchiveRecordsStateService,
    private activatedRoute: ActivatedRoute
  ) {
    this.routeSub = this.activatedRoute.params.subscribe(params => {
      const id = params['id'];
      this.activeArchiveId = id;
    });
  }

  async ngOnInit() {
    if (this.weatherArchivesState.weatherArchives$.getValue().length === 0) {
      await this.weatherArchivesState.getWeatherArchives(0, 30);
    }

    if (this.weatherArchiveRecords$.getValue().length > 0) {
      this.weatherArchiveRecordsState.clearWeatherArchiveRecords();
    }

    if (!this.activeArchiveId) throw new Error("ActiveArchiveId is undefined");

    const archive = this.weatherArchivesState.weatherArchives$.getValue().find(x => x.id === this.activeArchiveId);

    if (!archive) throw new Error("Archive is not found");

    this.weatherArchiveRecordsState.setWeatherArchive(archive);
    const recordCount = this.currentWeatherArchive$.getValue()?.recordCount ?? 1;
    this.maxPage = recordCount === 1 ? 1 : Math.ceil(recordCount / this.limit);
    await this.weatherArchiveRecordsState.getWeatherArchiveRecords(this.activeArchiveId, 0, this.limit);
  }

  ngOnDestroy() {
    this.routeSub.unsubscribe();
  }

  //events
  public async onClickLeftAnglePaginatorHandler() {
    if (!this.activeArchiveId) return;
    if (this.currentPage === 1) return;

    this.currentPage--;

    const offset = this.limit * (this.currentPage - 1);
    this.weatherArchiveRecordsState.clearWeatherArchiveRecords();
    await this.weatherArchiveRecordsState.getWeatherArchiveRecords(this.activeArchiveId, offset, this.limit);
  }

  public async onClickRightAnglePaginatorHandler() {
    if (!this.activeArchiveId) return;
    if (this.currentPage >= this.maxPage) return;

    this.currentPage++;

    const offset = this.limit * (this.currentPage - 1);
    this.weatherArchiveRecordsState.clearWeatherArchiveRecords();
    await this.weatherArchiveRecordsState.getWeatherArchiveRecords(this.activeArchiveId, offset, this.limit);
  }

  //common
  public findLastIndexOfWeatherArchiveRecordByDate(array: IWeatherArchiveRecordEntity[], date: string): number {
    let lastIndex = -1;

    for (let i = array.length - 1; i >= 0; i--) {
      if (array[i].date === date) {
        lastIndex = i;
        break;
      }
    }

    return lastIndex;
  }

  public isThereDifferenceInMonths(date1: string, date2: string) {
    const firstDate = new Date(date1);
    const secondDate = new Date(date2);

    return firstDate.getMonth() !== secondDate.getMonth();
  }

  public getNameTimeOfYear(date: string) {
    const month = new Date(date).getMonth();

    switch (month) {
      case 0:
        return "January";
      case 1:
        return "February";
      case 2:
        return "March";
      case 3:
        return "April";
      case 4:
        return "May";
      case 5:
        return "June";
      case 6:
        return "July";
      case 7:
        return "August";
      case 8:
        return "September";
      case 9:
        return "October";
      case 10:
        return "November";
      case 11:
        return "December";
    }

    return "";
  }

  protected readonly onclick = onclick;
}
