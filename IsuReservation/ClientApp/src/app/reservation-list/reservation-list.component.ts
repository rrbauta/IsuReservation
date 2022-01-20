import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {ReservationModel} from "../models/reservation.model";
import {MatPaginator} from "@angular/material/paginator";
import {ReservationService} from "../services/reservation.service";
import {map, switchMap} from "rxjs/operators";

@Component({
  selector: 'app-reservation-list',
  templateUrl: './reservation-list.component.html',
  styleUrls: ['./reservation-list.component.css']
})
export class ReservationListComponent implements AfterViewInit {

  columnsToDisplay = ['image', 'name', 'rating', 'favorite', 'actions'];
  totalRecords = 0;
  reservations: ReservationModel[] = [];
  rating: number = 0;

  @ViewChild(MatPaginator) paginator?: MatPaginator;

  constructor(private reservationService: ReservationService) {
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

}
