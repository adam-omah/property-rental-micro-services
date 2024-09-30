package ie.mtu.property_rental.Repositories;

import ie.mtu.property_rental.Entities.Properties;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface PropertiesRepo
        extends JpaRepository<Properties, Long> {
}
