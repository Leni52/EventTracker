import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { AllEventsComponent } from './modules/events/pages/all-events/all-events/all-events.component';
@NgModule({
  declarations: [
    AppComponent,
   AllEventsComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule   
  
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
