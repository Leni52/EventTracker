import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EventModel } from '../models/response/EventModel';
import  { BackendService } from '../../../shared/services/backend.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})



export class EventService extends BackendService<EventModel, string> {  
 httpOptions = {
   headers: new HttpHeaders({
     'Content-Type':'application/json'
   })
 };
private apiUrl = 'http://localhost:5021/api';
  constructor(httpClient: HttpClient) {
   // super(httpClient, `${environment.api.baseUrl}/Event`)
 super(httpClient,  'http://localhost:5021/api')
  }

  getAllEvents(): Observable<EventModel[]>{
  return this._http.get<EventModel[]>(this.apiUrl+'/Event');   
  } 
/*
  getEvent(id): Observable<EventModel>{
    return this._http.get<EventModel>(this.apiUrl+'/Event'+id);
  }
*/
  


  }



