import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ContactService} from "../services/contact.service";
import {ContactType} from "../models/contact-type.model";
import {Router} from "@angular/router";
import {NotificationsService} from "../services/notifications.service";
import {TranslateService} from "@ngx-translate/core";

export interface Subject {
  name: string;
}

@Component({
  selector: 'app-contact-create',
  templateUrl: './contact-create.component.html',
  styleUrls: ['./contact-create.component.css']
})
export class ContactCreateComponent implements OnInit {

  title: string = "Create Contact";
  subtitle: string = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam.";
  contact_list_button: boolean = true;
  myForm: FormGroup = this.fb.group({
    name: [''],
    phoneNumber: [''],
    birthDate: [''],
    contactType: ['']
  });
  isLoading: boolean = false;
  contactTypes: ContactType[] = []

  constructor(private notificationService: NotificationsService, private contactService: ContactService,
              public fb: FormBuilder, private router: Router, private translateService: TranslateService) {
  }

  ngOnInit(): void {
    this.myForm = this.fb.group({
      name: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      birthDate: ['', Validators.required],
      contactType: ['', Validators.required]
    });

    this.getContactTypes();
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

  submitForm() {
    this.isLoading = true;
    this.contactService.create(this.myForm.value)
      .subscribe(result => {
        this.isLoading = false;
        if (result.isSuccess) {
          this.notificationService.success(this.translateService.instant('Contact added successfully'));
          this.myForm.reset();
        } else {
          this.notificationService.error(result.exception);
        }
      }, error => {
        this.isLoading = false;
        if (error.error.errorDescription === undefined) {
          this.notificationService.error(this.translateService.instant('Something went wrong. Please try again later'));
        } else {
          this.notificationService.error(error.error.errorDescription);
        }
      })
  }
}
