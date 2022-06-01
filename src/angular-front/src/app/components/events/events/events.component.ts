import { Component, OnInit } from '@angular/core';
import { EventsService } from '../../../service/events.service';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit {
  events: Event[] = [];
  constructor(private eventsService: EventsService) { }

  ngOnInit(): void {
    this.eventsService.getAllEvents();
  }
  getAllEvents(){
    this.eventsService.getAllEvents()
    .subscribe(
      response=>{
        this.events = response;
        console.log(this.events);
      }
    )
  }
  
}
