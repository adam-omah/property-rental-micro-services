package ie.mtu.property_rental.Controller;

import ie.mtu.property_rental.Entities.Rentals;
import ie.mtu.property_rental.Services.RentalsService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

// Annotation
@RestController

// Class
public class RentalsController {

    @Autowired
    private RentalsService rentalsService;

    // Save operation
    @PostMapping("/rentals")
    public Rentals saveRentals(
            @RequestBody Rentals rentals) {

        // Using the custom builder
        Rentals customRental = Rentals.rentalsBuilder(rentals.getTenantId(), rentals.getPropertyId(), rentals.getRentalType(),
                rentals.getDepositPaid(), rentals.getAdditionalTenantIds(),
                rentals.getStartDate(), rentals.getEndDate());
        return rentalsService.saveRentals(customRental);
    }

    // Read operation
    @GetMapping("/rentals")
    public List<Rentals> fetchRentalsList() {
        return rentalsService.fetchRentalsList();
    }

    // Update operation
    @PutMapping("/rentals/{id}")
    public Rentals updateRentals(@RequestBody Rentals rentals,
                                 @PathVariable("id") Long rentalsId) {
        return rentalsService.updateRentals(rentals, rentalsId);
    }

    // Delete operation
    @DeleteMapping("/rentals/{id}")
    public String deleteRentalsById(@PathVariable("id") Long rentalsId) {
        rentalsService.deleteRentalsById(rentalsId);
        return "Deleted Successfully";
    }
}
