CREATE TABLE IF NOT EXISTS owner (
    owner_id BIGINT PRIMARY KEY,
    owner_name VARCHAR(255) NOT NULL,
    owner_email VARCHAR(255) NOT NULL,
    owner_address VARCHAR(255),
    owner_phone VARCHAR(20),
    owneriban VARCHAR(34)
);

CREATE SEQUENCE IF NOT EXISTS OWNER_SEQ START WITH 1 INCREMENT BY 1;

CREATE TABLE IF NOT EXISTS properties (
    properties_id BIGINT PRIMARY KEY,
    owner_id BIGINT,
    status VARCHAR(125) NOT NULL DEFAULT 'AVAILABLE',
    property_name VARCHAR(255) NOT NULL,
    rental_value DECIMAL,
    property_type VARCHAR(255),
    property_description TEXT,
    bedrooms INT,
    bathrooms INT,
    parking_spaces INT,
    features TEXT,
    FOREIGN KEY (owner_id) REFERENCES owner(owner_id) -- Foreign key constraint
    );

CREATE SEQUENCE IF NOT EXISTS properties_seq START WITH 1 INCREMENT BY 1;