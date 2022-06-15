import { Component, OnInit } from '@angular/core';
import { ConfirmationService } from 'src/app/shared/services/confirmation.service';
import { Category } from '../../models/enums/Category';
import { EventModelResponse } from '../../models/response/EventModelResponse';
import { EventService } from '../../services/event.service';

@Component({
  selector: 'app-all-events',
  templateUrl: './all-events.component.html',
  styleUrls: ['./all-events.component.css'],
})
export class AllEventsComponent implements OnInit {
  allEvents: EventModelResponse[] = [];
  categories: string[] = ['', 'Bussiness', 'IT', 'Software', 'Technology'];

  constructor(
    public eventService: EventService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit(): void {
    this.eventService.getAllEvents().subscribe((data: EventModelResponse[]) => {
      this.allEvents = data;
      console.log(typeof this.allEvents[0].category);
    });
  }

  openDialog(id: string) {
    this.confirmationService
      .confirmDialog({
        title: 'Please confirm action',
        message: 'Are you sure you want to delete the event?',
        confirmText: 'Yes',
        cancelText: 'No',
      })
      .subscribe((result: boolean) => {
        if (result === true) {
          this.eventService.deleteEvent(id).subscribe((res) => {
            this.allEvents = this.allEvents.filter((item) => item.id !== id);
          });
        }
      });
  }
}
