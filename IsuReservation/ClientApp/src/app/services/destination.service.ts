import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {DestinationModel, DestinationSingleListModel} from "../models/destination.model";
import {TranslateService} from "@ngx-translate/core";

@Injectable({
  providedIn: 'root'
})
export class DestinationService {
  public baseUrl: string;

  constructor(private translateService: TranslateService, private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  favorite(destinationId: string): Observable<DestinationModel> {
    return this.httpClient.put<DestinationModel>(this.baseUrl + `api/destinations/favorite/${destinationId}`,
      {},
      {headers: {'App-Language': this.translateService.currentLang}}
    );
  }

  ranking(destinationId: string, ranking: number): Observable<DestinationModel> {
    return this.httpClient.put<DestinationModel>(this.baseUrl + `api/destinations/ranking/${destinationId}`,
      {ranking: ranking},
      {headers: {'App-Language': this.translateService.currentLang}}
    );
  }

  getDestinations(name: string): Observable<DestinationSingleListModel> {
    return this.httpClient.get<DestinationSingleListModel>(
      this.baseUrl + `api/destinations?name=${name}`,
      {headers: {'App-Language': this.translateService.currentLang}}
    );
  }
}
