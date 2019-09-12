import { Router, ActivatedRoute } from '@angular/router';
import { Component, ViewChild  } from '@angular/core';
import { NgModel } from '@angular/forms';

import { Occupation } from '../../models/occupation.model';
import { OccupationRating } from '../../models/occupation-rating.model';

import { OccupationsService } from '../../services/occupations.service'
import { OccupationRatingsService } from '../../services/occupation-ratings.service';


@Component({
  selector: 'app-edit-occupation',
  templateUrl: './edit-occupation.component.html'
})
export class EditOccupationComponent {
  public router: Router;
  public route: ActivatedRoute;
  public occupationsService: OccupationsService;
  public occupationRatingsService: OccupationRatingsService;

  public occupation: Occupation;
  public occupationRatings: OccupationRating[];
  public id: number;

  @ViewChild('inputOccupationName')
  private inputOccupationName: NgModel;
  @ViewChild('inputOccupationOccupationRating')
  private inputOccupationOccupationRating: NgModel;

  constructor(router: Router, route: ActivatedRoute, occupationsService: OccupationsService, occupationRatingsService: OccupationRatingsService) {

    this.router = router;
    this.route = route;
    this.occupationsService = occupationsService;
    this.occupationRatingsService = occupationRatingsService;

  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      this.getOccupationRatings();
      this.getOccupation();
    });
  }

  getOccupationRatings() {
    this.occupationRatingsService.getList().subscribe(
      result => {
        this.occupationRatings = result;
      }, error => {
        alert(error.error.exceptionMessage)
      });
  }

  getOccupation() {

    if (this.id == 0) {
      this.occupation = <Occupation>{};
      this.occupation.id = 0;
      this.occupation.name = null;
      this.occupation.occupationRating = <OccupationRating>{};
      this.occupation.occupationRating.id = -1;
      return;
    }

    this.occupationsService.getById(this.id).subscribe(
      result => {
        this.occupation = result;
      }, error => {
        alert(error.error.exceptionMessage)
      });
  }

  saveOccupation() {

    if (this.inputOccupationName.invalid) {
      alert("Name is required and must be at most 150 characters long.");
      return;
    }

    if (this.inputOccupationOccupationRating.invalid) {
      alert("Occupation rating is required.");
      return;
    }

    if (this.occupation.id == 0) {

      this.occupationsService.insert(this.occupation).subscribe(
        result => {
          alert('insert successul');
          this.router.navigate(['/list-occupations']);
        }, error => {
          alert(error.exceptionMessage)
        });

    } else {

      this.occupationsService.updateById(this.occupation).subscribe(
        result => {
          alert('update successul');
          this.router.navigate(['/list-occupations']);
        }, error => {
          alert(error.exceptionMessage)
        });

    }

  }

}
