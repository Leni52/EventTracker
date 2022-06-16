import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EventModelResponse } from '../../models/response/EventModelResponse';
import { NgForm } from '@angular/forms';

import { EventService } from '../../services/event.service';
import { EventModelCreateRequest } from '../../models/request/EventModelCreateRequest';

@Component({
  selector: 'app-create-event',
  templateUrl: './create-event.component.html',
  styleUrls: ['./create-event.component.css']
})
export class CreateEventComponent implements OnInit {
  eventModels: EventModelResponse[] = [];

  defaultEvent: EventModelCreateRequest = {
    name: 'What is the event called',
    category: 1,
    location: 'Where will the event take place',
    description: 'Describe event',
    startDate: new Date(),
    endDate: new Date()
  };

  eventToSubmit: EventModelCreateRequest = { ...this.defaultEvent };

  constructor(
    public eventService: EventService,
    private router: Router
  ) { }   
 
  ngOnInit(): void {}

  onSubmit(form: NgForm): void{
    if (form.valid) {
      this.eventService.createEvent(this.eventToSubmit).subscribe(res => {
        this.router.navigateByUrl('/events');
      });
    } else {
      console.log("Form not valid.");
    }
  }
}
