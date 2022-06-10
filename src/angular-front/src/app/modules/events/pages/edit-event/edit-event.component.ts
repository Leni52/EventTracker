import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EventModelResponse } from '../../models/response/EventModelResponse';
import { EventService } from '../../services/event.service';

@Component({
  selector: 'app-edit-event',
  templateUrl: './edit-event.component.html',
  styleUrls: ['./edit-event.component.css']
})
export class EditEventComponent implements OnInit {

  id!: string;
  event!: EventModelResponse;
  
  eventModels: EventModelResponse[]=[];
  editForm: FormGroup;
  
  constructor(
    public eventService: EventService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder
  ) { 
    this.editForm = this.formBuilder.group({
    id: [''],
    name:['',Validators.required],
    category:[''],
    description: ['',Validators.required],
    location: ['',Validators.required],
    startDate:[''],
    endDate:['']

    })
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];

    this.eventService.getAllEvents().subscribe((data: EventModelResponse[])=>{
      this.eventModels = data;     
    });

    this.eventService.getEvent(this.id).subscribe((data:EventModelResponse)=>{
      this.event = data;
      this.editForm.patchValue(data);
    });
  }

  onSubmit(formData: { value: EventModelResponse; }){
    this.eventService.updateEvent(this.id, formData.value).subscribe(res=>{
      this.router.navigateByUrl('');
    });
  }

}
