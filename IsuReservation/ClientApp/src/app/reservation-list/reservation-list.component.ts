import {Component, ViewChild} from '@angular/core';
import {ReservationModel} from "../models/reservation.model";
import {MatPaginator, PageEvent} from "@angular/material/paginator";
import {ReservationService} from "../services/reservation.service";
import {DestinationService} from "../services/destination.service";
import {MatTableDataSource} from "@angular/material/table";

@Component({
  selector: 'app-reservation-list',
  templateUrl: './reservation-list.component.html',
  styleUrls: ['./reservation-list.component.css']
})
export class ReservationListComponent {

  columnsToDisplay = ['image', 'name', 'rating', 'favorite', 'actions'];
  totalRecords = 0;
  currentPage = 0;
  pageSize = 5;
  pageSizeOptions: number[] = [5, 10, 25, 100];
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
  ELEMENT_DATA: ReservationModel[] = [];
  dataSource: MatTableDataSource<ReservationModel> = new MatTableDataSource();
  isLoading = false;

  @ViewChild(MatPaginator)
  paginator!: MatPaginator;

  constructor(private reservationService: ReservationService, private destinationService: DestinationService) {
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  ngOnInit(): void {
    this.loadData();
  }

  pageChanged(event: PageEvent) {
    console.log({event});
    this.pageSize = event.pageSize;
    this.currentPage = event.pageIndex;
    this.loadData();
  }

  loadData() {
    this.isLoading = true;

    this.reservationService.getReservations(this.currentPage + 1, this.pageSize, this.sortBy, this.sortDesc)
      .subscribe(result => {
        this.dataSource.data = result.data.outcome;
        console.log(result)
        setTimeout(() => {
          this.paginator.pageIndex = this.currentPage;
          this.paginator.length = result.data.totalRecords;
          this.totalRecords = result.data.totalRecords;
        });
        this.isLoading = false;
      }, error => {
        console.log(error);
        this.isLoading = false;
      })
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
        this.sortDesc = false;
        break;
      }
    }

    this.loadData();
  }

  onRatingChanged(event: any) {
    this.isLoading = true;
    console.log('my rating ' + event.id);
    let index = this.dataSource.data.findIndex(element => element.id == event.id);
    let reservation = this.dataSource.data[index];

    this.destinationService.ranking(reservation.destination.id, event.rating)
      .subscribe(result => {
        this.dataSource.data[index].destination = result.data;
        console.log(result);
        this.isLoading = false;
      }, error => {
        console.log(error);
        this.isLoading = false;
      })
  }

  setFavorite(id: string, reservationId: string) {
    this.isLoading = true;
    let index = this.dataSource.data.findIndex(element => element.id === reservationId);

    console.log(index);
    this.destinationService.favorite(id)
      .subscribe(result => {
        this.dataSource.data[index].destination = result.data;
        console.log(result.data);
        this.isLoading = false;
      }, error => {
        console.log(error);
        this.isLoading = false;
      })
  }
}
