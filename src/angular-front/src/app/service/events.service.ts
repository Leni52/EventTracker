import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventsService {

  baseUrl = 'https://localhost:5021/api/event'
  constructor(private http: HttpClient) { }

  //Get all events

  getAllEvents():Observable<Event[]> {
   return this.http.get<Event[]>(this.baseUrl);

  }
 



}
