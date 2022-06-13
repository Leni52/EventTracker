import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AllCommentsComponent } from '../../pages/all-comments/all-comments.component';
import { CreateCommentComponent } from '../../pages/create-comment/create-comment.component';

const routes: Routes = [
  {path: 'events/:id/comments', component: AllCommentsComponent},
  {path: 'createComment', component: CreateCommentComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CommentsRoutingModule { }
