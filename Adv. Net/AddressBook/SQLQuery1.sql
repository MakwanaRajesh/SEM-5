CREATE DATABASE AddressBook_203;
GO
GO


USE AddressBook_203

-- DROP IF EXISTS (optional, for re-running safely)
IF OBJECT_ID('Users', 'U') IS NOT NULL DROP TABLE Users;
GO

-- 1. Create Users Table
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    UserName VARCHAR(100) NOT NULL,
    MobileNo VARCHAR(20) NOT NULL,
    EmailId VARCHAR(100) NOT NULL
);
GO

-- 2. Insert Sample Records
INSERT INTO Users (UserName, MobileNo, EmailId)
VALUES 
('Rohit Sharma', '9876543210', 'rohit@example.com'),
('Jasprit Bumrah', '9123456780', 'bumrah@example.com'),
('Hardik Pandya', '9908896562', 'hardik@example.com'),
('Virat Kohli', '9812345678', 'virat@example.com'),
('KL Rahul', '9898989898', 'rahul@example.com'),
('Shubman Gill', '9786453120', 'gill@example.com'),
('Suryakumar Yadav', '9345678123', 'sky@example.com'),
('Ravindra Jadeja', '9654321789', 'jadeja@example.com'),
('Mohammed Shami', '9234567890', 'shami@example.com'),
('Yuzvendra Chahal', '9109876543', 'chahal@example.com');
GO

-- 3. Procedure to Insert User
CREATE OR ALTER PROCEDURE PR_User_Insert
    @UserName VARCHAR(100),
    @MobileNo VARCHAR(20),
    @EmailId VARCHAR(100)
AS
BEGIN
    INSERT INTO Users (UserName, MobileNo, EmailId)
    VALUES (@UserName, @MobileNo, @EmailId);
END;
GO

-- 4. Procedure to Select All Users
CREATE OR ALTER PROCEDURE Pr_User_SelectAll
AS
BEGIN
    SELECT UserId, UserName, MobileNo, EmailId
    FROM Users;
END;
GO

-- 5. Procedure to Delete User by Id
CREATE OR ALTER PROCEDURE Pr_User_Delete
    @UserId INT
AS
BEGIN
    DELETE FROM Users WHERE UserId = @UserId;
END;
GO


EXEC Pr_User_SelectAll;  -- To list all users
EXEC PR_User_Insert 'Test User', '1234567890', 'test@example.com'; -- To insert
EXEC Pr_User_Delete 1;   -- To delete user with UserId = 1







----Country --------------------------



-- 3. Create Country table
CREATE TABLE Country (
    CountryId INT PRIMARY KEY IDENTITY(1,1),
    CountryName VARCHAR(100) NOT NULL,
    CountryCode VARCHAR(10) NOT NULL,
    UserId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId)
);
GO

INSERT INTO Country (CountryName, CountryCode, UserId)
VALUES 
('India', 'IN', 1),
('United States', 'US', 2),
('Australia', 'AU', 3),
('England', 'GB', 4),
('South Africa', 'ZA', 5),
('New Zealand', 'NZ', 6),
('Pakistan', 'PK', 7),
('Bangladesh', 'BD', 8),
('Sri Lanka', 'LK', 9),
('Afghanistan', 'AF', 10);

-- 4. Insert Sample Data
INSERT INTO Country (CountryName, CountryCode, UserId)
VALUES 
('India', 'IN', 1),
('United States', 'US', 2),
('Canada', 'CA', 3),
('Australia', 'AU', 4),
('Germany', 'DE', 5),
('France', 'FR', 6),
('Japan', 'JP', 7),
('Brazil', 'BR', 8),
('South Africa', 'ZA', 9),
('United Kingdom', 'UK', 10);
GO

-- 5. PR_Country_Insert
CREATE OR ALTER PROCEDURE PR_Country_Insert
    @CountryName VARCHAR(100),
    @CountryCode VARCHAR(10),
    @UserId INT
AS
BEGIN
    INSERT INTO Country (CountryName, CountryCode, UserId)
    VALUES (@CountryName, @CountryCode, @UserId);
END;
GO

-- 6. PR_Country_Update
CREATE OR ALTER PROCEDURE PR_Country_Update
    @CountryId INT,
    @CountryName VARCHAR(100),
    @CountryCode VARCHAR(10),
    @UserId INT
