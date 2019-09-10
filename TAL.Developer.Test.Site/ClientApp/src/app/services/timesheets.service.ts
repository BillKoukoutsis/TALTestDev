import { Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Injectable } from "@angular/core";

import { Observable } from "rxjs";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import { Timesheet } from "../models/Timesheet.model";
import { TimesheetsReport } from "../models/TimesheetsReport.model";

import { BaseService } from '../services/base.service'

@Injectable()
export class TimesheetsService extends BaseService {
  public http: Http;
  public apiUrl: string;

  constructor(http: Http, @Inject('API_URL') apiUrl: string) {

    super();

    this.http = http;
    this.apiUrl = apiUrl;

  }

  getList(): Observable<Timesheet[]> {

    return this.http.get(this.apiUrl + 'api/timesheets/getlist', { withCredentials: true })
      .map((response: Response) => {
        return this.translateResponse(response);
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })
  }

  reportByDate(date: Date): Observable<TimesheetsReport[]> {

    return this.http.post(this.apiUrl + 'api/timesheets/reportbydate', date, { withCredentials: true })
      .map((response: Response) => {
        return this.translateResponse(response);
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })
  }

  getById(id: number): Observable<Timesheet> {

    return this.http.get(this.apiUrl + 'api/timesheets/getbyid/' + id, { withCredentials: true })
      .map((response: Response) => {
        return this.translateResponse(response);
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })

  }

  insert(timesheet: Timesheet): Observable<any> {

    return this.http.post(this.apiUrl + 'api/timesheets/insert', timesheet, { withCredentials: true })
      .map((response: Response): Observable<any> => {
        return this.translateResponse(response);
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })

  }

  updateById(timesheet: Timesheet): Observable<any> {

    return this.http.post(this.apiUrl + 'api/timesheets/updatebyid', timesheet, { withCredentials: true })
      .map((response: Response): Observable<any> => {
        return this.translateResponse(response);
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })

  }

}
