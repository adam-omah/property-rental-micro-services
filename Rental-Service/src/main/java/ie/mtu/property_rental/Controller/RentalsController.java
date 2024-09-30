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
    @PostMapping("/rentalss")
    public Rentals saveRentals(
            @RequestBody Rentals rentals)
    {
        return rentalsService.saveRentals(rentals);
    }

    // Read operation
    @GetMapping("/rentalss")
    public List<Rentals> fetchRentalsList()
    {
        return rentalsService.fetchRentalsList();
    }

    // Update operation
    @PutMapping("/rentalss/{id}")
    public Rentals
    updateRentals(@RequestBody Rentals rentals,
                     @PathVariable("id") Long rentalsId)
    {
        return rentalsService.updateRentals(
                rentals, rentalsId);
    }

    // Delete operation
    @DeleteMapping("/rentalss/{id}")
    public String deleteRentalsById(@PathVariable("id")
                                       Long rentalsId)
    {
        rentalsService.deleteRentalsById(
                rentalsId);
        return "Deleted Successfully";
    }
}
