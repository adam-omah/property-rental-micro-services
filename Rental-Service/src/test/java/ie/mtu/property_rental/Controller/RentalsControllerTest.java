package ie.mtu.property_rental.Controller;

import ie.mtu.property_rental.Entities.Rentals;
import ie.mtu.property_rental.Services.RentalsService;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.jdbc.Sql;

import java.sql.Date;
import java.time.LocalDate;
import java.util.List;

import static org.junit.jupiter.api.Assertions.*;
import static org.springframework.test.context.jdbc.Sql.ExecutionPhase.BEFORE_TEST_METHOD;

@SpringBootTest
class RentalsControllerTest {

    @Autowired
    private RentalsController rentalsController;

    @Autowired
    private RentalsService rentalsService;

    @Test
    @Sql(scripts = "classpath:SQL_Scripts/test-data.sql", executionPhase = BEFORE_TEST_METHOD)
    void saveRentals() {
        Rentals rentals = new Rentals();
        rentals.setTenantId(1L);
        rentals.setPropertyId(2L);
        rentals.setRentalCost(1000.0f);
        rentals.setDepositPaid(500.0f);
        rentals.setStartDate(Date.valueOf(LocalDate.now()));
        rentals.setEndDate(Date.valueOf(LocalDate.now().plusYears(1)));

        Rentals savedRentals = rentalsController.saveRentals(rentals); // Call the controller method

        assertNotNull(savedRentals);
    }

    @Test
    @Sql(scripts = "classpath:SQL_Scripts/test-data.sql", executionPhase = BEFORE_TEST_METHOD)
    void fetchRentalsList() {
        List<Rentals> rentalsList = rentalsController.fetchRentalsList(); // Call the controller method

        assertNotNull(rentalsList);
    }

    @Test
    @Sql(scripts = "classpath:SQL_Scripts/test-data.sql", executionPhase = BEFORE_TEST_METHOD)
    void updateRentals() {
        // Fetch an existing rental using the service
        Rentals existingRental = rentalsService.fetchRentalsList().getFirst();
        assertNotNull(existingRental);

        // Modify existingRental values
        existingRental.setRentalCost(1500.00f);
        existingRental.setDepositPaid(700.00f);

        // Update the rental
        Rentals updatedRental = rentalsController.updateRentals(existingRental, existingRental.getRentalsId());

        assertNotNull(updatedRental);
        assertEquals(1500.00f, updatedRental.getRentalCost());
        assertEquals(700.00f, updatedRental.getDepositPaid());
    }

    @Test
    @Sql(scripts = "classpath:SQL_Scripts/test-data.sql", executionPhase = BEFORE_TEST_METHOD)
    void deleteRentalsById() {
        Long rentalsId = 3L;

        String result = rentalsController.deleteRentalsById(rentalsId); // Call the controller method

        assertEquals("Deleted Successfully", result);
    }
}