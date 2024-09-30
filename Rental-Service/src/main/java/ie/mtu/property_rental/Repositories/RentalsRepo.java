package ie.mtu.property_rental.Repositories;

import ie.mtu.property_rental.Entities.Rentals;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;


@Repository
public interface RentalsRepo
        extends JpaRepository<Rentals, Long> {
}