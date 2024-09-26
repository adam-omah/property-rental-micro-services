DROP TABLE IF EXISTS owner;
CREATE TABLE owner (
                        owner_id BIGINT PRIMARY KEY,
                        owner_name VARCHAR(255) NOT NULL,
                        owner_email VARCHAR(255) NOT NULL,
                        owner_address VARCHAR(255),
                        owner_phone VARCHAR(20),
                        owneriban VARCHAR(34)  -- Assuming a fixed length of 34 for IBAN
);

DROP SEQUENCE IF EXISTS OWNER_SEQ;
CREATE SEQUENCE OWNER_SEQ START WITH 1 INCREMENT BY 1;