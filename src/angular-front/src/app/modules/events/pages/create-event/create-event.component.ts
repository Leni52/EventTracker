import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EventModelRequest } from '../../models/request/EventModelRequest';
import { EventModel } from '../../models/response/EventModel';

import { EventService } from '../../services/event.service';


@Component({
  selector: 'app-create-event',
  templateUrl: './create-event.component.html',
  styleUrls: ['./create-event.component.css']
})
export class CreateEventComponent implements OnInit {
eventModels: EventModelRequest[] =[];
createForm;
  constructor(
    public eventService: EventService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder
  ) {
    this.createForm = this.formBuilder.group({
      name: [''],
      category: [''],
      description: [''],
      location: [''],
      startDate: [''],
      endDate: ['']
    })
   }

  ngOnInit(): void {
    this.eventService.getAllEvents().subscribe((data: EventModelRequest[])=>{
      this.eventModels = data;
    });
    
  }

  onSubmit(formData: { value: EventModel; }){
    this.eventService.createEvent(formData.value).subscribe(res=>{
      this.router.navigateByUrl('/event');
    })
  }
}
