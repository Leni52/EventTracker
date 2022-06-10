import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';


const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Accept':'application/json'
  }),
};


export class BackendService {

  constructor(
      private http: HttpClient
  ) { }

  GETRequest(requestTarget: string, responseType: any = 'json'): Observable<any> {
      return this.http.get(
          environment.api.backendApiUrl + requestTarget,
          { 
              observe: 'response',
              responseType: responseType
          }
      );
  }

  POSTRequest(requestTarget: string, requestData: any, responseType: any = 'json'): Observable<any> {
      return this.http.post(
          environment.api.backendApiUrl + requestTarget,
          requestData,
          {
              observe: 'response',
              responseType: responseType
          }
      );
  }

  PUTRequest(requestTarget: string, requestData: any, responseType: any = 'json'): Observable<any> {
      return this.http.put(
          environment.api.backendApiUrl + requestTarget,
          requestData,
          {
              observe: 'response',
              responseType: responseType
          }
      );
  }

  DELETERequest(requestTarget: string, responseType: any = 'json'): Observable<any> {
      return this.http.delete(
          environment.api.backendApiUrl + requestTarget,
          {
              observe: 'response',
              responseType: responseType
          }
      );
  }
}