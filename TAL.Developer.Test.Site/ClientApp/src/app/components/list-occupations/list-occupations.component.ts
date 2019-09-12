import { Component } from '@angular/core';

import { Occupation } from '../../models/occupation.model';

import { OccupationsService } from '../../services/occupations.service'

@Component({
  selector: 'app-list-occupations',
  templateUrl: './list-occupations.component.html'
})
export class ListOccupationsComponent {
  public occupations: Occupation[];
  public occupationsService: OccupationsService;

  constructor(occupationsService: OccupationsService) {

    this.occupationsService = occupationsService;

    this.occupationsService.getList().subscribe(
      result => {
        this.occupations = result;
      }, error => {
        alert(error.error.exceptionMessage)
      });
  }
}
