import {Component, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {MatPaginator, PageEvent} from "@angular/material/paginator";
import {Contact} from "../models/contact.model";
import {ContactService} from "../services/contact.service";
import {MatDialog} from "@angular/material/dialog";
import {ConfirmationDialogComponent} from "../custom-components/confirmation-dialog/confirmation-dialog.component";
import {NotificationsService} from "../services/notifications.service";
import {Router} from "@angular/router";

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

  constructor(private notificationService: NotificationsService, private contactService: ContactService, private dialog: MatDialog,
              private router: Router) {
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

    this.contactService.getContacts(this.currentPage + 1, this.pageSize, this.sortBy, this.sortDesc)
      .subscribe(result => {
        this.dataSource.data = result.data.outcome;

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

  openDialog(contactId: string) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: {
        message: 'Are you sure want to delete this element?',
        buttonText: {
          ok: 'Yes',
          cancel: 'No'
        }
      }
    });

    dialogRef.afterClosed().subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.deleteContact(contactId);
        const a = document.createElement('a');
        a.click();
        a.remove();
      }
    });
  }

  deleteContact(contactId: string) {
    this.isLoading = true;
    this.contactService.delete(contactId)
      .subscribe(result => {
        if (result.isSuccess) {
          this.notificationService.success('Contact removed successfully');
          this.loadData();

        } else {
          this.notificationService.error(result.exception);
        }
      }, error => {
        console.log(error);
        this.isLoading = false;
        this.notificationService.error(error.error.errorDescription);
      })
  }

}
