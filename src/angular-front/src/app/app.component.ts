import { Component, OnInit } from '@angular/core';
import { EventsService } from './service/events.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})


export class AppComponent implements OnInit{
 title= 'events';

  constructor(private eventsService: EventsService){


  }

  ngOnInit(): void {
    this.getAllEvents();
  }

  getAllEvents(){
    this.eventsService.getAllEvents()
      .subscribe(
        response=>{
         console.log(response);
          console.log("hello from appcomponents")
        });
  
  }
}
