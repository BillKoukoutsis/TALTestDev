import { Component } from '@angular/core';

import { TimesheetsReport } from '../../models/timesheetsreport.model';

import { TimesheetsService } from '../../services/timesheets.service';
import { TimezonesService } from '../../services/timezones.service';

@Component({
  selector: 'app-timesheets-report',
  templateUrl: './timesheets-report.component.html'
})
export class TimesheetsReportComponent {
  public timesheetsReports: TimesheetsReport[];
  public timesheetsService: TimesheetsService;
  public timezonesService: TimezonesService;
  public totalHoursWorked: number = 0.00;
  public totalCost: number = 0.00;
  public reportDate: Date = new Date();

  constructor(timesheetsService: TimesheetsService, timezonesService: TimezonesService) {
    
    this.timesheetsService = timesheetsService;
    this.timezonesService = timezonesService;

    this.reportByDate();
  }

  reportByDate() {

    this.totalHoursWorked = 0.00;
    this.totalCost = 0.00;

    var self = this;

    this.timesheetsService.reportByDate(this.reportDate).subscribe(
      result => {


          self.timesheetsReports = result;
          for (let i = 0; i < self.timesheetsReports.length; i++) {

            self.timesheetsReports[i].formattedStartDate = self.timezonesService.formatDatetime(self.timesheetsReports[i].startDate, self.timesheetsReports[i].timezoneName);
            self.timesheetsReports[i].formattedEndDate = self.timezonesService.formatDatetime(self.timesheetsReports[i].endDate, self.timesheetsReports[i].timezoneName);

            self.totalHoursWorked += (self.timesheetsReports[i].hoursWorked != null ? self.timesheetsReports[i].hoursWorked : 0.00);

            self.totalCost += (self.timesheetsReports[i].cost != null ? self.timesheetsReports[i].cost : 0.00);
          }



      }, error => {
        alert(error.error.exceptionMessage)
      });
  }

}
