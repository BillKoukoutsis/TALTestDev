import { Router, ActivatedRoute } from '@angular/router';
import { Component, ViewChild  } from '@angular/core';
import { NgModel } from '@angular/forms';

import { OccupationRating } from '../../models/occupation-rating.model';

import { OccupationRatingsService } from '../../services/occupation-ratings.service'


@Component({
  selector: 'app-edit-occupation-rating',
  templateUrl: './edit-occupation-rating.component.html'
})
export class EditOccupationRatingComponent {
  public router: Router;
  public route: ActivatedRoute;
  public occupationRatingsService: OccupationRatingsService;
  
  public occupationRating: OccupationRating;
  public id: number;

  @ViewChild('inputOccupationRatingName')
  private inputOccupationRatingName: NgModel;
  @ViewChild('inputOccupationRatingFactor')
  private inputOccupationRatingFactor: NgModel;

  constructor(router: Router, route: ActivatedRoute, occupationRatingsService: OccupationRatingsService) {

    this.router = router;
    this.route = route;
    this.occupationRatingsService = occupationRatingsService;

  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      this.getOccupationRating();
    });
  }

  getOccupationRating() {

    if (this.id == 0) {
      this.occupationRating = <OccupationRating>{};
      this.occupationRating.id = 0;
      this.occupationRating.name = null;
      return;
    }

    this.occupationRatingsService.getById(this.id).subscribe(
      result => {
        this.occupationRating = result;
      }, error => {
        alert(error.error.exceptionMessage)
      });
  }

  saveOccupationRating() {

    if (this.inputOccupationRatingName.invalid) {
      alert("Name is required and must be at most 150 characters long.");
      return;
    }

    if (this.inputOccupationRatingFactor.invalid) {
      alert("Factor is required.");
      return;
    }

    if (this.occupationRating.id == 0) {

      this.occupationRatingsService.insert(this.occupationRating).subscribe(
        result => {
          alert('insert successul');
          this.router.navigate(['/list-occupation-ratings']);
        }, error => {
          alert(error.exceptionMessage)
        });

    } else {

      this.occupationRatingsService.updateById(this.occupationRating).subscribe(
        result => {
          alert('update successul');
          this.router.navigate(['/list-occupation-ratings']);
        }, error => {
          alert(error.exceptionMessage)
        });

    }

  }

}
