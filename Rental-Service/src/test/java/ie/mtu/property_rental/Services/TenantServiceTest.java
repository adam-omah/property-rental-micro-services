package ie.mtu.property_rental.Services;

import ie.mtu.property_rental.Entities.Tenant;
import ie.mtu.property_rental.Repositories.TenantRepo;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.jdbc.Sql;

import java.util.List;

import static org.junit.jupiter.api.Assertions.*;
import static org.springframework.test.context.jdbc.Sql.ExecutionPhase.AFTER_TEST_METHOD;
import static org.springframework.test.context.jdbc.Sql.ExecutionPhase.BEFORE_TEST_METHOD;

@SpringBootTest
class TenantServiceTest {

    @Autowired
    TenantService tenantService;
    @Autowired
    TenantRepo tenantRepo;

    @Test
    @Sql(scripts = "classpath:SQL_Scripts/test-data.sql", executionPhase = BEFORE_TEST_METHOD) // Load test data before each test
    void saveTenant() {
        Tenant tenant = new Tenant();
        tenant.setTenantName("Test Tenant");
        tenant.setTenantEmail("test@example.com");
        tenant.setTenantPhone("123-456-7890");

        Tenant savedTenant = tenantService.saveTenant(tenant);

        assertNotNull(savedTenant);
        assertEquals(tenant.getTenantName(), savedTenant.getTenantName());
        assertEquals(tenant.getTenantEmail(), savedTenant.getTenantEmail());
        assertEquals(tenant.getTenantPhone(), savedTenant.getTenantPhone());
    }

    @Test
    @Sql(scripts = "classpath:SQL_Scripts/test-data.sql", executionPhase = BEFORE_TEST_METHOD)
    void fetchTenantList() {
        List<Tenant> tenantList = tenantService.fetchTenantList();

        assertFalse(tenantList.isEmpty());
    }

    @Test
    @Sql(scripts = "classpath:SQL_Scripts/test-data.sql", executionPhase = BEFORE_TEST_METHOD)
    void updateTenant() {
        Tenant existingTenant = tenantRepo.findById(1L).orElse(null);
        assertNotNull(existingTenant);

        existingTenant.setTenantName("Updated Tenant");

        Tenant updatedTenant = tenantService.updateTenant(existingTenant, 1L);

        assertEquals("Updated Tenant", updatedTenant.getTenantName());
    }

    @Test
    @Sql(scripts = "classpath:SQL_Scripts/test-data.sql", executionPhase = BEFORE_TEST_METHOD)
    void deleteTenantById() {
        Long tenantId = 4L;
        assertNotNull(tenantRepo.findById(tenantId));

        tenantService.deleteTenantById(tenantId);

        assertFalse(tenantRepo.findById(tenantId).isPresent());
    }
}