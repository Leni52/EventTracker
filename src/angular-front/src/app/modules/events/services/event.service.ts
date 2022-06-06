import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EventModel } from '../models/response/EventModel';
import  { BackendService } from '../../../shared/services/backend.service';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})

export class EventService extends BackendService<EventModel, number>  {  

  constructor(httpClient: HttpClient) {
   // super(httpClient, `${environment.api.baseUrl}/Event`)
 super(httpClient,  'http://localhost:5021/api')
  }

  getAllEvents(){
  
   
  } 

  


  }



