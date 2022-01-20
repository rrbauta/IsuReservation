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
  sortBy: string = 'date';
  sortDesc: boolean = false;
  sortOption: number = 0;
  selectOptions = [
    {'text': 'Sort By', 'value': 0},
    {'text': 'By Date Ascending', 'value': 1},
    {'text': 'By Date Descending', 'value': 2},
    {'text': 'By Alphabetic Ascending', 'value': 3},
    {'text': 'By Alphabetic Descending', 'value': 4},
    {'text': 'By Ranking', 'value': 5}
  ];

  @ViewChild(MatPaginator) paginator?: MatPaginator;

  constructor(private reservationService: ReservationService, private destinationService: DestinationService) {
  }

  ngAfterViewInit(): void {
    this.pageChange();
    this.initialLoad();
  }

  sortChange(option: number) {
    console.log(option);
    switch (option) {
      case 1: {
        this.sortBy = 'date';
        this.sortDesc = false;
        break;
      }
      case 2: {
        this.sortBy = 'date';
        this.sortDesc = true;
        break;
      }
      case 3: {
        this.sortBy = 'alphabetic';
        this.sortDesc = false;
        break;
      }
      case 4: {
        this.sortBy = 'alphabetic';
        this.sortDesc = true;
        break;
      }
      case 5: {
        this.sortBy = 'ranking';
        this.sortDesc = true;
        break;
      }
      default: {
        this.sortBy = 'date';
        this.sortDesc = true;
        break;
      }
    }

    this.initialLoad();
  }

  initialLoad() {
    console.log(this.sortBy)
    console.log(this.sortDesc)
    let currentPage = (this.paginator?.pageIndex ?? 0) + 1;
    this.reservationService.getReservations(currentPage, (this.paginator?.pageSize ?? 10), this.sortBy, this.sortDesc)
      .subscribe(result => {
        this.totalRecords = result.data.totalRecords;
        this.reservations = result.data.outcome;
      })
  }

  pageChange() {
    this.paginator?.page.pipe(
      switchMap(() => {
        let currentPage = (this.paginator?.pageIndex ?? 0) + 1;
        return this.reservationService.getReservations(currentPage, (this.paginator?.pageSize ?? 10));
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
