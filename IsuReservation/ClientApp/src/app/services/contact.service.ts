import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {ContactPagingModel} from "../models/contact.paging.model";
import {ContactResponse} from "../models/contact.model";
import * as moment from "moment";
import {ContactTypeResponse} from "../models/contact-type.model";
import {TranslateService} from "@ngx-translate/core";

@Injectable({
  providedIn: 'root'
})
export class ContactService {

  public baseUrl: string;

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string, private translateService: TranslateService) {
    this.baseUrl = baseUrl;
  }

  getContacts(page: number = 1, recordsPerPage: number = 10, sortBy: string, sortDesc: boolean, name: string = ''): Observable<ContactPagingModel> {
    return this.httpClient.get<ContactPagingModel>(
      this.baseUrl + `api/contacts?page=${page}&recordsPerPage=${recordsPerPage}&sortBy=${sortBy}&sortDesc=${sortDesc}&name=${name}`,
      {headers: {'App-Language': this.translateService.currentLang}}
    );
  }

  getContact(contactId: string): Observable<ContactResponse> {
    return this.httpClient.get<ContactResponse>(
      this.baseUrl + `api/contacts/${contactId}`,
      {headers: {'App-Language': this.translateService.currentLang}}
    );
  }

  getContactTypes(): Observable<ContactTypeResponse> {
    return this.httpClient.get<ContactTypeResponse>(
      this.baseUrl + `api/contacts/types`,
      {headers: {'App-Language': this.translateService.currentLang}}
    );
  }

  create(contact: any): Observable<ContactResponse> {
    return this.httpClient.post<ContactResponse>(
      this.baseUrl + `api/contacts`,
      {
        name: contact.name,
        birthDate: moment(new Date(contact.birthDate)).format("MM/DD/YYYY"),
        phoneNumber: contact.phoneNumber,
        contactTypeId: contact.contactType
      },
      {headers: {'App-Language': this.translateService.currentLang}}
    );
  }

  update(contact: any, contactId: string | null): Observable<ContactResponse> {
    return this.httpClient.put<ContactResponse>(
      this.baseUrl + `api/contacts/${contactId}`,
      {
        name: contact.name,
        birthDate: moment(new Date(contact.birthDate)).format("MM/DD/YYYY"),
        phoneNumber: contact.phoneNumber,
        contactTypeId: contact.contactType
      },
      {headers: {'App-Language': this.translateService.currentLang}}
    );
  }

  delete(contactId: string): Observable<ContactResponse> {
    return this.httpClient.delete<ContactResponse>(
      this.baseUrl + `api/contacts/${contactId}`,
      {headers: {'App-Language': this.translateService.currentLang}}
    );
  }
}