AS
BEGIN
    UPDATE Country
    SET CountryName = @CountryName,
        CountryCode = @CountryCode,
        UserId = @UserId
    WHERE CountryId = @CountryId;
END;
GO

-- 7. PR_Country_Delete
CREATE OR ALTER PROCEDURE PR_Country_Delete
    @CountryId INT
AS
BEGIN
    DELETE FROM Country WHERE CountryId = @CountryId;
END;
GO

-- 8. PR_Country_SelectAll
CREATE OR ALTER PROCEDURE PR_Country_SelectAll
AS
BEGIN
    SELECT 
        c.CountryId,
        c.CountryName,
        c.CountryCode,
        u.UserName
    FROM Country c
    INNER JOIN Users u ON c.UserId = u.UserId;
END;
GO

-- 9. PR_Country_SelectByID
CREATE OR ALTER PROCEDURE PR_Country_SelectByID
    @CountryId INT
AS
BEGIN
    SELECT 
        CountryId, 
        CountryName, 
        CountryCode, 
        UserId
    FROM Country
    WHERE CountryId = @CountryId;
END;
GO

-- 10. PR_User_Dropdown
CREATE OR ALTER PROCEDURE PR_User_Dropdown
AS
BEGIN
    SELECT UserId, UserName FROM Users;
END;
GO






-- 4. Create State table
CREATE TABLE State (
    StateId INT PRIMARY KEY IDENTITY(1,1),
    StateName VARCHAR(100) NOT NULL,
    CountryId INT NOT NULL FOREIGN KEY REFERENCES Country(CountryId),
    StateCode VARCHAR(10) NOT NULL,
    UserId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId)
);
GO

-- 5. Insert sample data into State
INSERT INTO State (StateName, CountryId, StateCode, UserId)
VALUES 
('Gujarat', 1, 'GJ', 1),
('California', 2, 'CA', 2),
('New South Wales', 3, 'NSW', 3),
('Yorkshire', 4, 'YKS', 4),
('Gauteng', 5, 'GT', 5),
('Auckland', 6, 'AKL', 6),
('Punjab', 7, 'PB', 7),
('Dhaka', 8, 'DHK', 8),
('Colombo', 9, 'CLM', 9),
('Kabul Province', 10, 'KBL', 10);

GO

-- 6. PR_State_Insert
CREATE OR ALTER PROCEDURE PR_State_Insert
    @StateName VARCHAR(100),
    @CountryId INT,
    @StateCode VARCHAR(10),
    @UserId INT
AS
BEGIN
    INSERT INTO State (StateName, CountryId, StateCode, UserId)
    VALUES (@StateName, @CountryId, @StateCode, @UserId);
END
GO

-- 7. PR_State_Update
CREATE OR ALTER PROCEDURE PR_State_Update
    @StateId INT,
    @StateName VARCHAR(100),
    @CountryId INT,
    @StateCode VARCHAR(10),
    @UserId INT
AS
BEGIN
    UPDATE State
    SET StateName = @StateName,
        CountryId = @CountryId,
        StateCode = @StateCode,
        UserId = @UserId
    WHERE StateId = @StateId;
END
GO

-- 8. PR_State_Delete
CREATE OR ALTER PROCEDURE PR_State_Delete
    @StateId INT
AS
BEGIN
    DELETE FROM State WHERE StateId = @StateId;
END
GO

-- 9. PR_State_SelectAll
CREATE OR ALTER PROCEDURE PR_State_SelectAll
AS
BEGIN
    SELECT 
        s.StateId,
        s.StateName,
        s.StateCode,
        c.CountryName,
        u.UserName
    FROM State s
    INNER JOIN Country c ON s.CountryId = c.CountryId
    INNER JOIN Users u ON s.UserId = u.UserId;
END
GO

-- 10. PR_State_SelectById
CREATE OR ALTER PROCEDURE PR_State_SelectById
    @StateId INT
AS
BEGIN
    SELECT 
        StateId,
        StateName,
        CountryId,
        StateCode,
        UserId
    FROM State
    WHERE StateId = @StateId;
END
GO

-- 11. PR_Country_Dropdown
CREATE OR ALTER PROCEDURE PR_Country_Dropdown
AS
BEGIN
    SELECT CountryId, CountryName FROM Country;
