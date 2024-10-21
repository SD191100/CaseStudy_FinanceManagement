Create Database FinanceMgt
go

use FinanceMgt
go

-- Create Users table

CREATE TABLE Users (
  UserID INT PRIMARY KEY IDENTITY(1,1),
  UserName VARCHAR(50) NOT NULL,
  Password VARBINARY(MAX) NOT NULL,
  Email VARCHAR(100) UNIQUE NOT NULL
)

-- Create ExpenseCategories table

CREATE TABLE ExpenseCategories (
  CategoryID INT PRIMARY KEY IDENTITY(1,1),
  CategoryName VARCHAR(50) NOT NULL
)

-- Create Expenses table

CREATE TABLE Expenses (
  ExpenseID INT PRIMARY KEY IDENTITY(1,1),
  UserId INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
  Amount DECIMAL(10, 2) NOT NULL,
  CategoryID INT NOT NULL FOREIGN KEY REFERENCES ExpenseCategories(CategoryID),
  Date DATE NOT NULL,
  Description VARCHAR(200) 
)

ALTER TABLE Users
ALTER COLUMN password nvarchar(50);


INSERT INTO ExpenseCategories (CategoryName)
VALUES 
('Food'),
('Transportation'),
('Utilities'),
('Entertainment'),
('Healthcare'),
('Education'),
('Shopping'),
('Rent'),
('Travel');