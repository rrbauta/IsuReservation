import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {ReservationModel} from "../models/reservation.model";
import {MatPaginator} from "@angular/material/paginator";
import {ReservationService} from "../services/reservation.service";
import {map, switchMap} from "rxjs/operators";
import {DestinationService} from "../services/destination.service";

@Component({
  selector: 'app-reservation-list',
  templateUrl: './reservation-list.component.html',
  styleUrls: ['./reservation-list.component.css']
})
export class ReservationListComponent implements AfterViewInit {

  columnsToDisplay = ['image', 'name', 'rating', 'favorite', 'actions'];
  totalRecords = 0;
  reservations: ReservationModel[] = [];

  @ViewChild(MatPaginator) paginator?: MatPaginator;

  constructor(private reservationService: ReservationService, private destinationService: DestinationService) {
  }

  ngAfterViewInit(): void {
    this.pageChange();
    this.initialLoad();
  }

  initialLoad() {
    let currentPage = (this.paginator?.pageIndex ?? 0) + 1;
    this.reservationService.getReservations(currentPage, (this.paginator?.pageSize ?? 10))
      .subscribe(result => {
        this.totalRecords = result.data.totalRecords;
        this.reservations = result.data.outcome;
      })
  }

  pageChange() {
    this.paginator?.page.pipe(
      switchMap(() => {
        let currentPage = (this.paginator?.pageIndex ?? 0) + 1;
        return this.reservationService.getReservations(currentPage, (this.paginator?.pageSize ?? 0));
      }),
      map(result => {
        if (!result) {
          return [];
        }
        this.totalRecords = result.data.totalRecords;
        return result.data.outcome;
      })
    )
      .subscribe(data => {
        this.reservations = data;
      });
  }

  onRatingChanged(event: any) {
    console.log('my rating ' + event.id);

    this.destinationService.ranking(event.id, event.rating)
      .subscribe(result => {
        console.log(result);
      })
  }

  setFavorite(id: string, reservationId: string) {
    const index = this.reservations.findIndex(element => element.id === reservationId);

    console.log(index);
    this.destinationService.favorite(id)
      .subscribe(result => {
        this.reservations[index].destination = result.data;
        console.log(result.data);
      })
  }

}
