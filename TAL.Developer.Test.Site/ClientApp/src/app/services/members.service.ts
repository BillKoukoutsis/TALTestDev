import { Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Injectable } from "@angular/core";

import { Observable } from "rxjs";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import { Member } from "../models/member.model";

import { BaseService } from '../services/base.service'

@Injectable()
export class MembersService extends BaseService {
  public http: Http;
  public apiUrl: string;

  constructor(http: Http, @Inject('API_URL') apiUrl: string) {

    super();

    this.http = http;
    this.apiUrl = apiUrl;

  }

  getList(): Observable<Member[]> {

    return this.http.get(this.apiUrl + 'api/members/getlist', { withCredentials: true })
      .map((response: Response) => {
        let members: Member[] = this.translateResponse(response);

        for (let i = 0; i < members.length; i++) {
          let member: Member = members[i];

          this.calculatePremium(member).subscribe(
            result => {
              member.premium = result;
            }, error => {
              alert(error.error.exceptionMessage)
            });

        }
        
        return members;
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })
  }

  getById(id: number): Observable<Member> {

    return this.http.get(this.apiUrl + 'api/members/getbyid/' + id, { withCredentials: true })
      .map((response: Response) => {
        let member: Member = this.translateResponse(response);

        this.calculatePremium(member).subscribe(
          result => {
            member.premium = result;
          }, error => {
            alert(error.error.exceptionMessage)
          });

        return member;
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })

  }

  insert(member: Member): Observable<any> {

    return this.http.post(this.apiUrl + 'api/members/insert', member, { withCredentials: true })
      .map((response: Response): Observable<any> => {
        return this.translateResponse(response);
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })

  }

  updateById(member: Member): Observable<any> {

    return this.http.post(this.apiUrl + 'api/members/updatebyid', member, { withCredentials: true })
      .map((response: Response): Observable<any> => {
        return this.translateResponse(response);
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })

  }

  calculatePremium(member: Member): Observable<number> {

    return this.http.post(this.apiUrl + 'api/members/calculatepremium', member, { withCredentials: true })
      .map((response: Response): Observable<any> => {
        return this.translateResponse(response);
      })
      .catch((error: any): Observable<any> => {
        return this.translateThenThrowError(error);
      })

  }

}
