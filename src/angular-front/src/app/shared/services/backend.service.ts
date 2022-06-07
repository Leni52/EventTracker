import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { CrudOperations } from './backend-service.interface';


 export abstract class BackendService<T, ID> implements CrudOperations<T, ID>{

  constructor(
    protected _http: HttpClient,
    protected _base: string
  ){}
  create(t: T): Observable<T> {
    return this._http.post<T>(`${this._base}`+'/event', JSON.stringify(t));
  }

  update(id: ID, t: T): Observable<T> {
    return this._http.put<T>(this._base + "/event" + id, t, {});
  }

  findOne(id: ID): Observable<T> {
    return this._http.get<T>(this._base + "/event/" + id);
  }

  findAll(): Observable<T[]> {
    return this._http.get<T[]>(`${this._base}`+'/event');
  }

  delete(id: ID): Observable<void> {
    return this._http.delete<void>(this._base + '/event/' + id);
  }

  




 
}
