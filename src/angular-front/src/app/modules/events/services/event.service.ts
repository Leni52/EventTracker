import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EventModel } from '../models/response/EventModel';
import  { BackendService } from '../../../shared/services/backend.service';
import { Observable } from 'rxjs';
import { EventModelRequest } from '../models/request/EventModelRequest';

@Injectable({
  providedIn: 'root'
})

export class EventService extends BackendService<EventModel, string> {  
 httpOptions = {
   headers: new HttpHeaders({
     'Content-Type':'application/json',
     'Accept':'application/json'
   })
 };
private apiUrl = 'https://localhost:5021/api/';
  constructor(httpClient: HttpClient) {
   // super(httpClient, `${environment.api.baseUrl}/Event`)
 super(httpClient,  'https://localhost:5021/api')
  }

  getAllEvents(): Observable<EventModel[]>{
    return this.findAll();   
  } 

  getEvent(id: string): Observable<EventModel>{
    return this.findOne(id);
  }

  deleteEvent(id:string) :Observable<void>{
    return this.delete(id); 
  }

  createEvent(eventModel:EventModel): Observable<EventModel>{
    return this.create(eventModel);
  }
updateEvent(id: string, eventModel: EventModel):Observable<EventModel>{
  return this.update(id, eventModel);
}
  }



