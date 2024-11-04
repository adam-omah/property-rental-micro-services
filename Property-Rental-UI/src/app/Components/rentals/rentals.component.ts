// rental.component.ts

import { Component, OnInit } from '@angular/core';
import { RentalService } from '../../Services/rentals-service.service';
import {FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import { Rental } from '../../Models/Rental.model';
import { RentalType } from '../../Enums/rental-type.enum';
import {DatePipe, KeyValuePipe, NgForOf, NgIf} from "@angular/common";
import {DialogModule} from "primeng/dialog";
import {TableModule} from "primeng/table";
import {Button, ButtonDirective} from "primeng/button";
import {InputTextModule} from "primeng/inputtext";
import {InputTextareaModule} from "primeng/inputtextarea";
import {CalendarModule} from "primeng/calendar";
import {Tenant} from "../../Models/Tenant.model";
import {TenantService} from "../../Services/tenants-service.service";
import {AutoCompleteModule, AutoCompleteSelectEvent} from "primeng/autocomplete";
import {PropertiesService} from "../../Services/properties-service.service";
import {Property} from "../../Models/Property.model";

@Component({
  selector: 'app-rental',
  templateUrl: './rentals.component.html',
  standalone: true,
  imports: [
    FormsModule,
    NgIf,
    NgForOf,
    DialogModule,
    ReactiveFormsModule,
    TableModule,
    Button,
    InputTextModule,
    InputTextareaModule,
    ButtonDirective,
    KeyValuePipe,
    DatePipe,
    CalendarModule,
    AutoCompleteModule,
  ],
  styleUrls: ['./rentals.component.css'],
})
export class RentalsComponent implements OnInit {
  rentals: Rental[] = [];
  filteredTenants: Tenant[] = [];
  filteredProperties: Property[] = [];
  editingRental: Rental | null = null;
  displayDialog: boolean = false;
  rentalForm: FormGroup;
  rentalTypes = RentalType;

  temporaryTenantEmail = new FormControl('');
  temporaryPropertyEircode = new FormControl('');

  constructor(
    private rentalService: RentalService,
    private fb: FormBuilder,
    private tenantService: TenantService,
    private propertyService: PropertiesService
  ) {
    this.rentalForm = this.fb.group({
      rentalsId: [0],
      tenantId: [0, Validators.required],
      propertyId: [0, Validators.required],
      rentalType: [RentalType.STANDARD, Validators.required],
      rentalCost: [0, Validators.required],
      depositPaid: [0, Validators.required],
      additionalTenantIds: [''],
      startDate: [null, Validators.required],
      endDate: [null, Validators.required],
    });
  }

  ngOnInit() {
    this.getRentals();
  }

  getRentals(): void {
    this.rentalService
      .getRentals()
      .subscribe((rentals) => (this.rentals = rentals));
  }

  editRental(rental: Rental): void {
    this.editingRental = { ...rental };
    this.rentalForm.patchValue(this.editingRental);
    this.displayDialog = true;
  }

  saveRental(): void {
    if (this.rentalForm.valid) {
      const rentalData = this.rentalForm.value;

      if (rentalData.rentalsId === 0) {
        // Create new rental
        console.log(this.rentalForm.value);
        this.rentalService.createRental(rentalData).subscribe(() => {
          this.getRentals();
          this.displayDialog = false;
          this.rentalForm.reset();
        });
      } else {
        // Update existing rental
        this.rentalService.updateRental(rentalData).subscribe(() => {
          this.getRentals();
          this.displayDialog = false;
          this.rentalForm.reset();
        });
      }
    }
  }

  deleteRental(id: number): void {
    if (confirm('Are you sure you want to delete this rental?')) {
      this.rentalService.deleteRental(id).subscribe({
        next: () => {
          this.getRentals();
        },
        error: (error) => {
          console.error('Error deleting rental:', error);
          // Handle error (e.g., display error message)
        },
      });
    }
  }

  filterTenants(event: any) {
    const query = event.query; // Get the search query from the input

    this.tenantService.getTenants().subscribe((tenants) => {
      this.filteredTenants = tenants.filter((tenant) =>
        tenant.tenantEmail.toLowerCase().includes(query.toLowerCase()),
      );
    });
  }

  onTenantSelect(event: AutoCompleteSelectEvent) {
    this.rentalForm.patchValue({ tenantId: event.value.tenantId }); // Access tenantId from event.value
  }

  filterProperties(event: any) {
    const query = event.query;

    this.propertyService.getProperties().subscribe(properties => {
      this.filteredProperties = properties.filter(property =>
        property.propertyEircode.toLowerCase().includes(query.toLowerCase())
      );
    });
  }

  onPropertySelect(event: AutoCompleteSelectEvent) {
    this.rentalForm.patchValue({ propertyId: event.value.propertiesId }); // Access from event.value
  }
}
