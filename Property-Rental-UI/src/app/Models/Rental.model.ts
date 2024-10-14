// rental.model.ts

import { RentalType } from '../Enums/rental-type.enum'; // Import RentalType if it's an enum

export interface Rental {
  rentalsId: number;
  tenantId: string;
  propertyId: string;
  rentalType: RentalType;
  rentalCost: number;
  depositPaid: number;
  additionalTenantIds: string;
  startDate: Date;
  endDate: Date;
}
