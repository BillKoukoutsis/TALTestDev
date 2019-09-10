import { Router, ActivatedRoute } from '@angular/router';
import { Component, ViewChild  } from '@angular/core';
import { NgModel } from '@angular/forms';

import { Timezone } from '../../models/timezone.model';

import { TimezonesService } from '../../services/timezones.service'

@Component({
  selector: 'app-edit-timezone',
  templateUrl: './edit-timezone.component.html'
})
export class EditTimezoneComponent {
  public router: Router;
  public route: ActivatedRoute;
  public timezonesService: TimezonesService;

  public timezone: Timezone;
  public id: number;

  @ViewChild('inputTimezoneName')
  private inputTimezoneName: NgModel;

  constructor(router: Router, route: ActivatedRoute, timezonesService: TimezonesService) {

    this.router = router;
    this.route = route;
    this.timezonesService = timezonesService;

  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      this.getTimezone();
    });
  }

  getTimezone() {

    if (this.id == 0) {
      this.timezone = <Timezone>{};
      this.timezone.id = 0;
      this.timezone.name = null;
      return;
    }

    this.timezonesService.getById(this.id).subscribe(
      result => {
        this.timezone = result;
      }, error => {
        alert(error.error.exceptionMessage)
      });
  }

  saveTimezone() {

    if (this.inputTimezoneName.invalid) {
      alert("Name is required and must be at most 150 characters long.");
      return;
    }

    if (this.timezone.id == 0) {

      this.timezonesService.insert(this.timezone).subscribe(
        result => {
          alert('insert successul');
          this.router.navigate(['/list-timezones']);
        }, error => {
          alert(error.exceptionMessage)
        });

    } else {

      this.timezonesService.updateById(this.timezone).subscribe(
        result => {
          alert('update successul');
          this.router.navigate(['/list-timezones']);
        }, error => {
          alert(error.exceptionMessage)
        });

    }

  }

}
