import { Router, ActivatedRoute } from '@angular/router';
import { Component, ViewChild  } from '@angular/core';
import { NgModel } from '@angular/forms';

import { Group } from '../../models/group.model';

import { GroupsService } from '../../services/groups.service'

@Component({
  selector: 'app-edit-group',
  templateUrl: './edit-group.component.html'
})
export class EditGroupComponent {
  public router: Router;
  public route: ActivatedRoute;
  public groupsService: GroupsService;

  public group: Group;
  public id: number;

  @ViewChild('inputGroupName')
  private inputGroupName: NgModel;

  constructor(router: Router, route: ActivatedRoute, groupsService: GroupsService) {

    this.router = router;
    this.route = route;
    this.groupsService = groupsService;

  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      this.getGroup();
    });
  }

  getGroup() {

    if (this.id == 0) {
      this.group = <Group>{};
      this.group.id = 0;
      this.group.name = null;
      return;
    }

    this.groupsService.getById(this.id).subscribe(
      result => {
        this.group = result;
      }, error => {
        alert(error.error.exceptionMessage)
      });
  }

  saveGroup() {

    if (this.inputGroupName.invalid) {
      alert("Name is required and must be at most 50 characters long.");
      return;
    }

    if (this.group.id == 0) {

      this.groupsService.insert(this.group).subscribe(
        result => {
          alert('insert successul');
          this.router.navigate(['/list-groups']);
        }, error => {
          alert(error.exceptionMessage)
        });

    } else {

      this.groupsService.updateById(this.group).subscribe(
        result => {
          alert('update successul');
          this.router.navigate(['/list-groups']);
        }, error => {
          alert(error.exceptionMessage)
        });

    }

  }

}
