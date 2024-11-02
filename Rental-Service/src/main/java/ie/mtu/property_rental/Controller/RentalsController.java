package ie.mtu.property_rental.Controller;

import ie.mtu.property_rental.Entities.Rentals;
import ie.mtu.property_rental.Services.RentalsService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

// Annotation
@RestController
@CrossOrigin(origins = "http://localhost:4200")
// Class
public class RentalsController {

    @Autowired
    private RentalsService rentalsService;

    // Save operation
    @PostMapping("/rentals")
    public Rentals saveRentals(
            @RequestBody Rentals rentals) {


        Rentals customRentalNew = new Rentals.RentalsBuilder(
                rentals.getPropertyId(),
                rentals.getTenantId(),
                rentals.getEndDate())
                .setRentalType(rentals.getRentalType())
                .setRentalCost(rentals.getRentalCost())
                .setAdditionalTenants(rentals.getAdditionalTenantIds())
                .setDepositPaid(rentals.getDepositPaid()).setEndDate(rentals.getEndDate()).build();

        return rentalsService.saveRentals(customRentalNew);
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
