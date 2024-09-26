package ie.mtu.property_rental.Repositories;

import ie.mtu.property_rental.Entities.Owner;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;


@Repository
public interface OwnerRepo
        extends JpaRepository<Owner, Long> {
}