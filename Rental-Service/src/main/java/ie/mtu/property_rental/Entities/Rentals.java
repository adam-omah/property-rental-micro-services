package ie.mtu.property_rental.Entities;

import ie.mtu.property_rental.models.RentalType;
import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.hibernate.annotations.ColumnDefault;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.client.RestTemplate;

import java.sql.Date;
import java.util.Map;

@Entity
@Data
@NoArgsConstructor
@AllArgsConstructor
public class Rentals {
    @Id
    @SequenceGenerator(name = "rentals_seq", sequenceName = "rentals_seq", allocationSize = 1)
    @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "rentals_seq")
    private Long rentalsId;
    private String tenantId;
    private String propertyId;
    @ColumnDefault("STANDARD")
    private RentalType rentalType;
    private float rentalCost;
    private float depositPaid;
    private String additionalTenantIds;
    @Temporal(TemporalType.DATE)
    @DateTimeFormat(pattern = "dd/MM/yyyy")
    private Date startDate;
    @Temporal(TemporalType.DATE)
    @DateTimeFormat(pattern = "dd/MM/yyyy")
    private Date endDate;

    // Custom builder with logic for rental cost
    @Builder(builderMethodName = "rentalsBuilder")
    public static Rentals rentalsBuilder(String tenantId, String propertyId, RentalType rentalType,
                                         float depositPaid, String additionalTenantIds, Date startDate, Date endDate) {
        Rentals rentals = new Rentals();
        rentals.tenantId = tenantId;
        rentals.propertyId = propertyId;
        rentals.rentalType = rentalType;
        rentals.depositPaid = depositPaid;
        rentals.additionalTenantIds = additionalTenantIds;
        rentals.startDate = startDate;
        rentals.endDate = endDate;

        // Calculate and set rentalCost if null
        if (rentals.rentalCost == 0) {
            rentals.rentalCost = calculateMonthlyCost(propertyId, rentalType);
        }
        return rentals;
    }

    private static float calculateMonthlyCost(String propertyId, RentalType rentalType) {
        RestTemplate restTemplate = new RestTemplate();
        var response = restTemplate.getForEntity("https://api.yourdomain.com/properties/" + propertyId, Map.class);

        if (response.getStatusCode() == HttpStatus.OK) {
            Map<String, Object> propertyData = response.getBody();
            Object monthlyCostObject = propertyData.get("monthlyCost");
            if (monthlyCostObject instanceof Float) {
                switch (rentalType){
                    case STANDARD -> {
                        return (Float)monthlyCostObject * 1.1f;
                    }
                    case SHORT -> {
                        return (Float)monthlyCostObject * 1.15f;
                    }
                    case LONG -> {
                        return (Float)monthlyCostObject * 1.05f;
                    }
                }
            } else {
                // Handle unexpected data type or null value
                System.out.println("Invalid monthlyCost format in API response");
                return 0.0f;
            }
        } else {
            // Handle API call errors
            System.out.println("Failed to retrieve property data from API");
            return 0.0f;
        }
        return 0.0f;
    }
}