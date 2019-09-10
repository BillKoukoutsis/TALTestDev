import { Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Injectable } from "@angular/core";

import { Observable } from "rxjs";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import { Group } from "../models/Group.model";

import { BaseService } from '../services/base.service'

@Injectable()
export class GroupsService extends BaseService {
  public http: Http;
  public apiUrl: string;

  constructor(http: Http, @Inject('API_URL') apiUrl: string) {

    super();

    this.http = http;
    this.apiUrl = apiUrl;

  }

  getList(): Observable<Group[]> {

    return this.http.get(this.apiUrl + 'api/groups/getlist', { withCredentials: true })
      .map((response: Response) => {
        return this.translateResponse(response);
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })
  }

  getById(id: number): Observable<Group> {

    return this.http.get(this.apiUrl + 'api/groups/getbyid/' + id, { withCredentials: true })
      .map((response: Response) => {
        return this.translateResponse(response);
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })

  }

  insert(group: Group): Observable<any> {

    return this.http.post(this.apiUrl + 'api/groups/insert', group, { withCredentials: true })
      .map((response: Response): Observable<any> => {
        return this.translateResponse(response);
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })

  }

  updateById(group: Group): Observable<any> {

    return this.http.post(this.apiUrl + 'api/groups/updatebyid', group, { withCredentials: true })
      .map((response: Response): Observable<any> => {
        return this.translateResponse(response);
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })

  }

}
