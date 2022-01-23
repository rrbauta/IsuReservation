import {Component, OnDestroy, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup} from "@angular/forms";
import {ContactType} from "../models/contact-type.model";
import {NotificationsService} from "../services/notifications.service";
import {ContactService} from "../services/contact.service";
import {Router} from "@angular/router";
import {ReservationService} from "../services/reservation.service";
import {Contact} from "../models/contact.model";
import {debounceTime, delay, filter, map, takeUntil, tap} from "rxjs/operators";
import {Subject} from "rxjs";
import {Destination} from "../models/destination.model";
import {DestinationService} from "../services/destination.service";

@Component({
  selector: 'app-reservation-create',
  templateUrl: './reservation-create.component.html',
  styleUrls: ['./reservation-create.component.css']
})
export class ReservationCreateComponent implements OnInit, OnDestroy {

  title: string = "Create Reservation";
  subtitle: string = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam.";
  reservation_list_button: boolean = true;
  description: string = '';
  isLoading: boolean = false;
  searching: boolean = false;
  contactTypes: ContactType[] = []
  contacts: Contact[] = [];
  destinations: Destination[] = []
  /** control for filter for server side. */
  public destinationServerSideFilteringCtrl: FormControl = new FormControl();
  public contactsServerSideFilteringCtrl: FormControl = new FormControl();
  meridian: boolean = true;
  showSeconds: boolean = false;
  minDate: Date = new Date();
  myForm: FormGroup = this.fb.group({
    description: [''],
    date: [new Date()],
    contactName: [''],
    contactPhoneNumber: [''],
    contactBirthDate: [new Date('')],
    contactTypeId: [''],
    contactId: [''],
    destinationId: [''],
  });
  /** Subject that emits when the component has been destroyed. */
  protected _onDestroy = new Subject<void>();

  constructor(private notificationService: NotificationsService,
              private contactService: ContactService,
              private reservationService: ReservationService,
              private destinationService: DestinationService,
              public fb: FormBuilder, private router: Router) {

  }

  ngOnInit(): void {
    this.myForm = this.fb.group({
      description: [''],
      date: [new Date()],
      contactName: [''],
      contactPhoneNumber: [''],
      contactBirthDate: [new Date('')],
      contactTypeId: [''],
      contactId: [''],
      destinationId: [''],
    });

    this.getContactTypes();

    // listen for search field value changes
    this.destinationServerSideFilteringCtrl.valueChanges
      .pipe(
        filter(search => !!search),
        tap(() => this.searching = true),
        takeUntil(this._onDestroy),
        debounceTime(200),
        map(search => {
          return this.getDestinations(search);
        }),
        delay(500),
        takeUntil(this._onDestroy)
      )
      .subscribe(filtered => {
          this.searching = false;
          // this.filteredServerSideBanks.next(filtered);
        },
        error => {
          // no errors in our simulated example
          this.searching = false;
          // handle error...
        });
  }

  onChange($event: any): void {
    console.log("onChange");
    (<FormControl>this.myForm.controls['description']).setValue(this.description);
  }

  onPaste($event: any): void {
    console.log("onPaste");
  }

  getContactTypes() {
    this.isLoading = true;
    this.contactService.getContactTypes()
      .subscribe(result => {
        this.contactTypes = result.data;
        this.isLoading = false;
      }, error => {
        console.log(error);
        this.isLoading = false;
      })
  }

  getContacts(name: string) {
    if (name.length >= 3) {
      this.contactService.getContacts(1, -1, 'name', false, name)
        .subscribe(result => {
          this.contacts = result.data.outcome;
        }, error => {
          console.log(error);
        })
    }
  }

  selectContact(contact: Contact) {
    (<FormControl>this.myForm.controls['contactName']).setValue(contact.name);
    (<FormControl>this.myForm.controls['contactPhoneNumber']).setValue(contact.phoneNumber);
    (<FormControl>this.myForm.controls['contactBirthDate']).setValue(new Date(contact.birthDate));
    (<FormControl>this.myForm.controls['contactTypeId']).setValue(contact.contactTypeId);
    (<FormControl>this.myForm.controls['contactId']).setValue(contact.id);
  }

  getDestinations(name: string) {
    this.destinationService.getDestinations(name)
      .subscribe(result => {
        console.log(result.data)
        this.destinations = result.data;
      }, error => {
        console.log(error);
      })
  }

  submitForm() {
    this.isLoading = true;
    this.reservationService.create(this.myForm.value)
      .subscribe(result => {
        this.isLoading = false;
        if (result.isSuccess) {
          this.notificationService.success('Reservation added successfully');
          this.myForm.reset();
          this.description = '';
        } else {
          this.notificationService.error(result.exception);
        }
      }, error => {
        this.isLoading = false;
        this.notificationService.error(error.error.errorDescription);
      })
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }

}
