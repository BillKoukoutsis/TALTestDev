import { Router, ActivatedRoute } from '@angular/router';
import { Component, ViewChild  } from '@angular/core';
import { NgModel } from '@angular/forms';

import { Employee } from '../../models/employee.model';
import { Group } from '../../models/group.model';
import { Timezone } from '../../models/timezone.model';

import { EmployeesService } from '../../services/employees.service'
import { GroupsService } from '../../services/groups.service';
import { TimezonesService } from '../../services/timezones.service';


@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html'
})
export class EditEmployeeComponent {
  public router: Router;
  public route: ActivatedRoute;
  public employeesService: EmployeesService;
  public groupsService: GroupsService;
  public timezonesService: TimezonesService;

  public employee: Employee;
  public groups: Group[];
  public timezones: Timezone[];
  public id: number;

  @ViewChild('inputEmployeeName')
  private inputEmployeeName: NgModel;
  @ViewChild('inputEmployeeUsername')
  private inputEmployeeUsername: NgModel;
  @ViewChild('inputEmployeePassword')
  private inputEmployeePassword: NgModel;
  @ViewChild('inputEmployeeGroup')
  private inputEmployeeGroup: NgModel;
  @ViewChild('inputEmployeeTimezone')
  private inputEmployeeTimezone: NgModel;
  @ViewChild('inputEmployeeRate')
  private inputEmployeeRate: NgModel;

  constructor(router: Router, route: ActivatedRoute, employeesService: EmployeesService, groupsService: GroupsService, timezonesService: TimezonesService) {

    this.router = router;
    this.route = route;
    this.employeesService = employeesService;
    this.groupsService = groupsService;
    this.timezonesService = timezonesService;

  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      this.getGroups();
      this.getTimezones();
      this.getEmployee();
    });
  }

  getGroups() {
    this.groupsService.getList().subscribe(
      result => {
        this.groups = result;
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

  getEmployee() {

    if (this.id == 0) {
      this.employee = <Employee>{};
      this.employee.id = 0;
      this.employee.name = null;
      this.employee.username = null;
      this.employee.password = null;
      this.employee.groupId = -1;
      this.employee.timezoneId = -1;
      this.employee.rate = 0.00;
      return;
    }

    this.employeesService.getById(this.id).subscribe(
      result => {
        this.employee = result;
      }, error => {
        alert(error.error.exceptionMessage)
      });
  }

  saveEmployee() {

    if (this.inputEmployeeName.invalid) {
      alert("Name is required and must be at most 150 characters long.");
      return;
    }

    if (this.inputEmployeeUsername.invalid) {
      alert("Username is required and must be at most 50 characters long.");
      return;
    }

    if (this.inputEmployeePassword.invalid) {
      alert("Password is required and must be at most 50 characters long.");
      return;
    }

    if (this.inputEmployeeGroup.invalid) {
      alert("Group is required.");
      return;
    }

    if (this.inputEmployeeTimezone.invalid) {
      alert("Timezone is required.");
      return;
    }

    if (this.inputEmployeeRate.invalid) {
      alert("Rate is required.");
      return;
    }

    if (this.employee.id == 0) {

      this.employeesService.insert(this.employee).subscribe(
        result => {
          alert('insert successul');
          this.router.navigate(['/list-employees']);
        }, error => {
          alert(error.exceptionMessage)
        });

    } else {

      this.employeesService.updateById(this.employee).subscribe(
        result => {
          alert('update successul');
          this.router.navigate(['/list-employees']);
        }, error => {
          alert(error.exceptionMessage)
        });

    }

  }

}
