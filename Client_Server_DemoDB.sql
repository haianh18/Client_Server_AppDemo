CREATE DATABASE ProductDB;
GO

USE ProductDB;
GO

CREATE TABLE Category (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL
);

CREATE TABLE Product (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    CategoryId INT,
    FOREIGN KEY (CategoryId) REFERENCES Category(Id)
);

INSERT INTO Category (Name) VALUES 
('Electronics'),
('Clothing'),
('Books'),
('Furniture');

INSERT INTO Product (Name, Price, CategoryId) VALUES 
('Laptop', 1200.00, 1),
('Smartphone', 800.00, 1),
('Headphones', 150.00, 1),
('T-Shirt', 20.00, 2),
('Jeans', 50.00, 2),
('Sneakers', 80.00, 2),
('Novel - Fiction', 15.00, 3),
('Cookbook', 25.00, 3),
('Office Chair', 200.00, 4),
('Dining Table', 500.00, 4);
