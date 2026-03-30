SET NOCOUNT ON;
GO

CREATE TABLE Employees2
(
    EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(128) NOT NULL,
    LastName NVARCHAR(128) NOT NULL,
    Age INT NOT NULL CHECK (Age >= 18 AND Age <= 65),
    BirthDate DATE NOT NULL,
    Position NVARCHAR(100) NOT NULL,
    Department NVARCHAR(100) NOT NULL,
    HireDate DATETIME NOT NULL DEFAULT GETDATE(),
    Salary DECIMAL(10,2) NOT NULL CHECK (Salary >= 0),
    Email NVARCHAR(256) UNIQUE NULL,
    PhoneNumber NVARCHAR(20) UNIQUE NULL,
    Address NVARCHAR(512) NULL,
    FullName AS (FirstName + ' ' + LastName)
);
GO


INSERT INTO Employees2 
(FirstName, LastName, Age, BirthDate, Position, Department, Salary, Email, PhoneNumber, Address)
VALUES 
('Ivan', 'Tkachenko', 29, '1994-06-10', 'QA Engineer', 'IT', 60000, 'ivan.tkachenko@gmail.com', '+380501112238', 'Kyiv, Ukraine');
GO



ALTER TABLE Employees2 DROP CONSTRAINT IF EXISTS UQ_Email;
ALTER TABLE Employees2 DROP CONSTRAINT IF EXISTS UQ_Phone;
GO


DECLARE @i INT = 1;
DECLARE @N INT = 5;  

WHILE @i <= @N
BEGIN
    INSERT INTO Employees2 
    (FirstName, LastName, Age, BirthDate, Position, Department, Salary)
    VALUES 
    (
        'TestName' + CAST(@i AS NVARCHAR),
        'TestLast' + CAST(@i AS NVARCHAR),
        20 + @i,
        DATEADD(YEAR, -(20 + @i), GETDATE()),
        'Intern',
        'HR',
        30000 + (@i * 1000)
    );
    SET @i = @i + 1;
END;
GO



SELECT * FROM Employees2;


SELECT TOP 3 * FROM Employees2 ORDER BY Salary;


SELECT * FROM Employees2 WHERE Department = 'IT';


SELECT * FROM Employees2 WHERE Salary > 60000;


SELECT * FROM Employees2 WHERE Position LIKE '%Engineer%';


SELECT * FROM Employees2 WHERE Email IS NULL;


SELECT * FROM Employees2 WHERE Email IS NOT NULL;


SELECT * FROM EMPLOYEES2 WHERE DEPARTMENT = 'IT' OR DEPARTMENT = 'HR';  

SELECT * FROM Employees2 ORDER BY Age ASC OFFSET 2 ROW FETCH NEXT 3 ROW ONLY;


SELECT * FROM Employees2 WHERE NOT Department = 'Design';


UPDATE Employees2 SET Salary = Salary + 5000 WHERE Department = 'IT';


UPDATE Employees2 SET Email = 'unknown@gmail.com' WHERE Email IS NULL;


UPDATE Employees2 SET Department = 'Management' WHERE Position = 'Project Manager';


UPDATE Employees2 SET Salary = Salary * 1.1 WHERE Salary BETWEEN 50000 AND 70000;
UPDATE Employees2 SET Salary = Salary * 1.1 WHERE Salary > 50000 AND Salary < 70000;


DELETE FROM Employees2 WHERE Age < 21;


DELETE FROM Employees2 WHERE Salary < 30000;


DELETE FROM Employees2 WHERE Department = 'HR';


SELECT TOP 5 * FROM Employees2 WHERE Salary > 60000 ORDER BY Salary DESC;


SELECT * FROM Employees2 WHERE (Department = 'IT' OR Department = 'Design') AND Salary > 65000;


SELECT * FROM Employees2 WHERE NOT (Salary < 50000 OR Age < 25);
GO