import { Router, ActivatedRoute } from '@angular/router';
import { Component, ViewChild  } from '@angular/core';
import { NgModel } from '@angular/forms';

import { Timesheet } from '../../models/timesheet.model';
import { Employee } from '../../models/employee.model';
import { Timezone } from '../../models/timezone.model';

import { TimesheetsService } from '../../services/timesheets.service'
import { EmployeesService } from '../../services/employees.service';
import { TimezonesService } from '../../services/timezones.service';


@Component({
  selector: 'app-edit-timesheet',
  templateUrl: './edit-timesheet.component.html'
})
export class EditTimesheetComponent {
  public router: Router;
  public route: ActivatedRoute;
  public timesheetsService: TimesheetsService;
  public employeesService: EmployeesService;
  public timezonesService: TimezonesService;

  public timesheet: Timesheet;
  public employees: Employee[];
  public timezones: Timezone[];
  public id: number;

  @ViewChild('inputTimesheetEmployee')
  private inputTimesheetEmployee: NgModel;
  @ViewChild('inputTimesheetTimezone')
  private inputTimesheetTimezone: NgModel;
  @ViewChild('inputTimesheetStartDate')
  private inputTimesheetStartDate: NgModel;
  @ViewChild('inputTimesheetRate')
  private inputTimesheetRate: NgModel;


  constructor(router: Router, route: ActivatedRoute, timesheetsService: TimesheetsService, employeesService: EmployeesService, timezonesService: TimezonesService) {

    this.router = router;
    this.route = route;
    this.timesheetsService = timesheetsService;
    this.employeesService = employeesService;
    this.timezonesService = timezonesService;

  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      this.getEmployees();
      this.getTimezones();
      this.getTimesheet();
    });
  }

  getEmployees() {
    this.employeesService.getList().subscribe(
      result => {
        this.employees = result;
      }, error => {
        alert(error.error.exceptionMessage)
      });
  }

  getTimezones() {
    this.timezonesService.getList().subscribe(
      result => {
        this.timezones = result;
      }, error => {
        alert(error.error.exceptionMessage)
      });
  }

  getSelectedEmployee() {
    console.log(2);
    this.employeesService.getById(this.timesheet.employeeId).subscribe(
      result => {
        this.timesheet.timezoneId = result.timezoneId;
        this.timesheet.rate = result.rate;
      }, error => {
        alert(error.error.exceptionMessage)
      });
  }

  getTimesheet() {

    if (this.id == 0) {
      this.timesheet = <Timesheet>{};
      this.timesheet.id = 0;
      this.timesheet.employeeId = 0;
      this.timesheet.timezoneId = 0;
      this.timesheet.startDate = null;
      this.timesheet.endDate = null;
      this.timesheet.rate = 0;
      return;
    }

    this.timesheetsService.getById(this.id).subscribe(
      result => {
        this.timesheet = result;
      }, error => {
        alert(error.error.exceptionMessage)
      });
  }

  saveTimesheet() {

    if (this.inputTimesheetEmployee && this.inputTimesheetEmployee.invalid) {
      alert("Employee is required.");
      return;
    }

    if (this.inputTimesheetTimezone && this.inputTimesheetTimezone.invalid) {
      alert("Timezone is required.");
      return;
    }

    if (this.inputTimesheetStartDate && this.inputTimesheetStartDate.invalid) {
      alert("Start date is required.");
      return;
    }

    if (this.inputTimesheetRate && this.inputTimesheetRate.invalid) {
      alert("Rate is required.");
      return;
    }

    if (this.timesheet.id == 0) {

      this.timesheetsService.insert(this.timesheet).subscribe(
        result => {
          alert('insert successul');
          this.router.navigate(['/list-timesheets']);
        }, error => {
          alert(error.exceptionMessage)
        });

    } else {

      this.timesheetsService.updateById(this.timesheet).subscribe(
        result => {
          alert('update successul');
          this.router.navigate(['/list-timesheets']);
        }, error => {
          alert(error.exceptionMessage)
        });

    }

  }

}
