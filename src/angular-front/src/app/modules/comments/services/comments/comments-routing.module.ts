import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AllCommentsComponent } from '../../pages/all-comments/all-comments.component';

const routes: Routes = [
  {path: 'Comment/Event/:eventId', component: AllCommentsComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CommentsRoutingModule { }
