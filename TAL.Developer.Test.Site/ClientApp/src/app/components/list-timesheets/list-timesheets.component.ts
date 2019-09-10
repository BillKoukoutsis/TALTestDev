import { Component } from '@angular/core';

import { Timesheet } from '../../models/timesheet.model';

import { TimesheetsService } from '../../services/timesheets.service'
import { TimezonesService } from '../../services/timezones.service'

@Component({
  selector: 'app-list-timesheets',
  templateUrl: './list-timesheets.component.html'
})
export class ListTimesheetsComponent {
  public timesheets: Timesheet[];
  public timesheetsService: TimesheetsService;
  public timezonesService: TimezonesService;

  constructor(timesheetsService: TimesheetsService, timezonesService: TimezonesService) {

    this.timesheetsService = timesheetsService;
    this.timezonesService = timezonesService;

    var self = this;

    this.timesheetsService.getList().subscribe(
      result => {

          self.timesheets = result;

          for (let i = 0; i < self.timesheets.length; i++) {

            self.timesheets[i].formattedStartDate = self.timezonesService.formatDatetime(self.timesheets[i].startDate, self.timesheets[i].timezoneName);
            self.timesheets[i].formattedEndDate = self.timezonesService.formatDatetime(self.timesheets[i].endDate, self.timesheets[i].timezoneName);
          }


      }, error => {
        alert(error.error.exceptionMessage)
      });
  }
}
