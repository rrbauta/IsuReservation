import {Inject, Injectable} from '@angular/core';
import {DestinationModel} from "../models/reservation.paging.model";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class DestinationService {
  public baseUrl: string;

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  favorite(destinationId: string): Observable<DestinationModel> {
    return this.httpClient.put<DestinationModel>(this.baseUrl + `api/destinations/favorite/${destinationId}`, {});
  }

  ranking(destinationId: string, ranking: number): Observable<DestinationModel> {
    return this.httpClient.put<DestinationModel>(this.baseUrl + `api/destinations/ranking/${destinationId}`, {ranking: ranking});
  }
}
