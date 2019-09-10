import { Response } from '@angular/http';
import { Injectable } from "@angular/core";

import { Observable } from "rxjs";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class BaseService {

  protected translateResponse(response: any): any {
    if (response != null && response._body != null && response._body.length > 0) {
      let response1: Response = response;
      return response1.json();
    } else {
      return {};
    }
  }

  protected translateThenThrowError(error: any): Observable<any> {
    if (error != null && error._body != null && error._body.length > 0) {
      return Observable.throw(JSON.parse(error._body));
    } else {
      return Observable.throw(JSON.parse('{}'));
    }
  }

}
