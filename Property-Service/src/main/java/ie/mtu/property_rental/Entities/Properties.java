package ie.mtu.property_rental.Entities;


import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.hibernate.annotations.ColumnDefault;

@Entity
@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class Properties {
    @Id
    @SequenceGenerator(name = "properties_seq", sequenceName = "properties_seq", allocationSize = 1)
    @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "properties_seq")
    private long propertiesId;
    private long ownerId;
    @ColumnDefault("AVAILABLE")
    private String status;
    private String propertyName;
    private float rentalValue;
    private String propertyType;
    private String propertyDescription;
    private int bedrooms;
    private int bathrooms;
    private int parkingSpaces;
    private String features;
}
