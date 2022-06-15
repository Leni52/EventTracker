import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmationService } from 'src/app/shared/services/confirmation.service';
import { EventModelResponse } from '../../models/response/EventModelResponse';
import { EventService } from '../../services/event.service';

@Component({
  selector: 'app-edit-event',
  templateUrl: './edit-event.component.html',
  styleUrls: ['./edit-event.component.css'],
})
export class EditEventComponent implements OnInit {
  id!: string;
  event!: EventModelResponse;
  eventModels: EventModelResponse[] = [];
  editForm: FormGroup;
  result!: string;

  constructor(
    public eventService: EventService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder,
    private confirmationService: ConfirmationService
  ) {
    this.editForm = this.formBuilder.group({
      id: [''],
      name: ['', Validators.required],
      category: [''],
      description: ['', Validators.required],
      location: ['', Validators.required],
      startDate: [''],
      endDate: [''],
    });
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];

    this.eventService.getAllEvents().subscribe((data: EventModelResponse[]) => {
      this.eventModels = data;
    });

    this.eventService
      .getEvent(this.id)
      .subscribe((data: EventModelResponse) => {
        this.event = data;
        this.editForm.patchValue(data);
      });
  }

  onSubmit(formData: { value: EventModelResponse }) {
    this.confirmationService
      .confirmDialog({
        title: 'Please confirm action',
        message: 'Are you sure you want to update the event?',
        confirmText: 'Yes',
        cancelText: 'No',
      })
      .subscribe((result) => {
        if (result === true) {
          this.eventService
            .updateEvent(this.id, this.editForm.value)
            .subscribe((res) => {
              this.router.navigateByUrl('/events');
            });
        }
      });
  }
}
