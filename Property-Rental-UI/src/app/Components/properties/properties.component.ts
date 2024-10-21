import {Component, OnInit} from '@angular/core';
import {PropertiesService} from "../../Services/properties-service.service";
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {NgForOf, NgIf} from "@angular/common";
import {DialogModule} from "primeng/dialog";
import {TableModule} from "primeng/table";
import {Button, ButtonDirective} from "primeng/button";
import {InputTextModule} from "primeng/inputtext";
import {InputTextareaModule} from "primeng/inputtextarea";
import {Property} from "../../Models/Property.model";


@Component({
  selector: 'app-properties',
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
  ],
  templateUrl: './properties.component.html',
  styleUrl: './properties.component.css',
})
export class PropertiesComponent implements OnInit {
  properties: Property[] = [];
  editingProperty: Property | null = null;
  displayDialog: boolean = false; // Control dialog visibility
  propertyForm: FormGroup; // Reactive form for property details

  constructor(
    private propertiesService: PropertiesService,
    private fb: FormBuilder,
  ) {
    this.propertyForm = this.fb.group({
      // Initialize the form here
    });
  }

  ngOnInit() {
    this.getProperties();
    this.initializeForm(); // Initialize the form
  }

  // ... (other methods like getProperties, deleteProperty remain the same) ...

  initializeForm(): void {
    this.propertyForm = this.fb.group({
      propertiesId: [0], // Will be 0 for new properties
      ownerId: ['', Validators.required],
      status: ['AVAILABLE', Validators.required],
      propertyName: ['', Validators.required],
      rentalValue: [0, Validators.required],
      propertyType: ['', Validators.required],
      propertyDescription: [''],
      bedrooms: [0, Validators.required],
      bathrooms: [0, Validators.required],
      parkingSpaces: [0, Validators.required],
      features: [''],
    });
  }

  editProperty(property: Property): void {
    this.editingProperty = { ...property };
    this.propertyForm.patchValue(this.editingProperty);
    this.displayDialog = true;
  }

  saveProperty(): void {
    if (this.propertyForm.valid) {
      const propertyData = this.propertyForm.value;

      if (propertyData.propertiesId === 0) {
        // Create new property
        this.propertiesService.createProperty(propertyData).subscribe(() => {
          this.getProperties();
          this.displayDialog = false;
        });
      } else {
        // Update existing property
        this.propertiesService.updateProperty(propertyData).subscribe(() => {
          this.getProperties();
          this.displayDialog = false;
        });
      }

      this.propertyForm.reset(); // Clear the form
    } // You might want to add else block to handle invalid forms
  }

  cancelEdit(): void {
    this.displayDialog = false;
    this.propertyForm.reset();
  }

  openNewPropertyDialog(): void {
    this.editingProperty = null; // Ensure it's a new property
    this.propertyForm.reset(); // Reset the form to its defaults
    this.displayDialog = true;
  }

  deleteProperty(id: number): void {
    if (confirm('Are you sure you want to delete this property?')) {
      this.propertiesService.deleteProperty(id)
        .subscribe({
          next: () => {
            this.getProperties();
          },
          error: (error) => {
            console.error('Error deleting property:', error);
            // Handle the error (e.g., display an error message to the user)
          }
        });
    }
  }

  getProperties(): void {
    this.propertiesService
      .getProperties()
      .subscribe((properties) => (this.properties = properties));
  }
}
