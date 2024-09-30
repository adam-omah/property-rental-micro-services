package ie.mtu.property_rental.Repositories;

import ie.mtu.property_rental.Entities.Tenant;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;


@Repository
public interface TenantRepo
        extends JpaRepository<Tenant, Long> {
}