Create Database Cse_233_Addressbook

--------------------------City--------------------------
------------------Select All------------------
Create Or Alter Proc [dbo].[PR_City_SelectAll]
As
Begin
	Select [dbo].[City].[CityID],
		   [dbo].[State].[StateName],
		   [dbo].[Country].[CountryName],
		   [dbo].[City].[CityName],
		   [dbo].[City].[STDCode],
		   [dbo].[City].[PinCode],
		   [dbo].[City].[CreationDate],
		   [dbo].[User].[UserName]
	From [dbo].[City]
	Inner Join [dbo].[User]
	On [dbo].[City].[UserId] = [dbo].[User].[UserId]
	Inner Join [dbo].[State]
	On [dbo].[City].[StateId] = [dbo].[State].[StateId]
	Inner Join [dbo].[Country]
	On [dbo].[City].[CountryId] = [dbo].[Country].[CountryId]
End

-----------------Select By Id------------------
Create Or Alter Proc [dbo].[PR_City_SelectByID]
@CityID int
As
Begin
	Select [dbo].[City].[CityID],
		   [dbo].[City].[StateID],
		   [dbo].[City].[CountryID],
		   [dbo].[City].[CityName],
		   [dbo].[City].[STDCode],
		   [dbo].[City].[PinCode],
		   [dbo].[City].[CreationDate],
		   [dbo].[City].[UserId]
	From [dbo].[City]
	Where [dbo].[City].[CityID] = @CityID
End

----------------------Insert--------------------
Create Or Alter Proc [dbo].[PR_City_Insert]
@StateID int,
@CountryID int,
@CityName nvarchar(100),
@STDCode nvarchar(50),
@PinCode nvarchar(6),
@UserID int
As
Begin
	Insert Into [dbo].[City]
	(
		StateID,
		CountryID,
		CityName,
		STDCode,
		PinCode,
		UserID,
		CreationDate
	)
	Values
	(
		@StateID,
		@CountryID,
		@CityName,
		@STDCode,
		@PinCode,
		@UserID,
		GETDATE()
	)
End

----------------------Update--------------------
Create Or Alter Proc [dbo].[PR_City_Update]
@CityID int,
@StateID int,
@CountryID int,
@CityName nvarchar(100),
@STDCode nvarchar(50),
@PinCode nvarchar(6),
@UserID int
As
Begin
	Update [dbo].[City] 
	Set [dbo].[City].[StateID]  = @StateID,
		[dbo].[City].[CountryID] = @CountryID,
		[dbo].[City].[CityName]  = @CityName,
		[dbo].[City].[STDCode]   = @STDCode,
		[dbo].[City].[PinCode]   = @PinCode,
		[dbo].[City].[UserID]    = @UserID,
		[dbo].[City].[CreationDate] = GETDATE()
	Where [dbo].[City].[CityID] = @CityID
End

----------------------Delete--------------------
Create Or Alter Proc [dbo].[PR_City_Delete]
@CityID int
As
Begin
	Delete From [dbo].[City]
	Where [dbo].[City].[CityID] = @CityID
End

--------------------------------User------------------------------------
------------------Select All------------------
Create Or Alter Proc [dbo].[PR_User_SelectAll]
As
Begin
	Select [dbo].[User].[UserID],
		   [dbo].[User].[UserName],
		   [dbo].[User].[MobileNo],
		   [dbo].[User].[EmailID],
		   [dbo].[User].[CreationDate]
	From [dbo].[User]
End

-----------------Select By Id------------------
Create Or Alter Proc [dbo].[PR_User_SelectByID]
@UserID int
As
Begin
	Select [dbo].[User].[UserID],
		   [dbo].[User].[UserName],
		   [dbo].[User].[MobileNo],
		   [dbo].[User].[EmailID],
		   [dbo].[User].[CreationDate]
	From [dbo].[User]
	Where [dbo].[User].[UserID] = @UserID
End

----------------------Insert--------------------
Create Or Alter Proc [dbo].[PR_User_Insert]
@UserName nvarchar(100),
@MobileNo nvarchar(50),
@EmailID nvarchar(100)
As
Begin
	Insert Into [dbo].[User]
	(
		UserName,
		MobileNo,
		EmailID,
		CreationDate
	)
	Values
	(
		@UserName,
		@MobileNo,
		@EmailID,
		GETDATE()
	)
End

----------------------Update--------------------
Create Or Alter Proc [dbo].[PR_User_Update]
@UserID int,
@UserName nvarchar(100),
@MobileNo nvarchar(50),
@EmailID nvarchar(100)
As
Begin
	Update [dbo].[User] 
	Set [dbo].[User].[UserName]  = @UserName,
		[dbo].[User].[MobileNo]  = @MobileNo,
		[dbo].[User].[EmailID]   = @EmailID,
		[dbo].[User].[CreationDate] = GETDATE()
	Where [dbo].[User].[UserID] = @UserID
