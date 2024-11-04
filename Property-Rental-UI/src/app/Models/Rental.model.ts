// rental.model.ts

import { RentalType } from '../Enums/rental-type.enum'; // Import RentalType if it's an enum

export interface Rental {
  rentalsId: number | null;
  tenantId: number;
  propertyId: number;
  rentalType: RentalType;
  rentalCost: number;
  depositPaid: number;
  additionalTenantIds: string;
  startDate: Date | null;
  endDate: Date | null;
}
