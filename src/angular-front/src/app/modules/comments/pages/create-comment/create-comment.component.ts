import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CommentModelRequest } from '../../models/request/CommentModelRequest';
import { CommentsService } from '../../services/comments.service';

@Component({
  selector: 'app-create-comment',
  templateUrl: './create-comment.component.html',
  styleUrls: ['./create-comment.component.css']
})
export class CreateCommentComponent implements OnInit {
  createForm: FormGroup;

  constructor(
    public commentService: CommentsService,
    private router: Router,
    private formBuilder: FormBuilder
    ) {
      this.createForm = this.formBuilder.group({
        eventId: ['', Validators.required],
        text: ['']
      })
    }

  ngOnInit(): void {
  }

  onSubmit(formData: { value: CommentModelRequest}): void {
    this.commentService.createComment(formData.value).subscribe(res => {
      this.router.navigateByUrl('/events');
    })
  }
}