End

----------------------Delete--------------------
Create Or Alter Proc [dbo].[PR_User_Delete]
@UserID int
As
Begin
	Delete From [dbo].[User]
	Where [dbo].[User].[UserID] = @UserID
End


----------------------State------------------------
------------------Select All------------------
Create Or Alter Proc [dbo].[PR_State_SelectAll]
As
Begin
	Select [dbo].[State].[StateID],
		   [dbo].[Country].[CountryName],
		   [dbo].[State].[StateName],
		   [dbo].[State].[StateCode],
		   [dbo].[State].[CreationDate],
		   [dbo].[User].[UserName]
	From [dbo].[State]
	Inner Join [dbo].[User]
	On [dbo].[State].[UserId] = [dbo].[User].[UserId]
	Inner Join [dbo].[Country]
	On [dbo].[State].[CountryId] = [dbo].[Country].[CountryId]
End

-----------------Select By Id------------------
Create Or Alter Proc [dbo].[PR_State_SelectByID]
@StateID int
As
Begin
	Select [dbo].[State].[StateID],
		   [dbo].[State].[CountryID],
		   [dbo].[State].[StateName],
		   [dbo].[State].[StateCode],
		   [dbo].[State].[CreationDate],
		   [dbo].[State].[UserID]
	From [dbo].[State]
	Where [dbo].[State].[StateID] = @StateID
End

----------------------Insert--------------------
Create Or Alter Proc [dbo].[PR_State_Insert]
@CountryID int,
@StateName nvarchar(100),
@StateCode nvarchar(50),
@UserID int
As
Begin
	Insert Into [dbo].[State]
	(
		CountryID,
		StateName,
		StateCode,
		UserID,
		CreationDate
	)
	Values
	(
		@CountryID,
		@StateName,
		@StateCode,
		@UserID,
		GETDATE()
	)
End

----------------------Update--------------------
Create Or Alter Proc [dbo].[PR_State_Update]
@StateID int,
@CountryID int,
@StateName nvarchar(100),
@StateCode nvarchar(50),
@UserID int
As
Begin
	Update [dbo].[State] 
	Set [dbo].[State].[CountryID] = @CountryID,
		[dbo].[State].[StateName]  = @StateName,
		[dbo].[State].[StateCode]  = @StateCode,
		[dbo].[State].[UserID]    = @UserID,
		[dbo].[State].[CreationDate] = GETDATE()
	Where [dbo].[State].[StateID] = @StateID
End

----------------------Delete--------------------
Create Or Alter Proc [dbo].[PR_State_Delete]
@StateID int
As
Begin
	Delete From [dbo].[State]
	Where [dbo].[State].[StateID] = @StateID
End

----------------Country---------------------------
dbcc checkident ('Country', reseed,0)
------------------Select All------------------
Create Or Alter Proc [dbo].[PR_Country_SelectAll]
As
Begin
	Select [dbo].[Country].[CountryID],
		   [dbo].[Country].[CountryName],
		   [dbo].[Country].[CountryCode],
		   [dbo].[Country].[CreationDate],
		   [dbo].[User].[UserName]
	From [dbo].[Country]
	Inner Join [dbo].[User]
	On [dbo].[Country].[UserId] = [dbo].[User].[UserId]
End

-----------------Select By Id------------------
Create Or Alter Proc [dbo].[PR_Country_SelectByID]
@CountryID int
As
Begin
	Select [dbo].[Country].[CountryID],
		   [dbo].[Country].[CountryName],
		   [dbo].[Country].[CountryCode],
		   [dbo].[Country].[CreationDate],
		   [dbo].[Country].[UserID]
	From [dbo].[Country]
	Where [dbo].[Country].[CountryID] = @CountryID
End

----------------------Insert--------------------
Create Or Alter Proc [dbo].[PR_Country_Insert]
@CountryName nvarchar(100),
@CountryCode nvarchar(50),
@UserID int
As
Begin
	Insert Into [dbo].[Country]
	(
		CountryName,
		CountryCode,
		UserID,
		CreationDate
	)
	Values
	(
		@CountryName,
		@CountryCode,
		@UserID,
		GETDATE()
	)
End

----------------------Update--------------------
Create Or Alter Proc [dbo].[PR_Country_Update]
@CountryID int,
@CountryName nvarchar(100),
@CountryCode nvarchar(50),
@UserID int
As
Begin
	Update [dbo].[Country] 
	Set [dbo].[Country].[CountryName]  = @CountryName,
		[dbo].[Country].[CountryCode]  = @CountryCode,
		[dbo].[Country].[UserID]    = @UserID,
		[dbo].[Country].[CreationDate] = GETDATE()
	Where [dbo].[Country].[CountryID] = @CountryID
