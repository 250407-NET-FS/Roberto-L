-- BASIC CHALLENGES
-- List all customers (full name, customer id, and country) who are not in the USA
SELECT FirstName + ' ' + LastName AS 'Full Name', CustomerId, Country
FROM Customer
WHERE Country != 'USA';

-- List all customers from Brazil
SELECT FirstName + ' ' + LastName AS 'Full Name', CustomerId, Country
FROM Customer
WHERE Country = 'Brazil';

-- List all sales agents
SELECT FirstName + ' ' + LastName AS 'Full Name', Country
FROM Employee
WHERE Title = 'Sales Agents';

-- Retrieve a list of all countries in billing addresses on invoices
SELECT DISTINCT BillingCountry AS Countries
FROM Invoice;

-- Retrieve how many invoices there were in 2021, and what was the sales total for that year?
SELECT COUNT(*) AS 'Total Invoices 2021'
FROM Invoice
WHERE YEAR(InvoiceDate) = '2021';

-- (challenge: find the invoice count sales total for every year using one query)
SELECT YEAR(i.InvoiceDate) AS 'Year', COUNT(i.InvoiceId) AS 'Invoice Count', SUM(i.Total) AS 'Sales Total'
FROM Invoice i
GROUP BY YEAR(i.InvoiceDate)
ORDER BY YEAR(i.InvoiceDate);

-- how many line items were there for invoice #37
SELECT SUM(Quantity) AS 'Total Items'
FROM InvoiceLine
WHERE InvoiceId = '37';


-- how many invoices per country? BillingCountry  # of invoices -
SELECT BillingCountry AS Country, COUNT(*) AS 'Invoice Count'
FROM Invoice
GROUP BY BillingCountry;

-- Retrieve the total sales per country, ordered by the highest total sales first.
SELECT BillingCountry AS Country, SUM(Total) AS 'Total Sales'
FROM Invoice
GROUP BY BillingCountry
ORDER BY 'Total Sales' DESC;


-- JOINS CHALLENGES
-- Every Album by Artist
SELECT Album.Title, Artist.Name
From Album
    JOIN Artist ON Album.ArtistID = Artist.ArtistID;

-- All songs of the rock genre
SELECT Track.Name, Genre.Name
FROM Track
    JOIN Genre ON Track.GenreId = Genre.GenreId
Where Genre.Name = 'Rock';


-- Show all invoices of customers from brazil (mailing address not billing)
SELECT i.InvoiceId, i.InvoiceDate, c.CustomerId, c.FirstName + ' ' + c.LastName 'Customer'
From Invoice i
    JOIN Customer c ON i.CustomerId = c.CustomerId
WHERE c.Country = 'Brazil'

-- Show all invoices together with the name of the sales agent for each one
Select i.InvoiceId, i.InvoiceDate, c.CustomerId, c.FirstName + ' ' + c.LastName 'Customer', e.FirstName + ' ' + e.LastName 'Sales Agent'
From Invoice i
    JOIN Customer c ON i.CustomerId = c.CustomerId
    JOIN Employee e ON c.SupportRepId = e.EmployeeId

-- Which sales agent made the most sales in 2021?
SELECT TOP 1
    e.FirstName + ' ' + e.LastName AS 'Agent Name', SUM(i.Total) AS 'Total Sales 2021'
FROM Invoice i
    JOIN Customer c ON i.CustomerId = c.CustomerId
    JOIN Employee e ON c.SupportRepId = e.EmployeeId
WHERE YEAR(i.InvoiceDate) = '2021'
GROUP BY e.FirstName, e.LastName, e.EmployeeId
ORDER BY 'Total Sales 2021' DESC;

-- How many customers are assigned to each sales agent?
SELECT e.FirstName + ' ' + e.LastName AS 'Agent Name', SUM(c.CustomerId) AS 'Customer Count'
FROM Employee e
    JOIN Customer c ON c.SupportRepId = e.EmployeeId
GROUP BY e.FirstName, e.LastName, e.EmployeeId
ORDER BY 'Customer Count' DESC;

-- Which track was purchased the most in 2021?
SELECT TOP 1
    t.Name AS 'Track Name', SUM(il.UnitPrice * il.Quantity) AS 'Total Sales'
FROM InvoiceLine il
    JOIN Invoice i ON il.InvoiceId = i.InvoiceId
    JOIN Track t ON il.TrackId = t.TrackId
WHERE YEAR(i.InvoiceDate) = '2021'
GROUP BY t.TrackId, t.Name
ORDER BY 'Total Sales' DESC;

-- Show the top three best selling artists.
SELECT TOP 3
    ar.Name AS 'Artist Name', SUM(il.UnitPrice * il.Quantity) AS 'Total Sales'
