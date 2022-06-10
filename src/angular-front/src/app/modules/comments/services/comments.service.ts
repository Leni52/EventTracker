import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BackendService } from 'src/app/shared/services/backend.service';
import { CommentModelRequest } from '../models/request/CommentModelRequest';

@Injectable({
  providedIn: 'root'
})
export class CommentsService {

  constructor(
    private backendService: BackendService
    ) { }

    getAllCommentsFromEvent(contextId: string): Observable<any>{
      return this.backendService.GETRequest('' +contextId);
    }

    editComment(request: CommentModelRequest):Observable<any>{
      return this.backendService.PUTRequest('', request);
    }
    createComment(request: CommentModelRequest):Observable<any>{
      return this.backendService.POSTRequest('', request);
    }
    deleteComment(id: string): Observable<any>{
      return this.backendService.DELETERequest('', id);
    }
    
}
