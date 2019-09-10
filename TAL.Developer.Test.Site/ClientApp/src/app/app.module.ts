import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { GroupsService } from './services/groups.service';
import { TimezonesService } from './services/timezones.service';
import { EmployeesService } from './services/employees.service';
import { TimesheetsService } from './services/timesheets.service';
import { SecurityService } from './services/security.service';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { ListGroupsComponent } from './components/list-groups/list-groups.component';
import { EditGroupComponent } from './components/edit-group/edit-group.component';
import { ListTimezonesComponent } from './components/list-timezones/list-timezones.component';
import { EditTimezoneComponent } from './components/edit-timezone/edit-timezone.component';
import { ListEmployeesComponent } from './components/list-employees/list-employees.component';
import { EditEmployeeComponent } from './components/edit-employee/edit-employee.component';
import { ListTimesheetsComponent } from './components/list-timesheets/list-timesheets.component';
import { EditTimesheetComponent } from './components/edit-timesheet/edit-timesheet.component';

import { TimesheetsReportComponent } from './components/timesheets-report/timesheets-report.component';

import { HttpModule } from '@angular/http';

import { OwlDateTimeModule, OwlNativeDateTimeModule, OWL_DATE_TIME_LOCALE } from 'ng-pick-datetime';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ListGroupsComponent,
    EditGroupComponent,
    ListTimezonesComponent,
    EditTimezoneComponent,
    ListEmployeesComponent,
    EditEmployeeComponent,
    ListTimesheetsComponent,
    EditTimesheetComponent,
    TimesheetsReportComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    OwlDateTimeModule,
    OwlNativeDateTimeModule,
    HttpClientModule,
    HttpModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'list-groups', component: ListGroupsComponent },
      { path: 'edit-group/:id', component: EditGroupComponent },
      { path: 'list-timezones', component: ListTimezonesComponent },
      { path: 'edit-timezone/:id', component: EditTimezoneComponent },
      { path: 'list-employees', component: ListEmployeesComponent },
      { path: 'edit-employee/:id', component: EditEmployeeComponent },
      { path: 'list-timesheets', component: ListTimesheetsComponent },
      { path: 'edit-timesheet/:id', component: EditTimesheetComponent },
      { path: 'timesheets-report', component: TimesheetsReportComponent },
    ])
  ],
  providers: [
    GroupsService,
    TimezonesService,
    EmployeesService,
    TimesheetsService,
    SecurityService,
    // use aus locale for datetime picker
    { provide: OWL_DATE_TIME_LOCALE, useValue: 'en-AU' },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
