import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { AllEventsComponent } from './modules/events/pages/all-events/all-events/all-events.component';
import { RouterModule } from '@angular/router';
import { CreateEventComponent } from './modules/events/pages/create-event/create-event.component';

@NgModule({
  declarations: [
    AppComponent,
   AllEventsComponent,
   CreateEventComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path: '', component: AllEventsComponent},
      {path: 'create', component: CreateEventComponent}
    ])   
  
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
