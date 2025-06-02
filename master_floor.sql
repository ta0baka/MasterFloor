CREATE TABLE product_types (
    product_type_id SERIAL PRIMARY KEY,
    type_name VARCHAR(100) NOT NULL UNIQUE,
    type_coefficient DECIMAL(10, 2) NOT NULL
);

CREATE TABLE material_types (
    material_type_id SERIAL PRIMARY KEY,
    type_name VARCHAR(100) NOT NULL UNIQUE,
    defect_percentage DECIMAL(10, 4) NOT NULL
);

CREATE TABLE products (
    product_id SERIAL PRIMARY KEY,
    product_type_id INTEGER NOT NULL,
    product_name VARCHAR(255) NOT NULL UNIQUE,
    article_number VARCHAR(50) NOT NULL UNIQUE,
    min_partner_price DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (product_type_id) REFERENCES product_types(product_type_id)
);

CREATE TABLE partners (
    partner_id SERIAL PRIMARY KEY,
    partner_type VARCHAR(50) NOT NULL,
    partner_name VARCHAR(100) NOT NULL UNIQUE,
    director VARCHAR(150) NOT NULL,
    email VARCHAR(100) NOT NULL,
    phone VARCHAR(50) NOT NULL,
    legal_address VARCHAR(255) NOT NULL,
    inn VARCHAR(20) NOT NULL UNIQUE,
    rating INTEGER NOT NULL
);

CREATE TABLE partner_products (
    id SERIAL PRIMARY KEY,
    product_id INTEGER NOT NULL,
    partner_id INTEGER NOT NULL,
    quantity INTEGER NOT NULL,
    sale_date TIMESTAMP NOT NULL,
    FOREIGN KEY (product_id) REFERENCES products(product_id),
    FOREIGN KEY (partner_id) REFERENCES partners(partner_id)
);