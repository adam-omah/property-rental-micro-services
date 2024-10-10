package ie.mtu.property_rental.Entities;

import com.fasterxml.jackson.annotation.JsonFormat;
import ie.mtu.property_rental.models.RentalType;
import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.hibernate.annotations.ColumnDefault;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.http.*;
import org.springframework.web.client.RestTemplate;
import org.springframework.boot.web.client.RestTemplateBuilder;
import org.springframework.context.annotation.Bean;

import java.sql.Date;
import java.util.Collections;
import java.util.Map;

@Entity
@Data
public class Rentals {
    private static final Logger log = LoggerFactory.getLogger(Rentals.class);
    @Id
    @SequenceGenerator(name = "rentals_seq", sequenceName = "rentals_seq", allocationSize = 1)
    @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "rentals_seq")
    private Long rentalsId;
    private String tenantId;
    private String propertyId;
    @Enumerated(EnumType.STRING)
    private RentalType rentalType = RentalType.STANDARD;
    private float rentalCost;
    private float depositPaid;
    private String additionalTenantIds;
    @Temporal(TemporalType.DATE)
    @DateTimeFormat(pattern = "dd/MM/yyyy")
    @JsonFormat(pattern = "dd/MM/yyyy")
    private Date startDate;
    @Temporal(TemporalType.DATE)
    @DateTimeFormat(pattern = "dd/MM/yyyy")
    @JsonFormat(pattern = "dd/MM/yyyy")
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
            rentals.rentalCost = calculateRentalValue(propertyId, rentalType);
        }
        return rentals;
    }

    private static float calculateRentalValue(String propertyId, RentalType rentalType) {
        RestTemplate restTemplate = new RestTemplateBuilder()
                .build();

        HttpHeaders headers = new HttpHeaders();
        headers.setAccept(Collections.singletonList(MediaType.APPLICATION_JSON));

        HttpEntity<String> entity = new HttpEntity<>(headers);

        try {
            ResponseEntity<Properties> response = restTemplate.exchange(
                    "http://localhost:8084/properties/" + propertyId,
                    HttpMethod.GET,
                    entity,
                    Properties.class
            );

            if (response.getStatusCode() == HttpStatus.OK) {
                Properties propertyData = response.getBody();
                log.info(response.toString());
                if (propertyData != null) {
                    switch (rentalType) {
                        case STANDARD -> {
                            return propertyData.getRentalValue() * 1.1f;
                        }
                        case SHORT -> {
                            return propertyData.getRentalValue() * 1.15f;
                        }
                        case LONG -> {
                            return propertyData.getRentalValue() * 1.05f;
                        }
                    }
                } else {
                        // Handle unexpected data type or null value
                        log.info("Invalid monthlyCost format in API response");
                    }
                } else {
                    log.info("No property data found in response");
                }
        } catch (Exception e) {
            log.info("Error during API call: {}", e.getMessage());
        }
        return 0.0f;
    }
}
