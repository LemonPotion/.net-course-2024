--Заполняем бд данными
INSERT INTO currencies (currency_id, code, name, symbol)
VALUES (gen_random_uuid(), 'USD', 'US Dollar', '$'),
       (gen_random_uuid(), 'EUR', 'Euro', '€'),
       (gen_random_uuid(), 'JPY', 'Japanese Yen', '¥'),
       (gen_random_uuid(), 'INR', 'Indian Rupee', '₹');

INSERT INTO employees (employee_id, first_name, last_name, birth_date, phone_number, email, passport_number, contract, salary)
VALUES (gen_random_uuid(), 'John', 'Doe', '1985-03-25', '123 456 7890', 'Doe@example.com', 'A1234567', 'Full-time contract', 10000.00),
       (gen_random_uuid(), 'Joseph', 'Stalin', '1990-07-15', '987-654-3210', 'USSR@email.gov', 'B1234568', 'CEO', 35000.00),
       (gen_random_uuid(), 'Michael', 'Vsause', '1988-11-05', '555-555-5555', 'vsause@gmail.com', 'C1234569', 'Temporary contract', 5000.00);

INSERT INTO clients (client_id, first_name, last_name, birth_date, phone_number, email, passport_number)
VALUES (gen_random_uuid(), 'Bob', 'Ross', '1975-09-12', '321-654-0987', 'BobRoss123@gmail.com', 'D1234570'),
       (gen_random_uuid(), 'Keanu', 'Reeves', '1992-05-30', '654-321-0123', 'cyberpunk@gmail.com', 'E1234571'),
       (gen_random_uuid(), 'Michael', 'Jackson', '1980-01-10', '789-012-3456', 'billiejean@gmail.com', 'F1234572');

INSERT INTO accounts(account_id, currency_id, amount, client_id)
VALUES (gen_random_uuid(), '5cc6d872-b3a9-42ad-b5f5-9f65af011aec', 12345, 'c88c8d61-3889-44b6-afec-7fcb241da281'),
       (gen_random_uuid(), '5cc6d872-b3a9-42ad-b5f5-9f65af011aec', 12345678, '1cc712a8-f6ce-41d5-b964-95752493b3d7'),
       (gen_random_uuid(), '5cc6d872-b3a9-42ad-b5f5-9f65af011aec', 10000, '4f063d79-9555-42b0-8ce6-5cf8d4f6fc6f');

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
ORDER BY a.amount
LIMIT 1;

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