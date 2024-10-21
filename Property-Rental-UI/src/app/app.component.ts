import { Component, OnInit } from '@angular/core';
import {MenuItem} from "primeng/api";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'part5AngularCouchAPI';
  items: MenuItem[] | undefined;

  ngOnInit() {
    this.items = [
      {
        label: 'Properties For Rent',
        icon: 'pi pi-fw pi-users',
        routerLink: ['/']
      },
      {
        label: 'Properties',
        icon: 'pi pi-fw pi-users',
        routerLink: ['/properties']
      },
      {
        label: 'Owners',
        icon: 'pi pi-fw pi-users',
        routerLink: ['/owners']
      },
      {
        label: 'Tenants',
        icon: 'pi pi-fw pi-users',
        routerLink: ['/tenants']
      },
      {
        label: 'Rentals',
        icon: 'pi pi-fw pi-users',
        routerLink: ['/rentals']
      }
    ]
  }
}
