import { Component, OnInit } from '@angular/core';
import { Category } from '../../models/enums/Category';
import { EventModelResponse } from '../../models/response/EventModelResponse';
import { EventService } from '../../services/event.service';

@Component({
  selector: 'app-all-events',
  templateUrl: './all-events.component.html',
  styleUrls: ['./all-events.component.css']
})
export class AllEventsComponent implements OnInit {
  allEvents : EventModelResponse[] = [];
  categories: string[] = ['', 'Bussiness', 'IT', 'Software', 'Technology'];

  constructor(public eventService: EventService) { }
  
  ngOnInit(): void {
    this.eventService.getAllEvents().subscribe((data: EventModelResponse[]) => {
      this.allEvents = data;
      console.log(typeof this.allEvents[0].category);
    })
  }

  deleteEvent(id: string) {
    this.eventService.deleteEvent(id).subscribe(res => {
      this.allEvents = this.allEvents.filter( item => item.id !== id);
    })
  }
}
