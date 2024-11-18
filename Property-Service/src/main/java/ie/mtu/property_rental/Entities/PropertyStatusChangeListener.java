package ie.mtu.property_rental.Entities;

import org.springframework.context.event.EventListener;
import org.springframework.stereotype.Component;

@Component
public class PropertyStatusChangeListener {

    @EventListener
    public void onPropertyChanged(PropertyChangedEvent event) {
        String propertyId = event.getPropertyId();
        String previousStatus = event.getPreviousStatus();
        String newStatus = event.getNewStatus();

        if (newStatus.equals("RENTED")) {
            System.out.println("Property with ID " + propertyId + " changed from " + previousStatus + " to RENTED.");
            System.out.println("Property owner notified of new rental!");
        }
    }
}
