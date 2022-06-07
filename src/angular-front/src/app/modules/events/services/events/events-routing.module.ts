import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AllEventsComponent } from '../../pages/all-events/all-events/all-events.component';
import { CreateEventComponent } from '../../pages/create-event/create-event.component';


const routes: Routes = [
  {path: '', component: AllEventsComponent},
  {path: 'create', component: CreateEventComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EventsRoutingModule { }
