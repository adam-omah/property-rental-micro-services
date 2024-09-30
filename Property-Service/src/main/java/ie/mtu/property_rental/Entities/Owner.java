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
public class Owner {
    @Id
    @SequenceGenerator(name = "owner_seq", sequenceName = "owner_seq", allocationSize = 1)
    @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "owner_seq")
    private Long ownerId;
    private String ownerName;
    private String ownerEmail;
    private String ownerAddress;
    private String ownerPhone;
    private String ownerIBAN;
}