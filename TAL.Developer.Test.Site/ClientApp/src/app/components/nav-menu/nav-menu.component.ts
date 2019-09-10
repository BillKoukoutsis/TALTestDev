import { Component } from '@angular/core';

import { SecurityService } from '../../services/security.service'

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  public securityService: SecurityService;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  constructor(securityService: SecurityService) {

    this.securityService = securityService;
  }

}
