import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EventModel } from '../../models/response/EventModel';
import { EventService } from '../../services/event.service';

@Component({
  selector: 'app-edit-event',
  templateUrl: './edit-event.component.html',
  styleUrls: ['./edit-event.component.css']
})
export class EditEventComponent implements OnInit {

  id!: string;
  event!: EventModel;
  
  eventModels: EventModel[]=[];
  editForm;
  constructor(
    public eventService: EventService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder
  ) { 
    this.editForm = this.formBuilder.group({
      id: [''],
    name:['',Validators.required],
    description: ['',Validators.required],
    location: ['',Validators.required],
    startDate:[''],
    endDate:['']

    })
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];

    this.eventService.getAllEvents().subscribe((data: EventModel[])=>{
      this.eventModels = data;     
    });

    this.eventService.getEvent(this.id).subscribe((data:EventModel)=>{
      this.event = data;
      this.editForm.patchValue(data);
    });
  }

  onSubmit(formData: { value: EventModel; }){
    this.eventService.update(this.id, formData.value).subscribe(res=>{
      this.router.navigateByUrl('/event');
    });
  }

}
