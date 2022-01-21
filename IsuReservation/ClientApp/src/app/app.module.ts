import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';

import {AppComponent} from './app.component';
import {HomeComponent} from './home/home.component';
import {ReservationListComponent} from "./reservation-list/reservation-list.component";
import {MatTableModule} from "@angular/material/table";
import {MatPaginatorModule} from "@angular/material/paginator";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatTooltipModule} from "@angular/material/tooltip";
import {StarRatingComponent} from "./star-rating/star-rating.component";
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar';
import {MatSelectModule} from "@angular/material/select";
import {MatProgressBarModule} from "@angular/material/progress-bar";
import {ContactListComponent} from "./contact-list/contact-list.component";
import {MatSortModule} from "@angular/material/sort";
import {MatProgressSpinnerModule} from "@angular/material/progress-spinner";
import {ContactCreateComponent} from "./contact-create/contact-create.component";
import {MatDatepickerModule} from "@angular/material/datepicker";
import {MatRadioModule} from "@angular/material/radio";
import {MatCardModule} from "@angular/material/card";
import {MatInputModule} from "@angular/material/input";
import {MatNativeDateModule} from "@angular/material/core";
import {MatSidenavModule} from "@angular/material/sidenav";
import {MatChipsModule} from "@angular/material/chips";
import {MatDividerModule} from "@angular/material/divider";
import {MyTelInput} from "./custom-components/custom-phone-input/custom-phone-input.component";
import {MatButtonToggleModule} from "@angular/material/button-toggle";
import {ConfirmationDialogComponent} from "./custom-components/confirmation-dialog/confirmation-dialog.component";
import {MatDialogModule} from "@angular/material/dialog";
import {ContactEditComponent} from "./contact-edit/contact-edit.component";
import {MenuOptionsComponent} from "./custom-components/menu-options/menu-options.component";
import {HeaderComponent} from "./custom-components/header/header.component";

@NgModule({
  entryComponents: [ContactCreateComponent],
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    ReservationListComponent,
    StarRatingComponent,
    ContactListComponent,
    ContactCreateComponent,
    ContactEditComponent,
    MenuOptionsComponent,
    MyTelInput,
    ConfirmationDialogComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    RouterModule.forRoot([
      {path: '', component: HomeComponent, pathMatch: 'full'},
      {path: 'reservations', component: ReservationListComponent},
      {path: 'contacts', component: ContactListComponent},
      {path: 'contact-create', component: ContactCreateComponent},
      {path: 'contact-edit/:id', component: ContactEditComponent},
    ]),
    MatTableModule,
    MatPaginatorModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatTooltipModule,
    MatSnackBarModule,
    MatSelectModule,
    MatProgressBarModule,
    MatSortModule,
    MatProgressSpinnerModule,
    MatDatepickerModule,
    MatRadioModule,
    MatCardModule,
    MatInputModule,
    MatNativeDateModule,
    MatSidenavModule,
    MatChipsModule,
    MatDividerModule,
    MatButtonToggleModule,
    MatDialogModule
  ],
  providers: [MatSnackBar],
  bootstrap: [AppComponent]
})
export class AppModule {
}
