import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {ReservationPagingModel, ReservationResponse} from "../models/reservation";
import * as moment from "moment";
import {TranslateService} from "@ngx-translate/core";

@Injectable({
  providedIn: 'root'
})
export class ReservationService {

  public baseUrl: string;

  constructor(private translateService: TranslateService, private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getReservations(page: number = 1, recordsPerPage: number = 10, sortBy: string, sortDesc: boolean): Observable<ReservationPagingModel> {
    return this.httpClient.get<ReservationPagingModel>(
      this.baseUrl + `api/reservations?page=${page}&recordsPerPage=${recordsPerPage}&sortBy=${sortBy}&sortDesc=${sortDesc}`,
      {headers: {'App-Language': this.translateService.currentLang}}
    );
  }

  getReservation(reservationId: string): Observable<ReservationResponse> {
    return this.httpClient.get<ReservationResponse>(
      this.baseUrl + `api/reservations/${reservationId}`,
      {headers: {'App-Language': this.translateService.currentLang}}
    );
  }

  create(reservation: any): Observable<ReservationResponse> {
    return this.httpClient.post<ReservationResponse>(
      this.baseUrl + `api/reservations`,
      {
        description: reservation.description,
        date: moment(new Date(reservation.date)).format("MM/DD/YYYY HH:mm:ss"),
        contactName: reservation.contactName,
        contactPhoneNumber: reservation.contactPhoneNumber,
        contactBirthDate: moment(new Date(reservation.contactBirthDate)).format("MM/DD/YYYY"),
        contactTypeId: reservation.contactTypeId,
        contactId: reservation.contactId,
        destinationId: reservation.destinationId
      },
      {headers: {'App-Language': this.translateService.currentLang}}
    );
  }

  update(reservation: any, reservationId: string): Observable<ReservationResponse> {
    return this.httpClient.put<ReservationResponse>(
      this.baseUrl + `api/reservations/${reservationId}`,
      {
        description: reservation.description,
        date: moment(new Date(reservation.date)).format("MM/DD/YYYY HH:mm:ss"),
        contactName: reservation.contactName,
        contactPhoneNumber: reservation.contactPhoneNumber,
        contactBirthDate: moment(new Date(reservation.contactBirthDate)).format("MM/DD/YYYY"),
        contactTypeId: reservation.contactTypeId,
        contactId: reservation.contactId,
        destinationId: reservation.destinationId
      },
      {headers: {'App-Language': this.translateService.currentLang}}
    );
  }
}
