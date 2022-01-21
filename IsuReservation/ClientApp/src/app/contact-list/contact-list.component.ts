import {Component, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {MatPaginator, PageEvent} from "@angular/material/paginator";
import {Contact} from "../models/contact.model";
import {ContactService} from "../services/contact.service";

@Component({
  selector: 'app-contact-list',
  templateUrl: './contact-list.component.html',
  styleUrls: ['./contact-list.component.css']
})
export class ContactListComponent implements OnInit {

  title: string = "Contact List";
  subtitle: string = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam.";
  reservation_list_button: boolean = true;
  contact_create_button: boolean = true;

  columnsToDisplay = ['name', 'contactTypeName', 'birthDate', 'phoneNumber', 'actions'];
  totalRecords = 0;
  currentPage = 0;
  pageSize = 5;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  sortBy: string = 'date';
  sortDesc: boolean = false;
  ELEMENT_DATA: Contact[] = [];
  dataSource: MatTableDataSource<Contact> = new MatTableDataSource();
  isLoading = false;

  @ViewChild(MatPaginator)
  paginator!: MatPaginator;

  constructor(private contactService: ContactService) {
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

    this.contactService.getContacts(this.currentPage + 1, this.pageSize, this.sortBy, this.sortDesc)
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
}
