package ie.mtu.property_rental.Entities;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Entity
@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder

// Class
public class Tenant {
    @Id
    @SequenceGenerator(name = "tenant_seq", sequenceName = "tenant_seq", allocationSize = 1)
    @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "tenant_seq")
    private Long tenantId;
    private String tenantName;
    private String tenantEmail;
    private String tenantPhone;
}