import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {NgForOf, NgIf} from "@angular/common";
import {DialogModule} from "primeng/dialog";
import {TableModule} from "primeng/table";
import {Button, ButtonDirective} from "primeng/button";
import {InputTextModule} from "primeng/inputtext";
import {InputTextareaModule} from "primeng/inputtextarea";
import {Owner} from "../../Models/Owner.model";
import {OwnerService} from "../../Services/owners-service.service";

@Component({
  selector: 'app-owner',
  templateUrl: './owners.component.html',
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
  styleUrls: ['./owners.component.css'],
})
export class OwnersComponent implements OnInit {
  owners: Owner[] = [];
  editingOwner: Owner | null = null;
  displayDialog: boolean = false;
  ownerForm: FormGroup;

  constructor(
    private ownerService: OwnerService,
    private fb: FormBuilder,
  ) {
    this.ownerForm = this.fb.group({
      ownerId: [0],
      ownerName: ['', Validators.required],
      ownerEmail: ['', [Validators.required, Validators.email]],
      ownerAddress: ['', Validators.required],
      ownerPhone: ['', Validators.required],
      ownerIBAN: ['', Validators.required],
    });
  }

  ngOnInit() {
    this.getOwners();
  }

  getOwners(): void {
    this.ownerService.getOwners().subscribe((owners) => (this.owners = owners));
  }

  resetForm() {
    this.ownerForm.reset();
    this.editingOwner = null; // Reset selected owner
  }

  editOwner(owner: Owner): void {
    this.editingOwner = { ...owner };
    this.ownerForm.patchValue(this.editingOwner);
    this.displayDialog = true;
  }

  saveOwner(): void {
    if (this.ownerForm.valid) {
      const ownerData = this.ownerForm.value;

      if (ownerData.ownerId === 0) {
        // Create new owner
        this.ownerService.createOwner(ownerData).subscribe(() => {
          this.getOwners();
          this.displayDialog = false;
          this.resetForm();
        });
      } else {
        // Update existing owner
        this.ownerService.updateOwner(ownerData).subscribe(() => {
          this.getOwners();
          this.displayDialog = false;
          this.resetForm();
        });
      }
    }
  }


  deleteOwner(id: number): void {
    if (confirm('Are you sure you want to delete this owner?')) {
      this.ownerService.deleteOwner(id).subscribe({
        next: () => {
          this.getOwners();
        },
        error: (error) => {
          console.error('Error deleting owner:', error);
        },
      });
    }
  }
}
