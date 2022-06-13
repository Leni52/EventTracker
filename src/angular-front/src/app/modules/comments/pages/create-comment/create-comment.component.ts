import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommentModelRequest } from '../../models/request/CommentModelRequest';
import { CommentsService } from '../../services/comments.service';

@Component({
  selector: 'app-create-comment',
  templateUrl: './create-comment.component.html',
  styleUrls: ['./create-comment.component.css']
})
export class CreateCommentComponent implements OnInit {
  createForm: FormGroup;
  eventId!: string;

  constructor(
    public commentService: CommentsService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder
    ) {
      this.createForm = this.formBuilder.group({
        eventId: [this.eventId],
        text: ['']
      })
    }

  ngOnInit(): void {
    this.eventId = this.route.snapshot.params['eventId'];
  }

  onSubmit(formData: { value: CommentModelRequest}): void {
    this.commentService.createComment(formData.value).subscribe(res => {
      this.router.navigateByUrl('/event');
    })
  }
}
