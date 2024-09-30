package ie.mtu.property_rental.Entities;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.sql.Date;

@Entity
@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder

// Class
public class Rentals {
    @Id
    @SequenceGenerator(name = "rentals_seq", sequenceName = "rentals_seq", allocationSize = 1)
    @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "rentals_seq")
    private Long rentalsId;
    private String tenantId;
    private String propertyId;
    private float rentalCost;
    private float depositPaid;
    private String additionalTenantIds;
    private Date startDate;
    private Date endDate;
}