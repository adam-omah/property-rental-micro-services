package ie.mtu.property_rental.Services;

import ie.mtu.property_rental.Entities.Rentals;
import ie.mtu.property_rental.Repositories.RentalsRepo;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.jdbc.Sql;

import java.sql.Date;
import java.time.LocalDate;
import java.util.List;

import static org.junit.jupiter.api.Assertions.*;
import static org.springframework.test.context.jdbc.Sql.ExecutionPhase.AFTER_TEST_METHOD;
import static org.springframework.test.context.jdbc.Sql.ExecutionPhase.BEFORE_TEST_METHOD;

@SpringBootTest
class RentalsServiceTest {

    @Autowired
    RentalsService rentalsService;
    @Autowired
    RentalsRepo rentalsRepo;


    @Test
    @Sql(scripts = "classpath:SQL_Scripts/test-data.sql", executionPhase = BEFORE_TEST_METHOD) // Load test data before each test
    void saveRentals() {
        Rentals rentals = new Rentals();
        rentals.setTenantId(1L);
        rentals.setPropertyId(2L);
        rentals.setRentalCost(1000.0f);
        rentals.setDepositPaid(500.0f);
        rentals.setStartDate(Date.valueOf(LocalDate.now())); // Set the start date
        rentals.setEndDate(Date.valueOf(LocalDate.now().plusYears(1))); // Set the end date

        Rentals savedRentals = rentalsService.saveRentals(rentals);

        assertEquals(rentals, savedRentals); // Assert values in the database
    }

    @Test
    @Sql(scripts = "classpath:SQL_Scripts/test-data.sql", executionPhase = BEFORE_TEST_METHOD)
    void fetchRentalsList() {
        // Fetch all rentals
        List<Rentals> rentalsList = rentalsService.fetchRentalsList();

        // Assert that the list is not empty (at least one rental exists from test-data.sql)
        assertFalse(rentalsList.isEmpty());
    }

    @Test
    @Sql(scripts = "classpath:SQL_Scripts/test-data.sql", executionPhase = BEFORE_TEST_METHOD)
    void updateRentals() {
        // Fetch a rental to update
        Rentals existingRental = rentalsRepo.findById(1L).orElse(null);
        assertNotNull(existingRental);

        // Modify existingRental values
        existingRental.setRentalCost(1500.00f);
        existingRental.setDepositPaid(700.00f);

        // Update the rental
        Rentals updatedRental = rentalsService.updateRentals(existingRental, 1L);

        // Assert that the updated values are correct
        assertEquals(1500.00, updatedRental.getRentalCost());
        assertEquals(700.00, updatedRental.getDepositPaid());

        // You can add more assertions to check other properties of the updatedRental
    }

    @Test
    @Sql(scripts = "classpath:SQL_Scripts/test-data.sql", executionPhase = BEFORE_TEST_METHOD)
    void deleteRentalsById() {
        // Fetch the rental ID to delete
        Long rentalId = 1L;
        assertNotNull(rentalsRepo.findById(rentalId));

        // Delete the rental
        rentalsService.deleteRentalsById(rentalId);

        // Assert that the rental no longer exists
        assertFalse(rentalsRepo.findById(rentalId).isPresent());
    }
}