import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { ConfirmDialogModel } from '../models/confirm-model';
import { ConfirmationComponent } from '../pages/confirmation/confirmation.component';

@Injectable({
  providedIn: 'root',
})
export class ConfirmationService {
  x!: boolean;
  constructor(private dialog: MatDialog) {}

  confirmDialog(data: ConfirmDialogModel): Observable<boolean> {
    return this.dialog
      .open(ConfirmationComponent, {
        data,
        minWidth: '450px',
        disableClose: true,
      })
      .afterClosed();
  }
}
