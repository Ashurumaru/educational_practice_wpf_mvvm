CREATE DATABASE mvvm_project;
USE mvvm_project;

CREATE TABLE [User]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Login VARCHAR(50),
    Password VARCHAR(50),
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    MiddleName VARCHAR(50),
    AccessLevel INT
);

INSERT INTO [User] (Login, Password, FirstName, LastName, MiddleName, AccessLevel)
VALUES ('admin', '123', 'admin', 'adminov', 'adminovich', 1);