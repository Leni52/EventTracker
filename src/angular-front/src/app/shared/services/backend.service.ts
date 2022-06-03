import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class BackendService {

  private baseUrl = 'http://localhost:5021/Event';

  constructor(private httpClient: HttpClient) { }

  GetRequest(){

  }
}
