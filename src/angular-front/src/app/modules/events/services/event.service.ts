import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EventModelResponse } from '../models/response/EventModelResponse';
import  { BackendService } from '../../../shared/services/backend.service';
import { Observable } from 'rxjs';
import { EventModelCreateRequest } from '../models/request/EventModelCreateRequest';

@Injectable({
  providedIn: 'root'
})

export class EventService extends BackendService<EventModelResponse, string> {  
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

  getAllEvents(): Observable<EventModelResponse[]>{
    return this.findAll();   
  } 

  getEvent(id: string): Observable<EventModelResponse>{
    return this.findOne(id);
  }

  deleteEvent(id:string) :Observable<void>{
    return this.delete(id); 
  }

  createEvent(eventModel:EventModelResponse): Observable<EventModelResponse>{
    console.log(eventModel);
    return this.create(eventModel);
  }
updateEvent(id: string, eventModel: EventModelResponse):Observable<EventModelResponse>{
  return this.update(id, eventModel);
}
  }



