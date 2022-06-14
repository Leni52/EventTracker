import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AllCommentsComponent } from '../../pages/all-comments/all-comments.component';
import { CreateCommentComponent } from '../../pages/create-comment/create-comment.component';
import { EditCommentComponent } from '../../pages/edit-comment/edit-comment.component';

const routes: Routes = [
  {path: 'events/:eventId/comments', component: AllCommentsComponent},
  {path: 'events/:eventId/comments/create', component: CreateCommentComponent},
  {path: 'events/:eventId/comments/:id/edit', component: EditCommentComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CommentsRoutingModule { }
