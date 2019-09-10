import { Component } from '@angular/core';

import { Employee } from '../../models/employee.model';

import { EmployeesService } from '../../services/employees.service'

@Component({
  selector: 'app-list-employees',
  templateUrl: './list-employees.component.html'
})
export class ListEmployeesComponent {
  public employees: Employee[];
  public employeesService: EmployeesService;

  constructor(employeesService: EmployeesService) {

    this.employeesService = employeesService;

    this.employeesService.getList().subscribe(
      result => {
        this.employees = result;
      }, error => {
        alert(error.error.exceptionMessage)
      });
  }
}
