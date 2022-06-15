import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommentModelResponse } from '../../models/response/CommentModelResponse';
import { CommentsService } from '../../services/comments.service';

@Component({
  selector: 'app-all-comments',
  templateUrl: './all-comments.component.html',
  styleUrls: ['./all-comments.component.css'],
})
export class AllCommentsComponent implements OnInit {
  allCommentsFromEvent: CommentModelResponse[] = [];
  eventId!: string;

  constructor(
    public commentService: CommentsService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.eventId = this.route.snapshot.params['eventId'];

    this.commentService
      .getAllCommentsFromEvent(this.eventId)
      .subscribe((data: CommentModelResponse[]) => {
        this.allCommentsFromEvent = data;
        console.log(data);
      });
  }

  deleteComment(id: string) {
    this.commentService.deleteComment(id).subscribe((res) => {
      this.allCommentsFromEvent = this.allCommentsFromEvent.filter(
        (item) => item.id !== id
      );
    });
  }
}
