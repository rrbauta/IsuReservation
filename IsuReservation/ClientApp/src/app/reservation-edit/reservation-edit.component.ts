import {Component, OnInit} from '@angular/core';
import {NotificationsService} from "../services/notifications.service";
import {ContactService} from "../services/contact.service";
import {ReservationService} from "../services/reservation.service";
import {DestinationService} from "../services/destination.service";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {ContactType} from "../models/contact-type.model";
import {Contact} from "../models/contact.model";
import {Destination} from "../models/destination.model";
import {Subject} from "rxjs";
import {debounceTime, delay, filter, map, takeUntil, tap} from "rxjs/operators";
import {Reservation} from "../models/reservation";

@Component({
  selector: 'app-reservation-edit',
  templateUrl: './reservation-edit.component.html',
  styleUrls: ['./reservation-edit.component.css']
})
export class ReservationEditComponent implements OnInit {

  title: string = "Edit Reservation";
  subtitle: string = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam.";
  reservation_list_button: boolean = true;
  description: string = '';
  isLoading: boolean = false;
  searching: boolean = false;
  contactTypes: ContactType[] = []
  contacts: Contact[] = [];
  destinations: Destination[] = []
  reservation!: Reservation;
  reservationId: string | null = ''
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
              public fb: FormBuilder, private router: Router,
              private route: ActivatedRoute) {

  }

  ngOnInit(): void {
    this.reservationId = this.route.snapshot.paramMap.get("id");
    this.getReservation(this.reservationId);

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

  getReservation(reservationId: string | null) {
    this.isLoading = true;
    if (reservationId != null) {
      this.reservationService.getReservation(reservationId)
        .subscribe(result => {
          this.reservation = result.data;

          console.log(new Date(this.reservation.date));
          console.log(this.reservation.date);

          this.getDestinations(this.reservation.destination.name);
          this.description = this.reservation.description;
          this.myForm = this.fb.group({
            description: [this.reservation.description, Validators.required],
            date: [new Date(this.reservation.date), Validators.required],
            contactName: [this.reservation.contact.name, Validators.required],
            contactPhoneNumber: [this.reservation.contact.phoneNumber, Validators.required],
            contactBirthDate: [new Date(this.reservation.contact.birthDate), Validators.required],
            contactTypeId: [this.reservation.contact.contactTypeId, Validators.required],
            contactId: [this.reservation.contact.id],
            destinationId: [this.reservation.destination.id, Validators.required]
          });

          this.isLoading = false;
        }, error => {
          console.log(error);
          this.isLoading = false;
        })
    }
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
    if (this.reservationId != null) {
      this.reservationService.update(this.myForm.value, this.reservationId)
        .subscribe(result => {
          this.isLoading = false;
          if (result.isSuccess) {
            this.notificationService.success('Reservation updated successfully');
            this.router.navigate(['/reservations'])
          } else {
            this.notificationService.error(result.exception);
          }
        }, error => {
          this.isLoading = false;
          this.notificationService.error(error.error.errorDescription);
        })
    }
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }

}