End

----------------------Delete--------------------
Create Or Alter Proc [dbo].[PR_Country_Delete]
@CountryID int
As
Begin
	Delete From [dbo].[Country]
	Where [dbo].[Country].[CountryID] = @CountryID
End



---------------------Dropdown-----------------
-------User------
Create Or Alter Proc [dbo].[PR_User_Dropdown]
As
Begin
	Select 
		[dbo].[User].[UserId],
		[dbo].[User].[UserName]
	From [dbo].[User]
End

-------Country------
Create Or Alter Proc [dbo].[PR_Country_Dropdown]
As
Begin
	Select 
		[dbo].[Country].[CountryID],
        [dbo].[Country].[CountryName]
	From [dbo].[Country]
End

-------State------
Create Or Alter Proc [dbo].[PR_State_Dropdown]
As
Begin
	Select
		[dbo].[State].[StateId],
		[dbo].[State].[StateName]
	From [dbo].[State]
End

-------User------
Create Or Alter Proc [dbo].[PR_City_Dropdown]
As
Begin
	Select 
		[dbo].[City].[CityId],
		[dbo].[City].[CityName]
	From [dbo].[CIty]
End

INSERT INTO [dbo].[User] (UserName, MobileNo, EmailID, CreationDate)
VALUES 
('Rahul Sharma', '9876543210', 'rahul.sharma@gmail.com', GETDATE()),
('Priya Agarwal', '9823456789', 'priya.agarwal@yahoo.com', GETDATE()),
('Arjun Verma', '9898123456', 'arjun.verma@outlook.com', GETDATE()),
('Neha Patel', '9876509876', 'neha.patel@rediffmail.com', GETDATE()),
('Vishal Kumar', '9812345678', 'vishal.kumar@indiatimes.com', GETDATE()),
('Sneha Nair', '9934567890', 'sneha.nair@hotmail.com', GETDATE()),
('Ananya Mishra', '9845098765', 'ananya.mishra@gmail.com', GETDATE()),
('Karan Singh', '9876123450', 'karan.singh@yahoo.com', GETDATE()),
('Ravi Chopra', '9812765432', 'ravi.chopra@outlook.com', GETDATE()),
('Meera Iyer', '9898989898', 'meera.iyer@rediffmail.com', GETDATE());



Select * From Country

INSERT INTO Country (CountryName, CountryCode, UserID, CreationDate)
VALUES 
('India', 'IN', 1, GETDATE()),
('United States', 'US', 2, GETDATE()),
('United Kingdom', 'UK', 3, GETDATE()),
('Canada', 'CA', 4, GETDATE()),
('Australia', 'AU', 5, GETDATE());




INSERT INTO [dbo].[State] (CountryID, StateName, StateCode, UserID, CreationDate)
VALUES 
(1, 'Gujarat', 'GJ', 1, GETDATE()),
(1, 'Maharashtra', 'MH', 2, GETDATE()),
(1, 'Karnataka', 'KA', 3, GETDATE()),
(1, 'Tamil Nadu', 'TN', 4, GETDATE()),
(1, 'Uttar Pradesh', 'UP', 5, GETDATE()),
(1, 'Rajasthan', 'RJ', 6, GETDATE()),
(1, 'West Bengal', 'WB', 7, GETDATE()),
(1, 'Madhya Pradesh', 'MP', 8, GETDATE()),
(1, 'Bihar', 'BR', 9, GETDATE()),
(1, 'Punjab', 'PB', 10, GETDATE());

select *from [dbo].[User]


INSERT INTO [dbo].[City] (StateID, CountryID, CityName, STDCode, PinCode, UserID, CreationDate)
VALUES 
(1, 1, 'Ahmedabad', '079', '380001', 1, GETDATE()),
(2, 1, 'Mumbai', '022', '400001', 2, GETDATE()),
(3, 1, 'Bengaluru', '080', '560001', 3, GETDATE()),
(4, 1, 'Chennai', '044', '600001', 4, GETDATE()),
(5, 1, 'Lucknow', '0522', '226001', 5, GETDATE()),
(6, 1, 'Jaipur', '0141', '302001', 6, GETDATE()),
(7, 1, 'Kolkata', '033', '700001', 7, GETDATE()),
(8, 1, 'Bhopal', '0755', '462001', 8, GETDATE()),
(9, 1, 'Patna', '0612', '800001', 9, GETDATE()),
(10, 1, 'Chandigarh', '0172', '160001', 10, GETDATE());


----------------Count--------------------------
Create Or Alter Proc PR_LOC_CountOfAll
As
Begin
	Select
		(Select Count(distinct CityId) From City) as CountCity,
		(Select	Count(distinct State.StateId) From State)  as CountState,
		(Select	Count(distinct Country.CountryId) From Country)  as CountCountry
End