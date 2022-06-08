import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EventModel } from '../../models/response/EventModel';

import { EventService } from '../../services/event.service';
import { Category } from '../../models/response/Category'; 

@Component({
  selector: 'app-create-event',
  templateUrl: './create-event.component.html',
  styleUrls: ['./create-event.component.css']
})
export class CreateEventComponent implements OnInit {
eventModels: EventModel[] =[];
public StateEnum = Category;
public InitCategory = Category.Business;
public categoryTypes = Object.values(Category);
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
    this.eventService.getAllEvents().subscribe((data: EventModel[])=>{
      this.eventModels = data;
    });
    
  }

  onSubmit(formData: { value: EventModel; }){
    this.eventService.createEvent(formData.value).subscribe(res=>{
      this.router.navigateByUrl('/event');
    })
  }
}
