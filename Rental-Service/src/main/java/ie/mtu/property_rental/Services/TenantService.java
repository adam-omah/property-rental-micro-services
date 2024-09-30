package ie.mtu.property_rental.Services;


import ie.mtu.property_rental.Entities.Tenant;
import ie.mtu.property_rental.Repositories.TenantRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

// Annotation
@Service

// Class
public class TenantService {

    @Autowired
    private TenantRepo tenantRepository;

    // Save operation
    public Tenant saveTenant(Tenant tenant)
    {
        return tenantRepository.save(tenant);
    }

    // Read All operation
    public List<Tenant> fetchTenantList()
    {
        return (List<Tenant>)
                tenantRepository.findAll();
    }


    public Tenant updateTenant(Tenant tenant, Long tenantId) {
        Tenant tenantDB = tenantRepository.findById(tenantId)
                .orElseThrow(() -> new IllegalArgumentException("Tenant not found with ID: " + tenantId));

        Optional.ofNullable(tenant.getTenantName())
                .filter(name -> !name.trim().isEmpty())
                .ifPresent(tenantDB::setTenantName);

        Optional.ofNullable(tenant.getTenantEmail())
                .filter(email -> !email.trim().isEmpty())
                .ifPresent(tenantDB::setTenantEmail);

        Optional.ofNullable(tenant.getTenantPhone())
                .filter(phone -> !phone.trim().isEmpty())
                .ifPresent(tenantDB::setTenantPhone);

        return tenantRepository.save(tenantDB);
    }

    // Delete operation
    public void deleteTenantById(Long tenantId)
    {
        tenantRepository.deleteById(tenantId);
    }
}