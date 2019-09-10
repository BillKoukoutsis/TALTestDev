import { Component } from '@angular/core';

import { Timezone } from '../../models/timezone.model';

import { TimezonesService } from '../../services/timezones.service'

@Component({
  selector: 'app-list-timezones',
  templateUrl: './list-timezones.component.html'
})
export class ListTimezonesComponent {
  public timezones: Timezone[];
  public timezonesService: TimezonesService;

  constructor(timezonesService: TimezonesService) {

    this.timezonesService = timezonesService;

    this.timezonesService.getList().subscribe(
      result => {
        this.timezones = result;
      }, error => {
        alert(error.error.exceptionMessage)
      });
  }
}
