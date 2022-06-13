import { Injectable } from '@angular/core';
import { EventModelResponse } from '../models/response/EventModelResponse';
import { BackendService } from '../../../shared/services/backend.service';
import { Observable, map } from 'rxjs';
import { EventModelCreateRequest } from '../models/request/EventModelCreateRequest';

@Injectable({
  providedIn: 'root'
})
export class EventService {  

  constructor(private backendService: BackendService) {}

  getAllEvents(): Observable<EventModelResponse[]> {
    return this.backendService.GETRequest('Event'); 
  } 

  getEvent(id: string): Observable<EventModelResponse> {
    return this.backendService.GETRequest('Event/' + id);
  }

  deleteEvent(id: string): Observable<void>{
    return this.backendService.DELETERequest('Event/' + id);
  }

  createEvent(eventModel: EventModelCreateRequest): Observable<EventModelCreateRequest> {
    return this.backendService.POSTRequest('Event', eventModel);
  }

  updateEvent(id: string, eventModel: EventModelCreateRequest):Observable<EventModelCreateRequest> {
    return this.backendService.PUTRequest('Event/' + id, eventModel);
  }
}