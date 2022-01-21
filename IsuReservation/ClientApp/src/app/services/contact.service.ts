import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {ContactPagingModel} from "../models/contact.paging.model";

@Injectable({
  providedIn: 'root'
})
export class ContactService {

  public baseUrl: string;

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getContacts(page: number = 1, recordsPerPage: number = 10, sortBy: string, sortDesc: boolean): Observable<ContactPagingModel> {
    return this.httpClient.get<ContactPagingModel>(this.baseUrl + `api/contacts?page=${page}&recordsPerPage=${recordsPerPage}&sortBy=${sortBy}&sortDesc=${sortDesc}`);
  }
}
