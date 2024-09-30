package ie.mtu.property_rental.Controller;

import ie.mtu.property_rental.Entities.Tenant;
import ie.mtu.property_rental.Services.TenantService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

// Annotation
@RestController

// Class
public class TenantController {

    @Autowired
    private TenantService tenantService;

    // Save operation
    @PostMapping("/tenants")
    public Tenant saveTenant(
            @RequestBody Tenant tenant)
    {
        return tenantService.saveTenant(tenant);
    }

    // Read operation
    @GetMapping("/tenants")
    public List<Tenant> fetchTenantList()
    {
        return tenantService.fetchTenantList();
    }

    // Update operation
    @PutMapping("/tenants/{id}")
    public Tenant
    updateTenant(@RequestBody Tenant tenant,
                     @PathVariable("id") Long tenantId)
    {
        return tenantService.updateTenant(
                tenant, tenantId);
    }

    // Delete operation
    @DeleteMapping("/tenants/{id}")
    public String deleteTenantById(@PathVariable("id")
                                       Long tenantId)
    {
        tenantService.deleteTenantById(
                tenantId);
        return "Deleted Successfully";
    }
}
