Create database Restaurant
use Restaurant


-- 1. Users Table
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY,
    UserName NVARCHAR(100),
    Email NVARCHAR(100),
    Password NVARCHAR(100)
);

-- 2. Categories Table
CREATE TABLE Categories (
    CategoryId INT PRIMARY KEY IDENTITY,
    CategoryName NVARCHAR(100)
);

-- 3. MenuItems Table
CREATE TABLE MenuItems (
    MenuItemId INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Description NVARCHAR(300),
    Price DECIMAL(10,2),
    ImagePath NVARCHAR(200),
    CategoryId INT FOREIGN KEY REFERENCES Categories(CategoryId)
);

-- 4. Orders Table
CREATE TABLE Orders (
    OrderId INT PRIMARY KEY IDENTITY,
    UserId INT FOREIGN KEY REFERENCES Users(UserId),
    OrderDate DATETIME DEFAULT GETDATE()
);

-- 5. OrderDetails Table
CREATE TABLE OrderDetails (
    OrderDetailId INT PRIMARY KEY IDENTITY,
    OrderId INT FOREIGN KEY REFERENCES Orders(OrderId),
    MenuItemId INT FOREIGN KEY REFERENCES MenuItems(MenuItemId),
    Quantity INT,
    TotalPrice AS (Quantity * (SELECT Price FROM MenuItems WHERE MenuItems.MenuItemId = OrderDetails.MenuItemId))
);

CREATE TABLE OrderDetails (
    OrderDetailId INT PRIMARY KEY IDENTITY,
    OrderId INT FOREIGN KEY REFERENCES Orders(OrderId),
    MenuItemId INT FOREIGN KEY REFERENCES MenuItems(MenuItemId),
    Quantity INT,
    Price DECIMAL(10, 2),  -- Store price at time of order
    TotalPrice AS (Quantity * Price) PERSISTED  -- Now this works
);





CREATE PROCEDURE AddOrder
    @UserId INT,
    @MenuItemId INT,
    @Quantity INT
AS
BEGIN
    DECLARE @OrderId INT

    -- Create new order
    INSERT INTO Orders (UserId) VALUES (@UserId)
    SET @OrderId = SCOPE_IDENTITY()

    -- Insert order item
    INSERT INTO OrderDetails (OrderId, MenuItemId, Quantity)
    VALUES (@OrderId, @MenuItemId, @Quantity)
END




CREATE PROCEDURE UpdateOrderDetail
    @OrderDetailId INT,
    @Quantity INT
AS
BEGIN
    UPDATE OrderDetails
    SET Quantity = @Quantity
    WHERE OrderDetailId = @OrderDetailId
END







CREATE PROCEDURE DeleteOrderDetail
    @OrderDetailId INT
AS
BEGIN
    DELETE FROM OrderDetails
    WHERE OrderDetailId = @OrderDetailId
END






ALTER TABLE Orders
ADD IsPlaced BIT DEFAULT 0;


ALTER TABLE Users
ADD IsAdmin BIT DEFAULT 0;


UPDATE Users SET IsAdmin = 1 WHERE Email = 'admin@example.com';
