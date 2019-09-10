import { Component } from '@angular/core';

import { TimesheetsReport } from '../../models/timesheetsreport.model';

import { TimesheetsService } from '../../services/timesheets.service';
import { TimezonesService } from '../../services/timezones.service';
import { SecurityService } from '../../services/security.service';

@Component({
  selector: 'app-timesheets-report',
  templateUrl: './timesheets-report.component.html'
})
export class TimesheetsReportComponent {
  public timesheetsReports: TimesheetsReport[];
  public timesheetsService: TimesheetsService;
  public timezonesService: TimezonesService;
  public securityService: SecurityService;
  public totalHoursWorked: number = 0.00;
  public totalCost: number = 0.00;
  public reportDate: Date = new Date();

  constructor(timesheetsService: TimesheetsService, timezonesService: TimezonesService, securityService: SecurityService) {
    
    this.timesheetsService = timesheetsService;
    this.timezonesService = timezonesService;
    this.securityService = securityService;

    this.reportByDate();
  }

  reportByDate() {

    this.totalHoursWorked = 0.00;
    this.totalCost = 0.00;

    var self = this;

    this.timesheetsService.reportByDate(this.reportDate).subscribe(
      result => {

        if (self.securityService.group.name == 'Admins') {

          self.timesheetsReports = result;
          for (let i = 0; i < self.timesheetsReports.length; i++) {

            self.timesheetsReports[i].formattedStartDate = self.timezonesService.formatDatetime(self.timesheetsReports[i].startDate, self.timesheetsReports[i].timezoneName);
            self.timesheetsReports[i].formattedEndDate = self.timezonesService.formatDatetime(self.timesheetsReports[i].endDate, self.timesheetsReports[i].timezoneName);

            self.totalHoursWorked += (self.timesheetsReports[i].hoursWorked != null ? self.timesheetsReports[i].hoursWorked : 0.00);

            self.totalCost += (self.timesheetsReports[i].cost != null ? self.timesheetsReports[i].cost : 0.00);
          }

        } else {

          self.timesheetsReports = [];

          for (let i = 0; i < result.length; i++) {

            if (result[i].employeeId == self.securityService.employee.id) {

              result[i].formattedStartDate = self.timezonesService.formatDatetime(result[i].startDate, result[i].timezoneName);
              result[i].formattedEndDate = self.timezonesService.formatDatetime(result[i].endDate, result[i].timezoneName);

              self.totalHoursWorked += (result[i].hoursWorked != null ? result[i].hoursWorked : 0.00);

              self.totalCost += (result[i].cost != null ? result[i].cost : 0.00);
              self.timesheetsReports.push(result[i])
            }
          }

        }


      }, error => {
        alert(error.error.exceptionMessage)
      });
  }

}
