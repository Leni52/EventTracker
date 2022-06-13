import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AllEventsComponent } from './modules/events/pages/all-events/all-events.component';
import { CreateEventComponent } from './modules/events/pages/create-event/create-event.component';
import { EditEventComponent } from './modules/events/pages/edit-event/edit-event.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HeaderComponent } from './modules/events/pages/header/header.component';
import { FooterComponent } from './modules/events/pages/footer/footer.component';
import { AboutComponent } from './modules/events/pages/about/about.component';
import { NavigationBarComponent } from './modules/events/pages/navigation-bar/navigation-bar.component';
import { AllCommentsComponent } from './modules/comments/pages/all-comments/all-comments.component';
import { CreateCommentComponent } from './modules/comments/pages/create-comment/create-comment.component';

@NgModule({
  declarations: [
    AppComponent,
   AllEventsComponent,
   CreateEventComponent,
   EditEventComponent,
   HeaderComponent,
   FooterComponent,
   AboutComponent,
   NavigationBarComponent,
   AllCommentsComponent,
   CreateCommentComponent,

  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      {path: 'events', component: AllEventsComponent, pathMatch:'full'},
      {path: 'create', component: CreateEventComponent, pathMatch:'full'},
      {path: 'edit/:id', component: EditEventComponent, pathMatch:'full'},
      {path: 'about', component: AboutComponent},
      {path: 'comment/event/:eventId', component:AllCommentsComponent},
      {path: 'comment/:eventId', component:AllCommentsComponent},
      {path: 'createComment', component: CreateCommentComponent}
    ])   
  
  ],
  exports:[RouterModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }