import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BackendService } from 'src/app/shared/services/backend.service';
import { CommentModelRequest } from '../models/request/CommentModelRequest';
import { CommentModelResponse } from '../models/response/CommentModelResponse';

@Injectable({
  providedIn: 'root',
})
export class CommentsService {
  constructor(private backendService: BackendService) {}

  getAllCommentsFromEvent(eventId: string): Observable<CommentModelResponse[]> {
    return this.backendService.GETRequest('Comment/Event/' + eventId);
  }

  editComment(
    id: string,
    request: CommentModelRequest
  ): Observable<CommentModelRequest> {
    return this.backendService.PUTRequest('Comment/' + id, request);
  }

  createComment(request: CommentModelRequest): Observable<CommentModelRequest> {
    return this.backendService.POSTRequest('Comment', request);
  }

  deleteComment(id: string): Observable<void> {
    return this.backendService.DELETERequest('Comment/' + id);
  }

  getCommentById(id: string): Observable<CommentModelResponse> {
    return this.backendService.GETRequest('Comment/' + id);
  }
}
