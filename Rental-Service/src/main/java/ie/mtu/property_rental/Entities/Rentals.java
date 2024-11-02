package ie.mtu.property_rental.Entities;

import com.fasterxml.jackson.annotation.JsonFormat;
import ie.mtu.property_rental.Configuration.RentalsConfig;
import ie.mtu.property_rental.models.RentalType;
import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.http.*;
import org.springframework.web.client.RestTemplate;
import org.springframework.boot.web.client.RestTemplateBuilder;

import java.sql.Date;
import java.util.Collections;

@Entity
@Data
@NoArgsConstructor
@AllArgsConstructor
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

    public Rentals(RentalsBuilder rentalsBuilder) {
    }



    // Custom builder with logic for rental cost
    public static class RentalsBuilder{
        // Required Parameters
        private String tenantId;
        private String propertyId;
        private Date startDate;


        // optional Parameters
        private float rentalCost = 0;
        private RentalType rentalType = RentalType.STANDARD;
        private String additionalTenantIds = null;
        private Date endDate = null;
        private float depositPaid = 0;


        public RentalsBuilder(String propertyId, String tenantId, Date startDate) {
            this.tenantId = tenantId;
            this.propertyId = propertyId;
            this.startDate = startDate;
        }

        public RentalsBuilder setRentalCost(float rentalCost) {
            this.rentalCost = rentalCost;
            if (this.rentalCost <= 0){
                this.rentalCost = calculateRentalValue(this.propertyId, this.rentalType);
            }
            return this;
        }

        public RentalsBuilder setRentalType(RentalType rentalType) {
            this.rentalType = rentalType;
            return this;
        }

        public RentalsBuilder setAdditionalTenants(String additionalTenantIds) {
            this.additionalTenantIds = additionalTenantIds;
            return this;
        }

        public RentalsBuilder setDepositPaid(float depositPaid) {
            this.depositPaid = depositPaid;
            return this;
        }

        public RentalsBuilder setEndDate(Date endDate) {
            this.endDate = endDate;
            return this;
        }

        public Rentals build(){
            return new Rentals(this);
        }
    }

    private static float calculateRentalValue(String propertyId, RentalType rentalType) {
        RestTemplate restTemplate = new RestTemplateBuilder()
                .build();

        HttpHeaders headers = new HttpHeaders();
        headers.setAccept(Collections.singletonList(MediaType.APPLICATION_JSON));

        HttpEntity<String> entity = new HttpEntity<>(headers);

        try {

            String propertiesServiceUrl = RentalsConfig.getPropertiesServiceUrl();
            log.warn(propertiesServiceUrl);
            ResponseEntity<Properties> response = restTemplate.exchange(
                    propertiesServiceUrl + "/properties/" + propertyId,
                    HttpMethod.GET,
                    entity,
                    Properties.class
            );

            if (response.getStatusCode() == HttpStatus.OK) {
                Properties propertyData = response.getBody();
                log.info(response.toString());
                log.warn(String.valueOf(propertyData.getRentalValue()));
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
