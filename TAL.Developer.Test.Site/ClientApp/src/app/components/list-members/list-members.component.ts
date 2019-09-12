import { Component } from '@angular/core';

import { Member } from '../../models/member.model';

import { MembersService } from '../../services/members.service'
import { isString } from 'util';

@Component({
  selector: 'app-list-members',
  templateUrl: './list-members.component.html'
})
export class ListMembersComponent {
  public members: Member[];
  public membersService: MembersService;

  constructor(membersService: MembersService) {

    this.membersService = membersService;

    this.membersService.getList().subscribe(
      result => {
        this.members = result;
        for (let i = 0; i < this.members.length; i++) {
          let member: Member = this.members[i];

          member.dob = new Date(Date.parse(member.dob.toString()));
        }

      }, error => {
        alert(error.error.exceptionMessage)
      });
  }
}
