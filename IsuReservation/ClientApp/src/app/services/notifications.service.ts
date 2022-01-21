import {Injectable} from '@angular/core';
import {MatSnackBar} from "@angular/material/snack-bar";

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {

  constructor(private snackBar: MatSnackBar) {
  }

  success(message: string) {
    return this.snackBar.open(message, '', {
      duration: 5000,
      panelClass: "my-notify-success"
    });
  }

  error(message: string) {
    return this.snackBar.open(message, '', {
      duration: 5000,
      panelClass: "my-notify-error"
    });
  }

  warning(message: string) {
    return this.snackBar.open(message, '', {
      duration: 5000,
      panelClass: "my-notify-warning"
    });
  }

  info(message: string) {
    return this.snackBar.open(message, '', {
      duration: 5000,
      panelClass: "my-notify-info"
    });
  }
}
