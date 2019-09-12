import { Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Injectable } from "@angular/core";

import { Observable } from "rxjs";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import { Occupation } from "../models/occupation.model";

import { BaseService } from '../services/base.service'

@Injectable()
export class OccupationsService extends BaseService {
  public http: Http;
  public apiUrl: string;

  constructor(http: Http, @Inject('API_URL') apiUrl: string) {

    super();

    this.http = http;
    this.apiUrl = apiUrl;

  }

  getList(): Observable<Occupation[]> {

    return this.http.get(this.apiUrl + 'api/occupations/getlist', { withCredentials: true })
      .map((response: Response) => {
        return this.translateResponse(response);
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })
  }

  getById(id: number): Observable<Occupation> {

    return this.http.get(this.apiUrl + 'api/occupations/getbyid/' + id, { withCredentials: true })
      .map((response: Response) => {
        return this.translateResponse(response);
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })

  }

  insert(occupation: Occupation): Observable<any> {

    return this.http.post(this.apiUrl + 'api/occupations/insert', occupation, { withCredentials: true })
      .map((response: Response): Observable<any> => {
        return this.translateResponse(response);
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })

  }

  updateById(occupation: Occupation): Observable<any> {

    return this.http.post(this.apiUrl + 'api/occupations/updatebyid', occupation, { withCredentials: true })
      .map((response: Response): Observable<any> => {
        return this.translateResponse(response);
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })

  }

}
