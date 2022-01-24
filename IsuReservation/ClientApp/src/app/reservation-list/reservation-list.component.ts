import {Component, ViewChild} from '@angular/core';
import {Reservation} from "../models/reservation";
import {MatPaginator, PageEvent} from "@angular/material/paginator";
import {ReservationService} from "../services/reservation.service";
import {DestinationService} from "../services/destination.service";
import {MatTableDataSource} from "@angular/material/table";
import {NotificationsService} from "../services/notifications.service";
import {TranslateService} from "@ngx-translate/core";

@Component({
  selector: 'app-reservation-list',
  templateUrl: './reservation-list.component.html',
  styleUrls: ['./reservation-list.component.css']
})
export class ReservationListComponent {

  title: string = "Reservation List";
  subtitle: string = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam.";
  reservation_create_button: boolean = true;
  contact_list_button: boolean = true;
  columnsToDisplay = ['image', 'name', 'rating', 'favorite', 'actions'];
  totalRecords = 0;
  currentPage = 0;
  pageSize = 5;
  pageSizeOptions: number[] = [5, 10, 25, 100];
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
  ELEMENT_DATA: Reservation[] = [];
  dataSource: MatTableDataSource<Reservation> = new MatTableDataSource();
  isLoading = false;

  @ViewChild(MatPaginator)
  paginator!: MatPaginator;

  constructor(private reservationService: ReservationService, private destinationService: DestinationService,
              private notificationService: NotificationsService, private translateService: TranslateService) {
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  ngOnInit(): void {
    this.loadData();
  }

  pageChanged(event: PageEvent) {
    this.pageSize = event.pageSize;
    this.currentPage = event.pageIndex;
    this.loadData();
  }

  loadData() {
    this.isLoading = true;

    this.reservationService.getReservations(this.currentPage + 1, this.pageSize, this.sortBy, this.sortDesc)
      .subscribe(result => {
        this.dataSource.data = result.data.outcome;
        if (result.data.totalRecords > 0) {
          setTimeout(() => {
            this.paginator.pageIndex = this.currentPage;
            this.paginator.length = result.data.totalRecords;
            this.totalRecords = result.data.totalRecords;
          });
        }
        this.isLoading = false;
      }, error => {
        console.log(error);
        this.isLoading = false;
        if (error.error.errorDescription === undefined) {
          this.notificationService.error(this.translateService.instant('Something went wrong. Please try again later'));
        } else {
          this.notificationService.error(error.error.errorDescription);
        }
      })
  }

  sortChange(option: number) {
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

    let index = this.dataSource.data.findIndex(element => element.id == event.id);
    let reservation = this.dataSource.data[index];

    this.destinationService.ranking(reservation.destination.id, event.rating)
      .subscribe(result => {
        this.dataSource.data[index].destination = result.data;
        this.isLoading = false;
        this.notificationService.success(this.translateService.instant('Rate updated successfully'));
      }, error => {
        console.log(error);
        this.isLoading = false;
        if (error.error.errorDescription === undefined) {
          this.notificationService.error(this.translateService.instant('Something went wrong. Please try again later'));
        } else {
          this.notificationService.error(error.error.errorDescription);
        }
      })
  }

  setFavorite(id: string, reservationId: string) {
    this.isLoading = true;
    let index = this.dataSource.data.findIndex(element => element.id === reservationId);

    this.destinationService.favorite(id)
      .subscribe(result => {
        this.dataSource.data[index].destination = result.data;
        this.isLoading = false;
        if (result.data.favorite)
          this.notificationService.success(this.translateService.instant('Add to favorites successfully'));
        else
          this.notificationService.success(this.translateService.instant('Remove from favorites successfully'));
      }, error => {
        console.log(error);
        this.isLoading = false;
        if (error.error.errorDescription === undefined) {
          this.notificationService.error(this.translateService.instant('Something went wrong. Please try again later'));
        } else {
          this.notificationService.error(error.error.errorDescription);
        }
      })
  }
}
