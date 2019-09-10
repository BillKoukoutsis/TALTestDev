import { Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Injectable } from "@angular/core";

import { Observable } from "rxjs";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import { Employee } from "../models/Employee.model";
import { Credentials } from '../models/credentials.model';
import { Group } from '../models/group.model';

import { BaseService } from '../services/base.service'
import { EmployeesService } from '../services/employees.service';
import { GroupsService } from '../services/groups.service';

@Injectable()
export class SecurityService extends BaseService {

  public employeesService: EmployeesService;
  public groupsService: GroupsService;

  public employee: Employee;
  public group: Group;
  public loggedIn: boolean = false;

  constructor(employeesService: EmployeesService, groupsService: GroupsService) {

    super();

    this.employeesService = employeesService;
    this.groupsService = groupsService

  }

  init(credentials: Credentials) {

    this.employee = <Employee>{};
    this.employee.id = 0;
    this.employee.name = null;
    this.employee.username = null;
    this.employee.password = null;
    this.employee.groupId = -1;
    this.employee.timezoneId = -1;
    this.employee.rate = 0.00;

    this.employeesService.getByCredentials(credentials).subscribe(

      result => {

        this.employee = result;

        if (this.employee == null) {

          alert('Invalid username and/or password');

        } else {

          this.groupsService.getById(this.employee.groupId).subscribe(

            result => {

              this.group = result;

              this.loggedIn = true;

            }, error => {
              alert(error.error.exceptionMessage)
            });

        }

      }, error => {
        alert(error.error.exceptionMessage)
      });
  }

  reset() {
    this.employee = null;
    this.group = null;
    this.loggedIn = false;
  }

}
