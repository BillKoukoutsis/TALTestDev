import { Router, ActivatedRoute } from '@angular/router';
import { Component, ViewChild, AfterViewInit } from '@angular/core';
import { NgModel } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewInit {
  public router: Router;

  public title = 'app';

  @ViewChild('loginScreen')
  private loginScreen: any;

  constructor(router: Router) {

    this.router = router;

  }

  ngAfterViewInit(): void {
    this.loginScreen.nativeElement.classList.add('show');
  }

}