FROM InvoiceLine il
    JOIN Track t ON il.TrackId = t.TrackId
    JOIN Album a ON t.AlbumId = a.AlbumId
    JOIN Artist ar ON a.ArtistId = ar.ArtistId
GROUP BY ar.ArtistId, ar.Name
ORDER BY 'Total Sales' DESC;

-- Which customers have the same initials as at least one other customer?
SELECT FirstName, LastName
FROM Customer
WHERE LEFT(FirstName, 1)  + LEFT(LastName, 1) IN (
    SELECT LEFT(FirstName, 1) + LEFT(LastName, 1)
FROM Customer
GROUP BY LEFT(FirstName, 1) + LEFT(LastName, 1)
HAVING COUNT(*) >1
)
ORDER BY LastName, FirstName;

-- ADVACED CHALLENGES
-- solve these with a mixture of joins, subqueries, CTE, and set operators.
-- solve at least one of them in two different ways, and see if the execution
-- plan for them is the same, or different.

-- 1. which artists did not make any albums at all?
SELECT a.name AS 'Artists with no ablums'
FROM Artist a
    lEFT JOIN Album al ON a.ArtistId = al. ArtistId
WHERE al.AlbumId IS NULL

-- 2. which artists did not record any tracks of the Latin genre?
SELECT ar.Name AS 'Artists with Latin music'
FROM Artist ar
WHERE ar.ArtistId NOT IN(
    SELECT DISTINCT ar.ArtistId
FROM Artist ar
    JOIN Album al ON ar.ArtistId = al.ArtistId
    JOIN Track t ON al.AlbumId = t.AlbumId
    JOIN Genre g ON t.GenreId = t.GenreId
WHERE g.Name = 'Latin'
);

-- 3. which video track has the longest length? (use media type table)
SELECT TOP 1
    t.Name AS 'Track Name', mt.Name AS 'Media Type', t.Milliseconds
FROM Track t
    JOIN MediaType mt ON t.MediaTypeId = mt.MediaTypeId
WHERE mt.MediaTypeId = '3'
ORDER BY t.Milliseconds DESC;

-- 4. find the names of the customers who live in the same city as the
--    boss employee (the one who reports to nobody)
SELECT FirstName+ ' ' + LastName AS 'Full Name'
FROM Employee
WHERE City = (
    SELECT City
    FROM Employee e
    WHERE ReportsTo IS NULL
)
    AND ReportsTo IS NOT NULL

-- 5. how many audio tracks were bought by German customers, and what was
--    the total price paid for them?
SELECT t.Name AS 'Track Name', mt.Name AS 'Media Type', c.FirstName+ ' ' + c.LastName AS 'Customer From Germany'
FROM Track t
    JOIN InvoiceLine il ON t.TrackId = il.TrackId
    JOIN Invoice i ON il.InvoiceId = i.InvoiceId
    JOIN Customer c ON i.CustomerId = c.CustomerId
    JOIN MediaType mt ON t.MediaTypeId = mt.MediaTypeId
WHERE c.Country = 'Germany'
    AND mt.Name LIKE '%audio%'

-- 6. list the names and countries of the customers supported by an employee
--    who was hired younger than 35.
SELECT c.FirstName + ' ' + c.LastName AS 'Customer Name', c.Country
FROM Customer c
    JOIN Employee e ON c.SupportRepId = e.EmployeeId
WHERE DATEDIFF(YEAR, e.BirthDate, e.HireDate) < 35;


-- DML exercises

-- 1. insert two new records into the employee table.
INSERT INTO Employee
    (EmployeeId, LastName, FirstName)
VALUES
    ((SELECT MAX(EmployeeId) + 1
        FROM Employee), 'White', 'Walter'),
    ((SELECT MAX(EmployeeId) + 2
        FROM Employee), 'Pinkman', 'Jesse');

-- 2. insert two new records into the tracks table.
INSERT INTO Track
    (TrackId, Name, MediaTypeId, Milliseconds, UnitPrice)
VALUES
    ((SELECT MAX(TrackId) + 1
        FROM Track), 'New Track A', 3, 1000000, 0.99),
    ((SELECT MAX(TrackId) + 2
        FROM Track), 'New Track B', 3, 1000000, 0.99);

-- 3. update customer Aaron Mitchell's name to Robert Walter
UPDATE Customer SET FirstName = 'Robert', LastName = 'Walter' WHERE FirstName = 'Aaron' AND LastName = 'Mitchell';

-- 4. delete one of the employees you inserted.
DELETE FROM Employee WHERE FirstName = 'Pinkman' AND LastName = 'Jesse';

-- 5. delete customer Robert Walter.
DELETE FROM Customer WHERE FirstName = 'Robert' AND LastName = 'Walter';