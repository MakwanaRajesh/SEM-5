USE Reastaurant


------------USER CREATE ---------------------------



CREATE TABLE NewUsers (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    ConfirmPassword NVARCHAR(100) NOT NULL,
    Mobile NVARCHAR(15) NOT NULL
);






-----------  Stored Procedure for New User Sign In

CREATE OR ALTER PROCEDURE PR_NewUsers_SignUp
    @Name NVARCHAR(100),
    @Email NVARCHAR(100),
    @Password NVARCHAR(100),
    @ConfirmPassword NVARCHAR(100),
    @Mobile NVARCHAR(15)
AS
BEGIN
    -- Optional: Prevent duplicate email registration
    IF EXISTS (SELECT 1 FROM NewUsers WHERE Email = @Email)
    BEGIN
        RAISERROR('Email already exists.', 16, 1);
        RETURN;
    END

    -- Insert new user
    INSERT INTO NewUsers (Name, Email, Password, ConfirmPassword, Mobile)
    VALUES (@Name, @Email, @Password, @ConfirmPassword, @Mobile);
END


-------- Stored Procedure for Existing User Login

CREATE OR ALTER PROC PR_NewUsers_Login
    @Email NVARCHAR(100),
    @Password NVARCHAR(100)
AS
BEGIN
    -- Step 1: Check if Email exists
    IF NOT EXISTS (SELECT 1 FROM NewUsers WHERE Email = @Email)
    BEGIN
        SELECT 'Invalid Email Address' AS ErrorMessage;
        RETURN;
    END

    -- Step 2: Check if Password matches for the given Email
    IF NOT EXISTS (SELECT 1 FROM NewUsers WHERE Email = @Email AND Password = @Password)
    BEGIN
        SELECT 'Incorrect Password' AS ErrorMessage;
        RETURN;
    END

    -- Step 3: Return user details on successful login
    SELECT 
        UserId, Name, Email, Mobile
		FROM NewUsers
		WHERE Email = @Email AND Password = @Password;
END


SELECT * FROM NewUsers ORDER BY UserId DESC;





--------- Menu Table ------------------


CREATE TABLE Menu (
    MenuId INT PRIMARY KEY IDENTITY(1,1),
    ItemName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    Price DECIMAL(10, 2) NOT NULL,
    IsAvailable BIT DEFAULT 1
);





CREATE PROCEDURE AddMenuItem
    @ItemName NVARCHAR(100),
    @Description NVARCHAR(255),
    @Price DECIMAL(10, 2),
    @IsAvailable BIT
AS
BEGIN
    INSERT INTO Menu (ItemName, Description, Price, IsAvailable)
    VALUES (@ItemName, @Description, @Price, @IsAvailable);
END;



CREATE PROCEDURE UpdateMenuItem
    @MenuId INT,
    @ItemName NVARCHAR(100),
    @Description NVARCHAR(255),
    @Price DECIMAL(10, 2),
    @IsAvailable BIT
AS
BEGIN
    UPDATE Menu
    SET ItemName = @ItemName,
        Description = @Description,
        Price = @Price,
        IsAvailable = @IsAvailable
    WHERE MenuId = @MenuId;
END;



CREATE PROCEDURE DeleteMenuItem
    @MenuId INT
AS
BEGIN
    DELETE FROM Menu WHERE MenuId = @MenuId;
END;




CREATE PROCEDURE GetAllMenuItems
AS
BEGIN
    SELECT * FROM Menu;
END;



----------- Order Table ---------------


CREATE TABLE Orders (
    OrderId INT PRIMARY KEY IDENTITY(1,1),
    OrderDate DATETIME DEFAULT GETDATE(),
    CustomerName NVARCHAR(100),
    TotalAmount DECIMAL(10, 2),
    PaymentStatus NVARCHAR(50)
);




CREATE PROCEDURE AddOrder
    @CustomerName NVARCHAR(100),
    @TotalAmount DECIMAL(10, 2),
    @PaymentStatus NVARCHAR(50)
AS
BEGIN
    INSERT INTO Orders (CustomerName, TotalAmount, PaymentStatus)
    VALUES (@CustomerName, @TotalAmount, @PaymentStatus);
END;



CREATE PROCEDURE UpdateOrder
    @OrderId INT,
    @CustomerName NVARCHAR(100),
    @TotalAmount DECIMAL(10, 2),
    @PaymentStatus NVARCHAR(50)
AS
BEGIN
    UPDATE Orders
    SET CustomerName = @CustomerName,
        TotalAmount = @TotalAmount,
        PaymentStatus = @PaymentStatus
    WHERE OrderId = @OrderId;
END;




CREATE PROCEDURE DeleteOrder
    @OrderId INT
AS
BEGIN
    DELETE FROM Orders WHERE OrderId = @OrderId;
END;




CREATE PROCEDURE GetAllOrders
AS
BEGIN
    SELECT * FROM Orders;
END;






--------------- Order Details --------------





CREATE TABLE OrderDetails (
    OrderDetailId INT PRIMARY KEY IDENTITY(1,1),
    OrderId INT FOREIGN KEY REFERENCES Orders(OrderId),
    MenuId INT FOREIGN KEY REFERENCES Menu(MenuId),
    Quantity INT NOT NULL,
    SubTotal DECIMAL(10, 2) NOT NULL
);





CREATE PROCEDURE AddOrderDetail
    @OrderId INT,
    @MenuId INT,
    @Quantity INT,
    @SubTotal DECIMAL(10, 2)
AS
BEGIN
    INSERT INTO OrderDetails (OrderId, MenuId, Quantity, SubTotal)
    VALUES (@OrderId, @MenuId, @Quantity, @SubTotal);
END;






CREATE PROCEDURE UpdateOrderDetail
    @OrderDetailId INT,
    @Quantity INT,
    @SubTotal DECIMAL(10, 2)
AS
BEGIN
    UPDATE OrderDetails
    SET Quantity = @Quantity,
        SubTotal = @SubTotal
    WHERE OrderDetailId = @OrderDetailId;
END;



CREATE PROCEDURE DeleteOrderDetail
    @OrderDetailId INT
AS
BEGIN
    DELETE FROM OrderDetails WHERE OrderDetailId = @OrderDetailId;
END;




CREATE PROCEDURE GetOrderDetailsByOrderId
    @OrderId INT
AS
BEGIN
    SELECT OD.*, M.ItemName, M.Price
    FROM OrderDetails OD
    INNER JOIN Menu M ON OD.MenuId = M.MenuId
    WHERE OD.OrderId = @OrderId;
END;





