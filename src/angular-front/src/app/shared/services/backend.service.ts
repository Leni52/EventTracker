import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { CrudOperations } from './backend-service.interface';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Accept':'application/json'
  }),
};


 export abstract class BackendService<T, ID> implements CrudOperations<T, ID>{

  constructor(
    protected _http: HttpClient,
    protected _base: string
  ){}
  create(t: T): Observable<T> {
    return this._http.post<T>(this._base+'/event', JSON.stringify(t), httpOptions);
  }

  update(id: ID, t: T): Observable<T> {
    return this._http.put<T>(this._base + '/event/' + id, JSON.stringify(t), httpOptions);
  }

  findOne(id: ID): Observable<T> {
    return this._http.get<T>(this._base + '/event/' + id);
  }

  findAll(): Observable<T[]> {
    return this._http.get<T[]>(this._base+'/event');
  }

  delete(id: ID): Observable<void> {
    return this._http.delete<void>(this._base + '/event/' + id);
  }

  




 
}
