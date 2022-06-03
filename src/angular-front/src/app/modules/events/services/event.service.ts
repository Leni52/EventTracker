import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EventModel } from '../models/response/EventModel';
import  { BackendService } from '../../../shared/services/backend.service';
@Injectable({
  providedIn: 'root'
})

export class EventService extends BackendService  {  

  constructor(httpClient: HttpClient) {
    super(httpClient)
  }

  getAllEvents(){

  }


  }



