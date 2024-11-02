CREATE TABLE IF NOT EXISTS owner (
    owner_id BIGINT PRIMARY KEY,
    owner_name VARCHAR(255) NOT NULL,
    owner_email VARCHAR(255) NOT NULL,
    owner_address VARCHAR(255),
    owner_phone VARCHAR(20),
    owneriban VARCHAR(34)
);

CREATE SEQUENCE IF NOT EXISTS OWNER_SEQ START WITH 1 INCREMENT BY 1;

-- Insert Alice Johnson IF she doesn't exist
INSERT INTO owner (owner_id, owner_name, owner_email, owner_address, owner_phone, owneriban)
SELECT nextval('OWNER_SEQ'), 'Alice Johnson', 'alice.johnson@example.com', '123 Main St', '085-111-2222', 'IE12BOFI00000000000000'
    WHERE NOT EXISTS (SELECT 1 FROM owner WHERE owner_email = 'alice.johnson@example.com');

-- Insert Bob Williams IF he doesn't exist
INSERT INTO owner (owner_id, owner_name, owner_email, owner_address, owner_phone, owneriban)
SELECT nextval('OWNER_SEQ'), 'Bob Williams', 'bob.williams@example.com', '456 Oak Ave', '089-333-4444', 'IE12BOFI00000000000000'
    WHERE NOT EXISTS (SELECT 1 FROM owner WHERE owner_email = 'bob.williams@example.com');

-- Insert Carol Davis IF she doesn't exist
INSERT INTO owner (owner_id, owner_name, owner_email, owner_address, owner_phone, owneriban)
SELECT nextval('OWNER_SEQ'), 'Carol Davis', 'carol.davis@example.com', '789 Pine Ln', '087-555-6666', 'IE12BOFI00000000000000'
    WHERE NOT EXISTS (SELECT 1 FROM owner WHERE owner_email = 'carol.davis@example.com');


CREATE TABLE IF NOT EXISTS properties (
    properties_id BIGINT PRIMARY KEY,
    owner_id BIGINT,
    status VARCHAR(125) NOT NULL DEFAULT 'AVAILABLE',
    property_name VARCHAR(255) NOT NULL,
    property_eircode VARCHAR(10) NOT NULL,
    rental_value DECIMAL,
    property_type VARCHAR(255),
    property_description TEXT,
    bedrooms INT,
    bathrooms INT,
    parking_spaces INT,
    features TEXT,
    FOREIGN KEY (owner_id) REFERENCES owner(owner_id)
);

CREATE SEQUENCE IF NOT EXISTS properties_seq START WITH 1 INCREMENT BY 1;


-- Use a subquery to get the correct owner IDs
INSERT INTO properties (properties_id, owner_id, status, property_name, property_eircode, rental_value, property_type, property_description, bedrooms, bathrooms, parking_spaces, features)
SELECT nextval('properties_seq'), o.owner_id, 'AVAILABLE', 'Cozy Apartment', 'A65 F4E2', 1500.00, 'Apartment', 'A comfortable one-bedroom apartment.', 1, 1, 0, 'Balcony, City View'
FROM owner o
WHERE o.owner_email = 'alice.johnson@example.com'  -- Alice Owner
  AND NOT EXISTS (SELECT 1 FROM properties WHERE property_eircode = 'A65 F4E2');

-- Insert for Spacious House (using subquery)
INSERT INTO properties (properties_id, owner_id, status, property_name, property_eircode, rental_value, property_type, property_description, bedrooms, bathrooms, parking_spaces, features)
SELECT nextval('properties_seq'), o.owner_id, 'AVAILABLE', 'Spacious House', 'D02 K7P5', 2200.00, 'House', 'A spacious three-bedroom house with a garden.', 3, 2, 2, 'Garden, Fireplace'
FROM owner o
WHERE o.owner_email = 'bob.williams@example.com'  -- Bob Williams owns this one
  AND NOT EXISTS (SELECT 1 FROM properties WHERE property_eircode = 'D02 K7P5');


-- Insert for Bungalow
INSERT INTO properties (properties_id, owner_id, status, property_name, property_eircode, rental_value, property_type, property_description, bedrooms, bathrooms, parking_spaces, features)
SELECT nextval('properties_seq'), o.owner_id, 'RENTED', 'The Cottage', 'T12 Y8H9', 1800.00, 'Bungalow', 'A modern two-bedroom bungalow with a pool.', 2, 2, 1, 'Pool, Gym'
FROM owner o
WHERE o.owner_email = 'alice.johnson@example.com'  -- Back to Alice Johnson
  AND NOT EXISTS (SELECT 1 FROM properties WHERE property_eircode = 'T12 Y8H9');