import { Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Injectable } from "@angular/core";

import { Observable } from "rxjs";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import { Employee } from "../models/Employee.model";

import { BaseService } from '../services/base.service'
import { Credentials } from '../models/Credentials.model';

@Injectable()
export class EmployeesService extends BaseService {
  public http: Http;
  public apiUrl: string;

  constructor(http: Http, @Inject('API_URL') apiUrl: string) {

    super();

    this.http = http;
    this.apiUrl = apiUrl;

  }

  getList(): Observable<Employee[]> {

    return this.http.get(this.apiUrl + 'api/employees/getlist', { withCredentials: true })
      .map((response: Response) => {
        return this.translateResponse(response);
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })
  }

  getById(id: number): Observable<Employee> {

    return this.http.get(this.apiUrl + 'api/employees/getbyid/' + id, { withCredentials: true })
      .map((response: Response) => {
        return this.translateResponse(response);
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })

  }

  getByCredentials(credentials: Credentials): Observable<Employee> {

    return this.http.post(this.apiUrl + 'api/employees/getbycredentials', credentials, { withCredentials: true })
      .map((response: Response) => {
        return this.translateResponse(response);
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })

  }

  insert(employee: Employee): Observable<any> {

    return this.http.post(this.apiUrl + 'api/employees/insert', employee, { withCredentials: true })
      .map((response: Response): Observable<any> => {
        return this.translateResponse(response);
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })

  }

  updateById(employee: Employee): Observable<any> {

    return this.http.post(this.apiUrl + 'api/employees/updatebyid', employee, { withCredentials: true })
      .map((response: Response): Observable<any> => {
        return this.translateResponse(response);
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })

  }

}
