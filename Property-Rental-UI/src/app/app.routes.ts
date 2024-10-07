import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {AppComponent} from './app.component';
import {PropertiesComponent} from "./Components/properties/properties.component";
import {RentalsComponent} from "./Components/rentals/rentals.component";
import {TenantsComponent} from "./Components/tenants/tenants.component";
import {OwnersComponent} from "./Components/owners/owners.component";

export const routes: Routes = [
  { path: '', component: PropertiesComponent  },
  { path: 'properties', component: PropertiesComponent },
  { path: 'owners', component: OwnersComponent },
  { path: 'tenants', component: TenantsComponent },
  { path: 'rentals', component: RentalsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
