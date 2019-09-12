import { Router, ActivatedRoute } from '@angular/router';
import { Component, ViewChild  } from '@angular/core';
import { NgModel } from '@angular/forms';

import { Member } from '../../models/member.model';
import { Occupation } from '../../models/occupation.model';

import { MembersService } from '../../services/members.service'
import { OccupationsService } from '../../services/occupations.service';


@Component({
  selector: 'app-edit-member',
  templateUrl: './edit-member.component.html'
})
export class EditMemberComponent {
  public router: Router;
  public route: ActivatedRoute;
  public membersService: MembersService;
  public occupationsService: OccupationsService;

  public member: Member;
  public occupations: Occupation[];
  public id: number;

  @ViewChild('inputMemberName')
  private inputMemberName: NgModel;
  @ViewChild('inputMemberDOB')
  private inputMemberDOB: NgModel;
  @ViewChild('inputMemberSumInsured')
  private inputMemberSumInsured: NgModel;
  @ViewChild('inputMemberOccupation')
  private inputMemberOccupation: NgModel;

  constructor(router: Router, route: ActivatedRoute, membersService: MembersService, occupationsService: OccupationsService) {

    this.router = router;
    this.route = route;
    this.membersService = membersService;
    this.occupationsService = occupationsService;

  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      this.getOccupations();
      this.getMember();
    });
  }

  getOccupations() {
    this.occupationsService.getList().subscribe(
      result => {
        this.occupations = result;
      }, error => {
        alert(error.error.exceptionMessage)
      });
  }

  getMember() {

    if (this.id == 0) {
      this.member = <Member>{};
      this.member.id = 0;
      this.member.name = null;
      this.member.dob = new Date();
      this.member.sumInsured = 0.0;
      this.member.occupation = <Occupation>{};
      this.member.occupation.id = -1;
      return;
    }

    this.membersService.getById(this.id).subscribe(
      result => {
        this.member = result;
      }, error => {
        alert(error.error.exceptionMessage)
      });
  }

  saveMember() {

    if (this.inputMemberName.invalid) {
      alert("Name is required and must be at most 150 characters long.");
      return;
    }

    if (this.inputMemberDOB.invalid) {
      alert("Date of birth is required.");
      return;
    }

    if (this.inputMemberSumInsured.invalid) {
      alert("Sum insured is required.");
      return;
    }

    if (this.inputMemberOccupation.invalid) {
      alert("Occupation is required.");
      return;
    }

    if (this.member.id == 0) {

      this.membersService.insert(this.member).subscribe(
        result => {
          alert('insert successul');
          this.router.navigate(['/list-members']);
        }, error => {
          alert(error.exceptionMessage)
        });

    } else {

      this.membersService.updateById(this.member).subscribe(
        result => {
          alert('update successul');
          this.router.navigate(['/list-members']);
        }, error => {
          alert(error.exceptionMessage)
        });

    }

  }

  recalculatePremium() {
    
    if (this.member.occupation == null || this.member.occupation.id <= 0) {
      return;
    }

    this.occupationsService.getById(this.member.occupation.id).subscribe(
      result => {
        this.member.occupation = result;

        this.membersService.calculatePremium(this.member).subscribe(
          result => {
            this.member.premium = result;
          }, error => {
            alert(error.error.exceptionMessage)
          });

      }, error => {
        alert(error.error.exceptionMessage)
      });
  }

}
