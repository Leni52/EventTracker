import { Component, OnInit } from '@angular/core';
import { EventModel } from '../../../models/response/EventModel';
import { EventService } from '../../../services/event.service';

@Component({
  selector: 'app-all-events',
  templateUrl: './all-events.component.html',
  styleUrls: ['./all-events.component.css']
})
export class AllEventsComponent implements OnInit {
  allEvents : EventModel[] =[];
  constructor(public eventService: EventService) { }

  ngOnInit():void {
this.eventService.getAllEvents().subscribe((data: EventModel[])=>{
  this.allEvents = data;
 ()=> console.log(data);
})




  }




}
