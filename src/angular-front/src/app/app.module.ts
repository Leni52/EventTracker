import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
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
import { EditCommentComponent } from './modules/comments/pages/edit-comment/edit-comment.component';
import { ConfirmationComponent } from './shared/pages/confirmation/confirmation.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoaderComponent } from './shared/pages/loader/loader.component';
import {MatProgressSpinner, MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { LoaderInterceptor } from './shared/interceptors/loader.interceptor';
import {MatCardModule} from '@angular/material/card';

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
    EditCommentComponent,
    ConfirmationComponent,
    LoaderComponent,
   
  ],
  imports: [
   MatCardModule,
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: 'events', component: AllEventsComponent, pathMatch: 'full' },
      {
        path: 'events/create',
        component: CreateEventComponent,
        pathMatch: 'full',
      },
      {
        path: 'events/:id/edit',
        component: EditEventComponent,
        pathMatch: 'full',
      },
      { path: 'about', component: AboutComponent },
      { path: 'events/:eventId/comments', component: AllCommentsComponent },
      {
        path: 'events/:eventId/comments/create',
        component: CreateCommentComponent,
      },
      {
        path: 'events/:eventId/comments/:id/edit',
        component: EditCommentComponent,
      },
    ]),
    MatDialogModule,
    MatIconModule,
    MatButtonModule,
    MatToolbarModule,
    BrowserAnimationsModule,
    MatProgressSpinnerModule
  ],
  exports: [RouterModule],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoaderInterceptor,
      multi:true,
    }
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
