import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { OccupationRatingsService } from './services/occupation-ratings.service';
import { OccupationsService } from './services/occupations.service';
import { MembersService } from './services/members.service';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';

import { ListOccupationRatingsComponent } from './components/list-occupation-ratings/list-occupation-ratings.component';
import { EditOccupationRatingComponent } from './components/edit-occupation-rating/edit-occupation-rating.component';
import { ListOccupationsComponent } from './components/list-occupations/list-occupations.component';
import { EditOccupationComponent } from './components/edit-occupation/edit-occupation.component';
import { ListMembersComponent } from './components/list-members/list-members.component';
import { EditMemberComponent } from './components/edit-member/edit-member.component';

import { HttpModule } from '@angular/http';

import { OwlDateTimeModule, OwlNativeDateTimeModule, OWL_DATE_TIME_LOCALE } from 'ng-pick-datetime';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ListOccupationRatingsComponent,
    EditOccupationRatingComponent,
    ListOccupationsComponent,
    EditOccupationComponent,
    ListMembersComponent,
    EditMemberComponent,
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
      { path: 'list-occupation-ratings', component: ListOccupationRatingsComponent },
      { path: 'edit-occupation-rating/:id', component: EditOccupationRatingComponent },
      { path: 'list-occupations', component: ListOccupationsComponent },
      { path: 'edit-occupation/:id', component: EditOccupationComponent },
      { path: 'list-members', component: ListMembersComponent },
      { path: 'edit-member/:id', component: EditMemberComponent },
    ])
  ],
  providers: [
    // use aus locale for datetime picker
    { provide: OWL_DATE_TIME_LOCALE, useValue: 'en-AU' },
    OccupationRatingsService,
    OccupationsService,
    MembersService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
