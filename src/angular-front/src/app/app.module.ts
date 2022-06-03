import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { EventsModule } from './components/events/events.module';
import { FormsModule } from '@angular/forms';
import { ListEventsComponent } from './components/events/list/list-events/list-events.component';

@NgModule({
  declarations: [
    AppComponent,
ListEventsComponent
  
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    
  RouterModule.forRoot([
    { path: 'event', component: ListEventsComponent},
  ])],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
