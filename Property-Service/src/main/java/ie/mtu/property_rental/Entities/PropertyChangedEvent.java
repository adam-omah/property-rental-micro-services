package ie.mtu.property_rental.Entities;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class PropertyChangedEvent {
    private final long propertyId;
    private final String previousStatus;
    private final String newStatus;


    public String getPropertyId() {
        return String.valueOf(propertyId);
    }
}