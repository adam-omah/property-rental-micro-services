package ie.mtu.property_rental.Controller;

import ie.mtu.property_rental.Entities.Tenant;
import ie.mtu.property_rental.Services.TenantService;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.jdbc.Sql;

import java.util.List;

import static org.junit.jupiter.api.Assertions.*;
import static org.springframework.test.context.jdbc.Sql.ExecutionPhase.BEFORE_TEST_METHOD;

@SpringBootTest
class TenantControllerTest {
    @Autowired
    private TenantController tenantController;

    @Autowired
    private TenantService tenantService;

    @Test
    @Sql(scripts = "classpath:SQL_Scripts/test-data.sql", executionPhase = BEFORE_TEST_METHOD)
    void saveTenant() {
        Tenant tenant = new Tenant();
        tenant.setTenantName("Test Tenant");
        tenant.setTenantEmail("test@example.com");
        tenant.setTenantPhone("123-456-7890");

        Tenant savedTenant = tenantController.saveTenant(tenant); // Call the controller method

        assertNotNull(savedTenant);
    }

    @Test
    @Sql(scripts = "classpath:SQL_Scripts/test-data.sql", executionPhase = BEFORE_TEST_METHOD)
    void fetchTenantList() {
        List<Tenant> tenantList = tenantController.fetchTenantList(); // Call the controller method

        assertNotNull(tenantList);
    }

    @Test
    @Sql(scripts = "classpath:SQL_Scripts/test-data.sql", executionPhase = BEFORE_TEST_METHOD)
    void updateTenant() {
        // Fetch an existing tenant using the service
        Tenant existingTenant = tenantService.fetchTenantList().get(0);
        assertNotNull(existingTenant);

        // Modify existingTenant values
        existingTenant.setTenantName("Updated Tenant");

        Tenant updatedTenant = tenantController.updateTenant(existingTenant, existingTenant.getTenantId());

        assertNotNull(updatedTenant);
        assertEquals("Updated Tenant", updatedTenant.getTenantName());
    }

    @Test
    @Sql(scripts = "classpath:SQL_Scripts/test-data.sql", executionPhase = BEFORE_TEST_METHOD)
    void deleteTenantById() {
        Long tenantId = 4L;

        String result = tenantController.deleteTenantById(tenantId); // Call the controller method

        assertEquals("Deleted Successfully", result);
    }

}