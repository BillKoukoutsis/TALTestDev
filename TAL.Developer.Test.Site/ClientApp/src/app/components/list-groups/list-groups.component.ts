import { Component } from '@angular/core';

import { Group } from '../../models/group.model';

import { GroupsService } from '../../services/groups.service'

@Component({
  selector: 'app-list-groups',
  templateUrl: './list-groups.component.html'
})
export class ListGroupsComponent {
  public groups: Group[];
  public groupsService: GroupsService;

  constructor(groupsService: GroupsService) {

    this.groupsService = groupsService;

    this.groupsService.getList().subscribe(
      result => {
        this.groups = result;
      }, error => {
        alert(error.error.exceptionMessage)
      });
  }
}
