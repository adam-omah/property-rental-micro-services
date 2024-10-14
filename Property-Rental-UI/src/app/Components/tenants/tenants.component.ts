import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {NgForOf, NgIf} from "@angular/common";
import {DialogModule} from "primeng/dialog";
import {TableModule} from "primeng/table";
import {Button, ButtonDirective} from "primeng/button";
import {InputTextModule} from "primeng/inputtext";
import {InputTextareaModule} from "primeng/inputtextarea";
import {Tenant} from "../../Models/Tenant.model";
import {TenantService} from "../../Services/tenants-service.service";

@Component({
  selector: 'app-tenant',
  templateUrl: './tenants.component.html',
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
  styleUrls: ['./tenants.component.css']
})
export class TenantsComponent implements OnInit {
  tenants: Tenant[] = [];
  editingTenant: Tenant | null = null;
  displayDialog: boolean = false;
  tenantForm: FormGroup;

  constructor(
    private tenantService: TenantService,
    private fb: FormBuilder
  ) {
    this.tenantForm = this.fb.group({
      tenantId: [0],
      tenantName: ['', Validators.required],
      tenantEmail: ['', [Validators.required, Validators.email]],
      tenantPhone: ['', Validators.required]
    });
  }

  ngOnInit() {
    this.getTenants();
  }

  getTenants(): void {
    this.tenantService.getTenants()
      .subscribe(tenants => this.tenants = tenants);
  }

  editTenant(tenant: Tenant): void {
    this.editingTenant = { ...tenant };
    this.tenantForm.patchValue(this.editingTenant);
    this.displayDialog = true;
  }

  saveTenant(): void {
    if (this.tenantForm.valid) {
      const tenantData = this.tenantForm.value;

      if (tenantData.tenantId === 0) {
        this.tenantService.createTenant(tenantData).subscribe(() => {
          this.getTenants();
          this.displayDialog = false;
          this.tenantForm.reset();
        });
      } else {
        this.tenantService.updateTenant(tenantData).subscribe(() => {
          this.getTenants();
          this.displayDialog = false;
          this.tenantForm.reset();
        });
      }
    }
  }

  cancelEdit(): void {
    this.displayDialog = false;
    this.tenantForm.reset();
  }

  openNewTenantDialog(): void {
    this.editingTenant = null;
    this.tenantForm.reset();
    this.displayDialog = true;
  }

  deleteTenant(id: number): void {
    if (confirm('Are you sure you want to delete this tenant?')) {
      this.tenantService.deleteTenant(id).subscribe({
        next: () => {
          this.getTenants();
        },
        error: (error) => {
          console.error('Error deleting tenant:', error);
          // Handle the error appropriately
        }
      });
    }
  }
}
