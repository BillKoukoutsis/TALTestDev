import { Component } from '@angular/core';

import { Timesheet } from '../../models/timesheet.model';

import { TimesheetsService } from '../../services/timesheets.service'
import { TimezonesService } from '../../services/timezones.service'
import { SecurityService } from '../../services/security.service'

@Component({
  selector: 'app-list-timesheets',
  templateUrl: './list-timesheets.component.html'
})
export class ListTimesheetsComponent {
  public timesheets: Timesheet[];
  public timesheetsService: TimesheetsService;
  public timezonesService: TimezonesService;
  public securityService: SecurityService;

  constructor(timesheetsService: TimesheetsService, timezonesService: TimezonesService, securityService: SecurityService) {

    this.timesheetsService = timesheetsService;
    this.timezonesService = timezonesService;
    this.securityService = securityService;

    var self = this;

    this.timesheetsService.getList().subscribe(
      result => {

        if (self.securityService.group.name == 'Admins') {
          self.timesheets = result;

          for (let i = 0; i < self.timesheets.length; i++) {

            self.timesheets[i].formattedStartDate = self.timezonesService.formatDatetime(self.timesheets[i].startDate, self.timesheets[i].timezoneName);
            self.timesheets[i].formattedEndDate = self.timezonesService.formatDatetime(self.timesheets[i].endDate, self.timesheets[i].timezoneName);
          }
        } else {

          result;
          self.timesheets = [];

          for (let i = 0; i < result.length; i++) {

            if (result[i].employeeId == self.securityService.employee.id) {
              result[i].formattedStartDate = self.timezonesService.formatDatetime(result[i].startDate, result[i].timezoneName);
              result[i].formattedEndDate = self.timezonesService.formatDatetime(result[i].endDate, result[i].timezoneName);
              self.timesheets.push(result[i]);
            }

          }

        }


      }, error => {
        alert(error.error.exceptionMessage)
      });
  }
}
