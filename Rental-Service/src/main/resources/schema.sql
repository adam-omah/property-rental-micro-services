CREATE TABLE IF NOT EXISTS tenant (
                                      tenant_id BIGINT PRIMARY KEY,
                                      tenant_name VARCHAR(255) NOT NULL,
    tenant_email VARCHAR(255) NOT NULL,
    tenant_phone VARCHAR(20)
    );

CREATE SEQUENCE IF NOT EXISTS TENANT_SEQ START WITH 1 INCREMENT BY 1;

-- Insert tenants individually with conditional checks
INSERT INTO tenant (tenant_id, tenant_name, tenant_email, tenant_phone)
SELECT nextval('TENANT_SEQ'), 'John Doe', 'john.doe@example.com', '089-456-7890'
    WHERE NOT EXISTS (SELECT 1 FROM tenant WHERE tenant_email = 'john.doe@example.com');

INSERT INTO tenant (tenant_id, tenant_name, tenant_email, tenant_phone)
SELECT nextval('TENANT_SEQ'), 'Jane Smith', 'jane.smith@example.com', '087-654-3210'
    WHERE NOT EXISTS (SELECT 1 FROM tenant WHERE tenant_email = 'jane.smith@example.com');

INSERT INTO tenant (tenant_id, tenant_name, tenant_email, tenant_phone)
SELECT nextval('TENANT_SEQ'), 'David Lee', 'david.lee@example.com', '087-123-4567'
    WHERE NOT EXISTS (SELECT 1 FROM tenant WHERE tenant_email = 'david.lee@example.com');


CREATE TABLE IF NOT EXISTS rentals (
                                       rentals_id BIGINT PRIMARY KEY,
                                       tenant_id BIGINT,
                                       property_id BIGINT,
                                       rental_type VARCHAR(12),
    rental_cost DECIMAL,
    deposit_paid DECIMAL,
    additional_tenant_ids VARCHAR(255),
    start_date DATE,
    end_date DATE,
    FOREIGN KEY (tenant_id) REFERENCES tenant(tenant_id)
    );

CREATE SEQUENCE IF NOT EXISTS rentals_seq START WITH 1 INCREMENT BY 1;

-- Insert rentals individually, retrieving tenant_id using subqueries
INSERT INTO rentals (rentals_id, tenant_id, property_id, rental_type, rental_cost, deposit_paid, additional_tenant_ids, start_date, end_date)
SELECT nextval('rentals_seq'), t.tenant_id, 1, 'STANDARD', 1200.00, 500.00, NULL, '2024-01-15', '2025-01-14'
FROM tenant t
WHERE t.tenant_email = 'john.doe@example.com';  -- Assuming John Doe is the tenant

INSERT INTO rentals (rentals_id, tenant_id, property_id, rental_type, rental_cost, deposit_paid, additional_tenant_ids, start_date, end_date)
SELECT nextval('rentals_seq'), t.tenant_id, 2, 'SHORT', 1800.00, 700.00, NULL, '2024-02-20', '2025-02-19'
FROM tenant t
WHERE t.tenant_email = 'jane.smith@example.com'; -- Assuming Jane Smith is the tenant


INSERT INTO rentals (rentals_id, tenant_id, property_id, rental_type, rental_cost, deposit_paid, additional_tenant_ids, start_date, end_date)
SELECT nextval('rentals_seq'), t.tenant_id, 3, 'LONG', 1500.00, 600.00, NULL, '2024-03-10', '2025-03-09'
FROM tenant t
WHERE t.tenant_email = 'david.lee@example.com'; -- Assuming David is the tenant