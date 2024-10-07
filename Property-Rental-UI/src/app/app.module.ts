import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { InputTextModule } from "primeng/inputtext";
import { InputTextareaModule } from "primeng/inputtextarea";
import { InputNumberModule } from "primeng/inputnumber";
import { ButtonModule } from "primeng/button";
import { TableModule } from "primeng/table";
import { RadioButtonModule } from "primeng/radiobutton";
import { ConfirmationService } from "primeng/api";
import { AppComponent } from "./app.component";
import { IconFieldModule } from "primeng/iconfield";
import { InputIconModule } from "primeng/inputicon";
import {RouterOutlet} from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import {PanelModule} from 'primeng/panel';
import {MenubarModule} from 'primeng/menubar';
import {TabMenuModule} from 'primeng/tabmenu';
import {MenuModule} from 'primeng/menu';
import { AppRoutingModule } from './app.routes';
import {TenantsComponent} from "./Components/tenants/tenants.component";
import {OwnersComponent} from "./Components/owners/owners.component";
import {PropertiesComponent} from "./Components/properties/properties.component";
import {RentalsComponent} from "./Components/rentals/rentals.component";



@NgModule({
  declarations: [AppComponent],
  imports: [
    HttpClientModule,
    AppRoutingModule,
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    TableModule,
    InputTextModule,
    InputNumberModule,
    InputTextareaModule,
    RadioButtonModule,
    ButtonModule,
    IconFieldModule,
    InputIconModule,
    RouterOutlet,
    PanelModule,
    MenubarModule,
    TabMenuModule,
    MenuModule,
    TenantsComponent,
    OwnersComponent,
    PropertiesComponent,
    RentalsComponent
  ],
  providers: [ConfirmationService],
  bootstrap: [AppComponent],
})
export class AppModule {}
