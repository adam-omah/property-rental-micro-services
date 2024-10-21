import {Component, OnInit} from '@angular/core';
import {Property} from "../../Models/Property.model";
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {PropertiesService} from "../../Services/properties-service.service";
import {ButtonDirective} from "primeng/button";
import {InputTextModule} from "primeng/inputtext";
import {InputTextareaModule} from "primeng/inputtextarea";
import {PrimeTemplate} from "primeng/api";
import {TableModule} from "primeng/table";
import {DialogModule} from "primeng/dialog";
import {TenantService} from "../../Services/tenants-service.service";
import {Tenant} from "../../Models/Tenant.model";
import {AutoCompleteModule, AutoCompleteSelectEvent} from "primeng/autocomplete";

@Component({
  selector: 'app-properties-for-rent',
  standalone: true,
  imports: [
    ButtonDirective,
    InputTextModule,
    InputTextareaModule,
    PrimeTemplate,
    TableModule,
    ReactiveFormsModule,
    DialogModule,
    FormsModule,
    AutoCompleteModule,
  ],
  templateUrl: './properties-for-rent.component.html',
  styleUrl: './properties-for-rent.component.css',
})
export class PropertiesForRentComponent implements OnInit {
  properties: Property[] = [];
  selectedProperty: Property | null = null;
  selectedTenant: Tenant | null = null;

  filteredTenants: Tenant[] = [];
  showTenantDialog: boolean = false;
  newTenant: Tenant = {
    // Model for the new tenant form
    tenantId: 0,
    tenantName: '',
    tenantEmail: '',
    tenantPhone: '',
  };

  constructor(
    private propertiesService: PropertiesService,
    private tenantService: TenantService,
  ) {}

  ngOnInit() {
    this.getProperties();
  }

  getProperties(): void {
    // Assuming you have a backend endpoint to fetch available properties
    this.propertiesService
      .getAvailableProperties()
      .subscribe((properties) => (this.properties = properties));
  }

  onRentProperty(property: Property) {
    this.selectedProperty = property;
    this.showTenantDialog = true;
  }

  filterTenants(event: any) {
    const query = event.query;
    this.tenantService.getTenants().subscribe((tenants) => {
      this.filteredTenants = tenants.filter((tenant) =>
        tenant.tenantEmail.toLowerCase().includes(query.toLowerCase()),
      );
    });
  }

  onTenantSelect(event: AutoCompleteSelectEvent) {
    // Access the selected tenant from the event object
    this.selectedTenant = event.value as Tenant;

    console.log("Selected Tenant:", this.selectedTenant);
    // Add logic here to handle tenant selection (e.g., navigate to rental form)
    this.showTenantDialog = false;
  }

  addNewTenant() {
    this.tenantService.createTenant(this.newTenant).subscribe(
      (createdTenant) => {
        console.log('Tenant created successfully:', createdTenant);
        this.filteredTenants.push(createdTenant); // Update the filtered list
        this.newTenant = {
          // Reset the form
          tenantId: 0,
          tenantName: '',
          tenantEmail: '',
          tenantPhone: '',
        };
      },
      (error) => {
        console.error('Error creating tenant:', error);
        // Handle error (e.g., display an error message)
      },
    );
  }
}
