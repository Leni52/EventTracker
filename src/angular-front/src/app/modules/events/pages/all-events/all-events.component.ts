import { Component, OnInit } from '@angular/core';
import { EventModelResponse } from '../../models/response/EventModelResponse';
import { EventService } from '../../services/event.service';


@Component({
  selector: 'app-all-events',
  templateUrl: './all-events.component.html',
  styleUrls: ['./all-events.component.css']
})
export class AllEventsComponent implements OnInit {
  allEvents : EventModelResponse[] =[];
  constructor(public eventService: EventService) { }

  ngOnInit():void {
this.eventService.getAllEvents().subscribe((data: EventModelResponse[])=>{
  this.allEvents = data;
  console.log("Hello data");
 ()=> console.log(data);
})

}
deleteEvent(id:string) {
  this.eventService.delete(id).subscribe(res=>{
    this.allEvents = this.allEvents.filter(item=>item.id!==id);
  })
}


}