END
GO






--- city ----------



CREATE TABLE City (
    CityId INT PRIMARY KEY IDENTITY(1,1),
    CityName VARCHAR(100) NOT NULL,
    CountryId INT FOREIGN KEY REFERENCES Country(CountryId),
    StateId INT FOREIGN KEY REFERENCES State(StateId),
    StdCode VARCHAR(10) NOT NULL,
    PinCode VARCHAR(10) NOT NULL,
    UserId INT FOREIGN KEY REFERENCES Users(UserId)
);

-- Sample City data
INSERT INTO City (CityName, CountryId, StateId, StdCode, PinCode, UserId) VALUES
('Ahmedabad', 1, 1, '079', '380001', 1),
('Mumbai', 2, 2, '022', '400001', 2),
('Los Angeles', 2, 3, '213', '90001', 3),
('Toronto', 3, 4, '416', 'M5H', 1);




CREATE TABLE City (
    CityId INT PRIMARY KEY IDENTITY(1,1),
    CityName VARCHAR(100) NOT NULL,
    CountryId INT NOT NULL,
    StateId INT NOT NULL,
    StdCode VARCHAR(10) NOT NULL,
    PinCode VARCHAR(10) NOT NULL,
    UserId INT NOT NULL
);


CREATE OR ALTER PROCEDURE PR_City_Insert
    @CityName VARCHAR(100),
    @CountryId INT,
    @StateId INT,
    @StdCode VARCHAR(10),
    @PinCode VARCHAR(10),
    @UserId INT
AS
BEGIN
    INSERT INTO City (CityName, CountryId, StateId, StdCode, PinCode, UserId)
    VALUES (@CityName, @CountryId, @StateId, @StdCode, @PinCode, @UserId);
END



CREATE OR ALTER PROCEDURE PR_City_Update
    @CityId INT,
    @CityName VARCHAR(100),
    @CountryId INT,
    @StateId INT,
    @StdCode VARCHAR(10),
    @PinCode VARCHAR(10),
    @UserId INT
AS
BEGIN
    UPDATE City
    SET 
        CityName = @CityName,
        CountryId = @CountryId,
        StateId = @StateId,
        StdCode = @StdCode,
        PinCode = @PinCode,
        UserId = @UserId
    WHERE CityId = @CityId;
END


CREATE OR ALTER PROCEDURE Pr_City_Delete
    @CityId INT
AS
BEGIN
    DELETE FROM City WHERE CityId = @CityId;
END



CREATE OR ALTER PROCEDURE Pr_City_SelectAll
AS
BEGIN
    SELECT 
        c.CityId,
        c.CityName,
        s.StateName,
        cn.CountryName,
        c.StdCode,
        c.PinCode,
        u.UserName
    FROM 
        City c
        INNER JOIN State s ON c.StateId = s.StateId
        INNER JOIN Country cn ON c.CountryId = cn.CountryId
        INNER JOIN Users u ON c.UserId = u.UserId
END



CREATE OR ALTER PROCEDURE PR_City_SelectByID
    @CityId INT
AS
BEGIN
    SELECT 
        CityId,
        CityName,
        CountryId,
        StateId,
        StdCode,
        PinCode,
        UserId
    FROM City
    WHERE CityId = @CityId;
END


-- Country Dropdown
CREATE OR ALTER PROCEDURE PR_Country_Dropdown
AS
BEGIN
    SELECT CountryId, CountryName FROM Country;
END

-- State Dropdown
CREATE OR ALTER PROCEDURE PR_State_Dropdown
AS
BEGIN
    SELECT StateId, StateName FROM State;
END

-- User Dropdown
CREATE OR ALTER PROCEDURE PR_User_Dropdown
AS
BEGIN
    SELECT UserId, UserName FROM Users;
END













CREATE OR ALTER PROC PR_Question_CountByLevel
AS
BEGIN
	SELECT 
		Lvl.QuestionLevel,
		COUNT(Qst.QuestionId) AS TotalQuestions
	FROM 
		MST_QuestionLevel Lvl
	LEFT JOIN 
		MST_Question Qst ON Qst.QuestionLevelId = Lvl.QuestionLevelId
	GROUP BY 
		Lvl.QuestionLevel
END
