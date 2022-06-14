import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from '../../pages/about/about.component';
import { AllEventsComponent } from '../../pages/all-events/all-events.component';
import { CreateEventComponent } from '../../pages/create-event/create-event.component';
import { EditEventComponent } from '../../pages/edit-event/edit-event.component';

const routes: Routes = [
  {path: 'events', component: AllEventsComponent},
  {path: 'event/create', component: CreateEventComponent},
  {path: 'event/edit', component: EditEventComponent},
  {path: 'about', component:AboutComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EventsRoutingModule { }
