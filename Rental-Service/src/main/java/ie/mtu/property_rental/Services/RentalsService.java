package ie.mtu.property_rental.Services;


import ie.mtu.property_rental.Entities.Rentals;
import ie.mtu.property_rental.Repositories.RentalsRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

// Annotation
@Service
public class RentalsService {

    @Autowired
    private RentalsRepo rentalsRepository;

    // Save operation
    public Rentals saveRentals(Rentals rentals)
    {
        return rentalsRepository.save(rentals);
    }

    // Read All operation
    public List<Rentals> fetchRentalsList()
    {
        return (List<Rentals>)
                rentalsRepository.findAll();
    }


    public Rentals updateRentals(Rentals rentals, Long rentalsId) {
        Rentals rentalsDB = rentalsRepository.findById(rentalsId)
                .orElseThrow(() -> new IllegalArgumentException("Rentals not found with ID: " + rentalsId));


        if (rentals.getTenantId() != 0) {
            rentalsDB.setTenantId(rentals.getTenantId());
        }

        if (rentals.getPropertyId() != 0) {
            rentalsDB.setPropertyId(rentals.getPropertyId());
        }

        if (rentals.getRentalCost() != 0) { // Check if a new rentalCost is provided
            rentalsDB.setRentalCost(rentals.getRentalCost());
        }

        if (rentals.getDepositPaid() != 0) { // Check if a new depositPaid is provided
            rentalsDB.setDepositPaid(rentals.getDepositPaid());
        }

        Optional.ofNullable(rentals.getAdditionalTenantIds())
                .filter(additionalTenantIds -> !additionalTenantIds.trim().isEmpty())
                .ifPresent(rentalsDB::setAdditionalTenantIds);

        Optional.ofNullable(rentals.getStartDate())
                .ifPresent(rentalsDB::setStartDate);

        Optional.ofNullable(rentals.getEndDate())
                .ifPresent(rentalsDB::setEndDate);

        return rentalsRepository.save(rentalsDB);
    }

    // Delete operation
    public void deleteRentalsById(Long rentalsId)
    {
        rentalsRepository.deleteById(rentalsId);
    }
}