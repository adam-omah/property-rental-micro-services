CREATE TABLE IF NOT EXISTS tenant (
                        tenant_id BIGINT PRIMARY KEY,
                        tenant_name VARCHAR(255) NOT NULL,
                        tenant_email VARCHAR(255) NOT NULL,
                        tenant_phone VARCHAR(20)
);

CREATE SEQUENCE IF NOT EXISTS TENANT_SEQ START WITH 1 INCREMENT BY 1;

CREATE TABLE IF NOT EXISTS rentals (
    rentals_id BIGINT PRIMARY KEY,
    tenant_id BIGINT,
    property_id BIGINT,
    rental_cost DECIMAL,
    deposit_paid DECIMAL,
    additional_tenant_ids VARCHAR(255),
    start_date DATE,
    end_date DATE,
    FOREIGN KEY (tenant_id) REFERENCES tenant(tenant_id) -- Foreign key to the 'tenant' table
    );

CREATE SEQUENCE IF NOT EXISTS rentals_seq START WITH 1 INCREMENT BY 1;