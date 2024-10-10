package ie.mtu.property_rental.Entities;

import jakarta.persistence.*;
import lombok.Data;
import lombok.Getter;
import lombok.Setter;

@Entity
@Data
@Getter
@Setter
public class Properties {
    @Id
    private long propertiesId;
    private long ownerId;
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