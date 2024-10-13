--Заполняем бд данными
INSERT INTO currencies (currency_id, code, name, symbol)
VALUES ('5cc6d872-b3a9-42ad-b5f5-9f65af011aec', 'USD', 'US Dollar', '$'),
       ('1958f11e-7db0-4ae0-96cd-e55b76865654', 'EUR', 'Euro', '€'),
       ('3237d2e3-5b68-4311-b4d9-e7d3fd122b61', 'JPY', 'Japanese Yen', '¥'),
       ('c2feea0b-ab57-4297-87df-2baa231da5cd', 'INR', 'Indian Rupee', '₹');

INSERT INTO employees (employee_id, first_name, last_name, birth_date, phone_number, email, passport_number, contract, salary)
VALUES ('0719860e-32bf-46b0-a929-da1a32c76e24', 'John', 'Doe', '1985-03-25', '123 456 7890', 'Doe@example.com', 'A1234567', 'Full-time contract', 10000.00),
       ('f8d02106-3cdb-4db0-8a26-caf160d3b447', 'Joseph', 'Stalin', '1990-07-15', '987-654-3210', 'USSR@email.gov', 'B1234568', 'CEO', 35000.00),
       ('58226b6a-f60b-40cb-9da7-108669d82469', 'Michael', 'Vsause', '1988-11-05', '555-555-5555', 'vsause@gmail.com', 'C1234569', 'Temporary contract', 5000.00);

INSERT INTO clients (client_id, first_name, last_name, birth_date, phone_number, email, passport_number)
VALUES ('c88c8d61-3889-44b6-afec-7fcb241da281', 'Bob', 'Ross', '1975-09-12', '321-654-0987', 'BobRoss123@gmail.com', 'D1234570'),
       ('1cc712a8-f6ce-41d5-b964-95752493b3d7', 'Keanu', 'Reeves', '1992-05-30', '654-321-0123', 'cyberpunk@gmail.com', 'E1234571'),
       ('4f063d79-9555-42b0-8ce6-5cf8d4f6fc6f', 'Michael', 'Jackson', '1980-01-10', '789-012-3456', 'billiejean@gmail.com', 'F1234572');

INSERT INTO accounts(account_id, currency_id, amount, client_id)
VALUES ('6a8c0444-bf5f-403c-865b-acb2e9d638d7', '5cc6d872-b3a9-42ad-b5f5-9f65af011aec', 12345, 'c88c8d61-3889-44b6-afec-7fcb241da281'),
       ('199d3ade-3d63-4c62-b6ae-a220cc8662ca', '5cc6d872-b3a9-42ad-b5f5-9f65af011aec', 12345678, '1cc712a8-f6ce-41d5-b964-95752493b3d7'),
       ('19a538e4-561c-411a-a6e1-d5277ea8ca2b', '5cc6d872-b3a9-42ad-b5f5-9f65af011aec', 10000, '4f063d79-9555-42b0-8ce6-5cf8d4f6fc6f');

--б
SELECT c.client_id, c.first_name, c.last_name
FROM clients c
    JOIN accounts a ON c.client_id = a.client_id
WHERE a.amount < 20000
ORDER BY a.amount ASC;

--в
SELECT c.client_id, c.first_name, c.last_name
FROM clients c
         JOIN accounts a ON c.client_id = a.client_id
WHERE a.amount = (SELECT MIN(amount) FROM accounts)
ORDER BY a.amount;

--г
SELECT SUM(amount) AS total
FROM accounts;

--д
SELECT  a.account_id, c.client_id, c.first_name, c.last_name, a.amount
FROM accounts a
    JOIN clients c ON a.client_id = c.client_id
    JOIN currencies cu ON a.currency_id = cu.currency_id;

--e
SELECT client_id, first_name, last_name
FROM clients
ORDER BY birth_date DESC;

--ж
SELECT  EXTRACT(YEAR FROM AGE(birth_date)) AS age, COUNT(*) AS count
FROM clients
GROUP BY age;

--з
SELECT EXTRACT(YEAR FROM AGE(birth_date)) AS age, COUNT(*) AS count
FROM clients
GROUP BY age;

--и
SELECT client_id, first_name, last_name
FROM clients
LIMIT 1;