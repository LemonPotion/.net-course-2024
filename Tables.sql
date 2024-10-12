CREATE EXTENSION IF NOT EXISTS "pgcrypto";

CREATE TABLE employees (
    employee_id uuid PRIMARY KEY,
    first_name VARCHAR NOT NULL,
    last_name VARCHAR NOT NULL,
    birth_date DATE NOT NULL,
    phone_number VARCHAR(20),
    email VARCHAR(254) UNIQUE,
    passport_number VARCHAR(50) UNIQUE NOT NULL,
    contract TEXT NOT NULL,
    salary DECIMAL(10,2) NOT NULL
);

CREATE TABLE clients (
    client_id uuid PRIMARY KEY,
    first_name VARCHAR NOT NULL,
    last_name VARCHAR NOT NULL,
    birth_date DATE NOT NULL,
    phone_number VARCHAR(20),
    email VARCHAR(254) UNIQUE,
    passport_number VARCHAR(50) UNIQUE NOT NULL
);

CREATE TABLE currencies(
    currency_id uuid PRIMARY KEY,
    code VARCHAR(3) UNIQUE,
    name VARCHAR(50) UNIQUE,
    symbol VARCHAR(5)
);

CREATE TABLE accounts (
    account_id uuid PRIMARY KEY,
    currency_id uuid REFERENCES currencies(currency_id),
    amount decimal(10,2) NOT NULL,
    client_id uuid REFERENCES clients(client_id)
);