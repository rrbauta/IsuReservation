import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {ReservationPagingModel} from "../models/reservation.paging.model";

@Injectable({
  providedIn: 'root'
})
export class ReservationService {
  public reservations: ReservationPagingModel | undefined;

  public baseUrl: string;

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getReservations(page: number = 1, recordsPerPage: number = 10, sortBy: string = 'date', sortDesc: boolean = false): Observable<ReservationPagingModel> {
    return this.httpClient.get<ReservationPagingModel>(this.baseUrl + `api/reservations?page=${page}&recordsPerPage=${recordsPerPage}&sortBy=${sortBy}&sortDesc=${sortDesc}`);
  }
}
