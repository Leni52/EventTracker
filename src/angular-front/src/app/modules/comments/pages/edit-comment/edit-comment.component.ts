import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmationService } from 'src/app/shared/services/confirmation.service';
import { CommentModelResponse } from '../../models/response/CommentModelResponse';
import { CommentsService } from '../../services/comments.service';

@Component({
  selector: 'app-edit-comment',
  templateUrl: './edit-comment.component.html',
  styleUrls: ['./edit-comment.component.css'],
})
export class EditCommentComponent implements OnInit {
  id!: string;
  eventId!: string;
  comment!: CommentModelResponse;
  result!: string;
  commentModels: CommentModelResponse[] = [];
  editCommentForm!: FormGroup;

  constructor(
    public commentService: CommentsService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder,
    private confirmationService: ConfirmationService
  ) {
    this.editCommentForm = this.formBuilder.group({
      id: [''],
      eventId: [''],
      text: [''],
    });
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.eventId = this.route.snapshot.params['eventId'];

    this.commentService
      .getAllCommentsFromEvent(this.eventId)
      .subscribe((data: CommentModelResponse[]) => {
        this.commentModels = data;
      });

    this.commentService
      .getCommentById(this.id)
      .subscribe((data: CommentModelResponse) => {
        this.comment = data;
        this.editCommentForm.patchValue(data);
      });
  }

  onSubmit(formData: { value: CommentModelResponse }) {
    this.confirmationService
      .confirmDialog({
        title: 'Please confirm action',
        message: 'Are you sure you want to update the comment?',
        confirmText: 'Yes',
        cancelText: 'No',
      })
      .subscribe((result) => {
        if (result === true) {
          this.commentService
            .editComment(this.id, formData.value)
            .subscribe((res) => {
              this.router.navigateByUrl(
                '/events/' + this.eventId + '/comments'
              );
            });
        }
      });
  }
}
