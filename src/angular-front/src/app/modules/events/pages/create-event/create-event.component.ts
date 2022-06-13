import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EventModelResponse } from '../../models/response/EventModelResponse';

import { EventService } from '../../services/event.service';
import { Category } from '../../models/enums/Category'; 
import { EventModelCreateRequest } from '../../models/request/EventModelCreateRequest';

@Component({
  selector: 'app-create-event',
  templateUrl: './create-event.component.html',
  styleUrls: ['./create-event.component.css']
})
export class CreateEventComponent implements OnInit {
  eventModels: EventModelResponse[] = [];
  public StateEnum = Category;
  public InitCategory = Category.Business;
  public categoryTypes = Object.values(Category);

  createForm: FormGroup;

  constructor(
    public eventService: EventService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder
  ) {
      this.createForm = this.formBuilder.group({
        name: ['', Validators.required],
        category: [''],
        description: ['',Validators.required],
        location: ['',Validators.required],
        startDate: [''],
        endDate: ['']
      })    
  }   
 
  ngOnInit(): void {
    console.log(this.categoryTypes.keys);
    this.eventService.getAllEvents().subscribe((data: EventModelResponse[]) => {
      this.eventModels = data;
    });
  }

  onSubmit(formData: { value: EventModelCreateRequest; }): void{
    this.eventService.createEvent(formData.value).subscribe(res => {
      this.router.navigateByUrl('/events');
    })
  }
}
