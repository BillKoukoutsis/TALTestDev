import { Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Injectable } from "@angular/core";
import { DatePipe } from '@angular/common';

import { Observable } from "rxjs";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import { Timezone } from "../models/Timezone.model";

import { BaseService } from '../services/base.service'

@Injectable()
export class TimezonesService extends BaseService {
  public http: Http;
  public apiUrl: string;

  constructor(http: Http, @Inject('API_URL') apiUrl: string) {

    super();

    this.http = http;
    this.apiUrl = apiUrl;

  }

  getList(): Observable<Timezone[]> {

    return this.http.get(this.apiUrl + 'api/timezones/getlist', { withCredentials: true })
      .map((response: Response) => {
        return this.translateResponse(response);
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })
  }

  getById(id: number): Observable<Timezone> {

    return this.http.get(this.apiUrl + 'api/timezones/getbyid/' + id, { withCredentials: true })
      .map((response: Response) => {
        return this.translateResponse(response);
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })

  }

  insert(timezone: Timezone): Observable<any> {

    return this.http.post(this.apiUrl + 'api/timezones/insert', timezone, { withCredentials: true })
      .map((response: Response): Observable<any> => {
        return this.translateResponse(response);
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })

  }

  updateById(timezone: Timezone): Observable<any> {

    return this.http.post(this.apiUrl + 'api/timezones/updatebyid', timezone, { withCredentials: true })
      .map((response: Response): Observable<any> => {
        return this.translateResponse(response);
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })

  }

  formatDatetime(date: Date, timezoneName: string): string {
    switch (timezoneName) {
      case 'E. Australia Standard Time':
      default: 
        let pipe = new DatePipe('en-AU');
        return pipe.transform(date, 'dd/MM/yyyy HH:mm');
    };
  }

}
