import { Router, ActivatedRoute } from '@angular/router';
import { Component, ViewChild, AfterViewInit } from '@angular/core';
import { NgModel } from '@angular/forms';

import { Credentials } from './models/credentials.model';

import { SecurityService } from './services/security.service'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewInit {
  public router: Router;
  public securityService: SecurityService;

  public title = 'app';
  public credentials: Credentials = {
    username: '',
    password: ''
  }

  @ViewChild('loginScreen')
  private loginScreen: any;

  constructor(router: Router, securityService: SecurityService) {

    this.router = router;
    this.securityService = securityService;

  }

  ngAfterViewInit(): void {
    this.loginScreen.nativeElement.classList.add('show');
  }

  initSecurity(): void {
    this.securityService.init(this.credentials);
  }

  resetSecurity(): void {
    this.securityService.reset();

    location.reload(true);
  }

}
