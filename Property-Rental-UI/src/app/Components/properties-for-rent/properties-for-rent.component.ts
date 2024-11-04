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
import {RentalService} from "../../Services/rentals-service.service";
import {Rental} from "../../Models/Rental.model";
import {RentalType} from "../../Enums/rental-type.enum";
import {CalendarModule} from "primeng/calendar";
import {KeyValuePipe, NgForOf} from "@angular/common";

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
    CalendarModule,
    KeyValuePipe,
    NgForOf,
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

  rentalFormData: Rental = {
    // Initialize with default values if needed
    rentalsId: null,
    tenantId: 0, // Or null, depending on your model
    propertyId: 0, // Or null
    rentalType: RentalType.STANDARD, // Or a default value
    rentalCost: 0,
    depositPaid: 0,
    additionalTenantIds: '',
    startDate: new Date(),
    endDate: new Date(),
  };

  showRentalFormDialog: boolean = false;

  constructor(
    private propertiesService: PropertiesService,
    private tenantService: TenantService,
    private rentalService: RentalService, // Inject RentalsService
  ) {}

  ngOnInit() {
    this.getProperties();
    console.log(this.properties);
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
    this.selectedTenant = event.value as Tenant;
    console.log('Selected Tenant:', this.selectedTenant);

    // After selecting a tenant, pre-fill the rental form data
    this.rentalFormData.tenantId = this.selectedTenant.tenantId;

    this.showTenantDialog = false; //Close the dialog for tenant selection
    this.showRentalFormDialog = true; // Open the rental form dialog
  }

  saveRental() {
    // Set propertyId based on selected property
    this.rentalFormData.propertyId = this.selectedProperty!.propertiesId;

    this.rentalService.createRental(this.rentalFormData).subscribe(
      (createdRental) => {
        console.log('Rental created successfully:', createdRental);

        // Optional: Reset form, navigate to another view, etc.
        this.rentalFormData = {
          // Initialize with default values if needed
          rentalsId: 0,
          tenantId: 0, // Or null, depending on your model
          propertyId: 0, // Or null
          rentalType: RentalType.STANDARD, // Or a default value
          rentalCost: 0,
          depositPaid: 0,
          additionalTenantIds: '',
          startDate: new Date(),
          endDate: new Date(),
        };

        this.showRentalFormDialog = false;
        this.ngOnInit();
      },
      (error) => {
        console.error('Error creating rental:', error);
        // Handle error
      },
    );
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

  protected readonly rentalTypes = RentalType;
}
