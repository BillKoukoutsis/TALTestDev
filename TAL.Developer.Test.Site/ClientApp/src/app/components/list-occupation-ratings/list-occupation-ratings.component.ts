import { Component } from '@angular/core';

import { OccupationRating } from '../../models/occupation-rating.model';

import { OccupationRatingsService } from '../../services/occupation-ratings.service'

@Component({
  selector: 'app-list-occupation-ratings',
  templateUrl: './list-occupation-ratings.component.html'
})
export class ListOccupationRatingsComponent {
  public occupationRatings: OccupationRating[];
  public occupationRatingsService: OccupationRatingsService;

  constructor(occupationRatingsService: OccupationRatingsService) {

    this.occupationRatingsService = occupationRatingsService;

    this.occupationRatingsService.getList().subscribe(
      result => {
        this.occupationRatings = result;
      }, error => {
        alert(error.error.exceptionMessage)
      });
  }
}
