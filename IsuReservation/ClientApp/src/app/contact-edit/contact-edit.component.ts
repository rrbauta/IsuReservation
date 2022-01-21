import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {ContactType} from "../models/contact-type.model";
import {NotificationsService} from "../services/notifications.service";
import {ContactService} from "../services/contact.service";
import {ActivatedRoute, Router} from "@angular/router";
import {Contact} from "../models/contact.model";

@Component({
  selector: 'app-contact-edit',
  templateUrl: './contact-edit.component.html',
  styleUrls: ['./contact-edit.component.css']
})
export class ContactEditComponent implements OnInit {

  title: string = "Edit Contact";
  subtitle: string = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam.";
  contact_list_button: boolean = true;
  myForm: FormGroup = this.fb.group({
    name: [''],
    phoneNumber: [''],
    birthDate: [''],
    contactType: ['']
  });
  isLoading: boolean = false;
  contact!: Contact;
  contactTypes: ContactType[] = [];
  contactId: string | null = ''

  constructor(private notificationService: NotificationsService, private contactService: ContactService, public fb: FormBuilder,
              private route: ActivatedRoute, private router: Router) {
  }

  ngOnInit(): void {
    this.contactId = this.route.snapshot.paramMap.get("id");
    this.getContact(this.route.snapshot.paramMap.get("id"));

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

  getContact(contactId: string | null) {
    this.isLoading = true;
    if (contactId != null) {
      this.contactService.getContact(contactId)
        .subscribe(result => {
          this.contact = result.data;

          this.myForm = this.fb.group({
            name: new FormControl(this.contact.name, Validators.required),
            phoneNumber: new FormControl(this.contact.phoneNumber, Validators.required),
            birthDate: new FormControl(new Date(this.contact.birthDate), Validators.required),
            contactType: new FormControl(this.contact.contactTypeId, Validators.required)
          });

          console.log(this.myForm.value)

          this.isLoading = false;
        }, error => {
          console.log(error);
          this.isLoading = false;
        })
    }
  }

  submitForm() {
    if (this.contactId != null) {
      this.isLoading = true;
      this.contactService.update(this.myForm.value, this.contactId)
        .subscribe(result => {
          if (result.isSuccess) {
            this.notificationService.success('Contact updated successfully');
            this.router.navigate(['/contacts'])
          } else {
            this.notificationService.error(result.exception);
          }
          this.isLoading = false;
        }, error => {
          this.isLoading = false;
          this.notificationService.error(error.error.errorDescription);
        })
    }
  }

}
