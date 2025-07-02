create database Quiz_203
USE Quiz_203

-- Creating MST_User table
CREATE TABLE MST_User (
    UserID			INT PRIMARY KEY IDENTITY(1,1),
    UserName		NVARCHAR(100) NOT NULL,
    Password		NVARCHAR(100) NOT NULL,
    Email			NVARCHAR(100) NOT NULL,
    Mobile			NVARCHAR(100) NOT NULL,
    IsActive		BIT NOT NULL DEFAULT 1,
    IsAdmin			BIT NOT NULL DEFAULT 0,
    Created			DATETIME NOT NULL DEFAULT GETDATE(),
    Modified		DATETIME NOT NULL
);

-- Creating MST_Quiz table
CREATE TABLE MST_Quiz (
    QuizID			INT PRIMARY KEY IDENTITY(1,1),
    QuizName		NVARCHAR(100) NOT NULL,
    TotalQuestions	INT NOT NULL,
    QuizDate		DATETIME NOT NULL,
    UserID			INT NOT NULL,
    Created			DATETIME NOT NULL DEFAULT GETDATE(),
    Modified		DATETIME NOT NULL,
    FOREIGN KEY (UserID) REFERENCES MST_User(UserID)
);

-- Creating MST_Question table
CREATE TABLE MST_Question (
    QuestionID		INT PRIMARY KEY IDENTITY(1,1),
    QuestionText	NVARCHAR(MAX) NOT NULL,
    QuestionLevelID INT NOT NULL,
    OptionA			NVARCHAR(100) NOT NULL,
    OptionB			NVARCHAR(100) NOT NULL,
    OptionC			NVARCHAR(100) NOT NULL,
    OptionD			NVARCHAR(100) NOT NULL,
    CorrectOption	NVARCHAR(100) NOT NULL,
    QuestionMarks	INT NOT NULL,
    IsActive		BIT NOT NULL DEFAULT 1,
    UserID			INT NOT NULL,
    Created			DATETIME NOT NULL DEFAULT GETDATE(),
    Modified		DATETIME NOT NULL,
    FOREIGN KEY (UserID) REFERENCES MST_User(UserID)
);

-- Creating MST_QuestionLevel table
CREATE TABLE MST_QuestionLevel (
    QuestionLevelID INT PRIMARY KEY IDENTITY(1,1),
    QuestionLevel	NVARCHAR(100) NOT NULL,
    UserID			INT NOT NULL,
    Created			DATETIME NOT NULL DEFAULT GETDATE(),
    Modified		DATETIME NOT NULL,
    FOREIGN KEY (UserID) REFERENCES MST_User(UserID)
);

-- Creating MST_QuizWiseQuestions table
CREATE TABLE MST_QuizWiseQuestions (
    QuizWiseQuestionsID INT PRIMARY KEY IDENTITY(1,1),
    QuizID				INT NOT NULL,
    QuestionID			INT NOT NULL,
    UserID				INT NOT NULL,
    Created				DATETIME NOT NULL DEFAULT GETDATE(),
    Modified			DATETIME NOT NULL,
    FOREIGN KEY (QuizID) REFERENCES MST_Quiz(QuizID),
    FOREIGN KEY (QuestionID) REFERENCES MST_Question(QuestionID),
    FOREIGN KEY (UserID) REFERENCES MST_User(UserID)
);


-----------------------------------------Procedure---------------------------------------------

-----------------------------------<------Procedure------>---------------------------------------
------------------------------Stored Procedures for MST_User Table -----------------------------
--Stored Procedures for MST_User Table Insert
--EXEC PR_MST_User_Insert 'haril','xyz','harilparmar@gmail.com','9023960415'
CREATE OR ALTER PROCEDURE PR_MST_User_Insert
    @UserName	NVARCHAR(100),
    @Password	NVARCHAR(100),
    @Email		NVARCHAR(100),
    @Mobile		NVARCHAR(100)
AS
BEGIN
	DECLARE @Modified DATETIME = GETDATE();
    INSERT INTO [dbo].[MST_User] 
    (
		UserName,
		Password,
		Email,	
		Mobile,
		Modified
    )
    VALUES
    (
        @UserName,
		@Password,
		@Email,
		@Mobile,	
		@Modified
    );
END;

exec PR_MST_User_Insert 'Rajesh','Rajesh@123','rajesh@gmail.com','9876543210'

--Stored Procedures for MST_User Table Update
--EXEC PR_User_Update '1','SAMIR','HARIL','hp','8780031119','1','0'
CREATE OR ALTER PROC PR_MST_User_Update
	@UserID		INT,
	@UserName	NVARCHAR(100),
	@Email		NVARCHAR(100),
	@Password	NVARCHAR(100),
	@Mobile		NVARCHAR(100),
	@IsActive	BIT,
    @IsAdmin	BIT
AS
BEGIN
	DECLARE @Modified Datetime = Getdate()
	UPDATE [dbo].[MST_User]
	SET 
		[dbo].[MST_User].[UserName]		=	@UserName,
		[dbo].[MST_User].[Email]		=	@Email,
		[dbo].[MST_User].[Password]		=	@Password,
		[dbo].[MST_User].[Mobile]		=	@Mobile,
		[dbo].[MST_User].[IsActive]		=	@IsActive,
		[dbo].[MST_User].[IsAdmin]		=	@IsAdmin,
		[dbo].[MST_User].[Modified]		=	@Modified
	WHERE [dbo].[MST_User].[UserID]		=	@UserID
END

--Stored Procedures for MST_User Table Delete
-- EXEC PR_MST_User_Delete 1
CREATE OR ALTER PROC PR_MST_User_Delete
    @UserID INT
AS
BEGIN
    DELETE FROM [dbo].[MST_User]
    WHERE UserID = @UserID
END

--Stored Procedures for MST_User Table Select All
-- EXEC PR_MST_User_SelectAll
CREATE OR ALTER PROC PR_MST_User_SelectAll
AS
BEGIN
	SELECT 
		[dbo].[MST_User].[UserID],               
		[dbo].[MST_User].[UserName],			
		[dbo].[MST_User].[Password],			
		[dbo].[MST_User].[Email],				
		[dbo].[MST_User].[Mobile],
		[dbo].[MST_User].[IsActive],
		[dbo].[MST_User].[IsAdmin],
		[dbo].[MST_User].[Created],				
		[dbo].[MST_User].[Modified]				
	FROM [dbo].[MST_User]						
END												
												
--Stored Procedures for MST_User Table Select By Id
-- EXEC PR_MST_User_SelectByID 2
CREATE OR ALTER PROC PR_MST_User_SelectByID
@UserID INT
AS
BEGIN
	SELECT 
		[dbo].[MST_User].[UserID],  
		[dbo].[MST_User].[UserName],
		[dbo].[MST_User].[Password],
		[dbo].[MST_User].[Email],	
		[dbo].[MST_User].[Mobile],
		[dbo].[MST_User].[IsActive],
		[dbo].[MST_User].[IsAdmin],
		[dbo].[MST_User].[Created],	
		[dbo].[MST_User].[Modified]
	FROM [dbo].[MST_User]
	WHERE [dbo].[MST_User].[UserID] = @UserID
END

-- Stored Procedure For User Login Access
--Store Procedure for MST_User Table Login
--Exec PR_MST_User_Login 'Patel Balkrushna','Patel@123'
CREATE OR ALTER PROC PR_MST_User_Login
    @UserName  NVARCHAR(100),
    @Password  NVARCHAR(100)
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM [dbo].[MST_User] WHERE [UserName] = @UserName)
    BEGIN
        SELECT 'Invalid Username' AS ErrorMessage;
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM [dbo].[MST_User] WHERE [UserName] = @UserName AND [Password] = @Password)
    BEGIN
        SELECT 'Incorrect Password' AS ErrorMessage;
        RETURN;
    END

    -- If both username and password are correct, return user details
    SELECT 
        [UserID],
        [UserName],
        [Mobile],
        [Email]
    FROM [dbo].[MST_User]
    WHERE [UserName] = @UserName AND [Password] = @Password;
END


-------------------------------Stored Procedures for MST_Quiz Table----------------------------
--Stored Procedures for MST_Quiz Table Insert
--EXEC PR_MST_Quiz_Insert 'ABC',10,3,'2020-10-10'
CREATE OR ALTER PROC PR_MST_Quiz_Insert
    @QuizName			NVARCHAR(100),    	
    @TotalQuestions		INT,	
    @UserID				INT,								
    @QuizDate			DATETIME				
AS								
BEGIN
	DECLARE @Modified DATETIME = GETDATE()
    INSERT INTO [dbo].[MST_Quiz] 
    (
        QuizName,
        TotalQuestions,
        QuizDate,
        UserID,
        Modified
    )
    VALUES
    (
        @QuizName,
        @TotalQuestions,
        @QuizDate,
        @UserID,
        @Modified
    )
END

--Stored Procedures for MST_Quiz Table Update
-- Exec PR_MST_Quiz_Update 9,'ASD',12,'2020-10-10',2
CREATE OR ALTER PROC PR_MST_Quiz_Update
    @QuizID				INT,
    @QuizName			NVARCHAR(100),
    @TotalQuestions		INT,
    @QuizDate			DATETIME,
	@UserID				INT
AS
BEGIN 
	DECLARE @Modified DATETIME = GETDATE()
    UPDATE [dbo].[MST_Quiz]
    SET
        [dbo].[MST_Quiz].[QuizName]			=	@QuizName,
        [dbo].[MST_Quiz].[TotalQuestions]	=	@TotalQuestions,
        [dbo].[MST_Quiz].[QuizDate]			=	@QuizDate,
        [dbo].[MST_Quiz].[Modified]			=	@Modified,
		[dbo].[MST_Quiz].[UserID]			=	@UserID
    WHERE [dbo].[MST_Quiz].[QuizID]			=	@QuizID
END

--Stored Procedures for MST_Quiz Table Delete
-- EXEC PR_MST_Quiz_Delete 9
CREATE OR ALTER PROC PR_MST_Quiz_Delete
	@QuizID INT
AS
BEGIN
	DELETE FROM [dbo].[MST_Quiz]
	WHERE [dbo].[MST_Quiz].[QuizID] = @QuizID
END

--Stored Procedures for MST_Quiz Table Select All
-- EXEC PR_MST_Quiz_SelectAll
CREATE OR ALTER PROC PR_MST_Quiz_SelectAll
AS
BEGIN
    SELECT 
        [dbo].[MST_Quiz].[QuizID],
        [dbo].[MST_Quiz].[QuizName],
        [dbo].[MST_Quiz].[TotalQuestions],
		[dbo].[MST_Quiz].[QuizDate],
        [dbo].[MST_User].[UserName],
		[dbo].[MST_User].[Created],
        [dbo].[MST_Quiz].[Modified]
	From [dbo].[MST_Quiz] join [dbo].[MST_User]
	On [dbo].[MST_Quiz].[UserID] = [dbo].[MST_User].[UserID]
END

-- Stored Procedure for Selecting a Quiz by QuizID
-- EXEC PR_MST_Quiz_SelectByID 1
CREATE OR ALTER PROC PR_MST_Quiz_SelectByID
@QuizID INT
AS
BEGIN
    SELECT 
        [dbo].[MST_Quiz].[QuizID],
        [dbo].[MST_Quiz].[QuizName],
        [dbo].[MST_Quiz].[QuizDate],
        [dbo].[MST_Quiz].[Created],
        [dbo].[MST_Quiz].[Modified]
    FROM [dbo].[MST_Quiz]
    WHERE [dbo].[MST_Quiz].[QuizID] = @QuizID
END

----------------------- Stored Procedures for MST_Question Table ------------------------------
--Stored Procedures for MST_Question Table Insert
-- EXEC PR_MST_Question_Insert 'EWFSD',1,'A','B','C','D','ANS',1,2
CREATE OR ALTER PROC PR_MST_Question_Insert
    @QuestionText			NVARCHAR(MAX),
    @QuestionLevelID		INT,
    @OptionA				NVARCHAR(100),
    @OptionB				NVARCHAR(100),
    @OptionC				NVARCHAR(100),
    @OptionD				NVARCHAR(100),
    @CorrectOption			NVARCHAR(100),
    @QuestionMarks			INT,
    @UserID					INT
AS
BEGIN
	DECLARE @IsActive BIT = 1 , @Modified DATETIME = GETDATE()
	
    INSERT INTO [dbo].[MST_Question] 
    (
        QuestionText,
        QuestionLevelID,
        OptionA,
        OptionB,
        OptionC,
        OptionD,
        CorrectOption,
        QuestionMarks,
		IsActive,
        UserID,
        Modified
    )
    VALUES
    (
        @QuestionText,
        @QuestionLevelID,
        @OptionA,
        @OptionB,
        @OptionC,
        @OptionD,
        @CorrectOption,
        @QuestionMarks,
		@IsActive,
        @UserID,
        @Modified
    )
END


-- Stored Procedure for MST_Question Update
-- EXEC PR_MST_Question_Update 1,'TEXT','AB','BC','CD','DA','ABCDDCBA',5,5,2
CREATE OR ALTER PROC PR_MST_Question_Update
	@QuestionID			INT,
	@QuestionText		NVARCHAR(MAX),
	@OptionA			NVARCHAR(500),
	@OptionB			NVARCHAR(500),
	@OptionC			NVARCHAR(500),
	@OptionD			NVARCHAR(500),
	@CorrectOption		NVARCHAR(500),
	@QuestionMarks		INT,
	@QuestionLevelID	INT,
	@UserID				INT
AS
BEGIN
	DECLARE @Modified DATETIME = GETDATE()
    UPDATE [dbo].[MST_Question]
    SET 
        [dbo].[MST_Question].[QuestionText]			=	@QuestionText,
        [dbo].[MST_Question].[OptionA]				=	@OptionA,
        [dbo].[MST_Question].[OptionB]				=	@OptionB,
        [dbo].[MST_Question].[OptionC]				=	@OptionC,
        [dbo].[MST_Question].[OptionD]				=	@OptionD,
        [dbo].[MST_Question].[CorrectOption]		=	@CorrectOption, 
        [dbo].[MST_Question].[QuestionMarks]		=	@QuestionMarks,
		[dbo].[MST_Question].[QuestionLevelID]		=	@QuestionLevelID,
        [dbo].[MST_Question].[UserID]				=	@UserID,
        [dbo].[MST_Question].[Modified]				=	@Modified
    WHERE [dbo].[MST_Question].[QuestionID]			=	@QuestionID
END


-- Stored Procedure for MST_Question Delete
-- EXEC PR_MST_Question_Delete 2
CREATE OR ALTER PROC PR_MST_Question_Delete
@QuestionID INT
AS
BEGIN
    DELETE FROM [dbo].[MST_Question]
    WHERE [dbo].[MST_Question].[QuestionID] = @QuestionID
END

--Stored Procedures for MST_Question Select All
-- EXEC PR_MST_Question_SelectAll
CREATE OR ALTER PROC PR_MST_Question_SelectAll
AS
BEGIN
    SELECT 
        [dbo].[MST_Question].[QuestionID],
        [dbo].[MST_Question].[QuestionText],
        [dbo].[MST_Question].[QuestionLevelID],
        [dbo].[MST_Question].[OptionA],
        [dbo].[MST_Question].[OptionB],
        [dbo].[MST_Question].[OptionC],
        [dbo].[MST_Question].[OptionD],
        [dbo].[MST_Question].[CorrectOption],
        [dbo].[MST_Question].[QuestionMarks],
        [dbo].[MST_Question].[IsActive],
        [dbo].[MST_Question].[Created],
        [dbo].[MST_Question].[Modified],
        [dbo].[MST_QuestionLevel].[QuestionLevel],
        [dbo].[MST_User].[UserName]
    FROM [dbo].[MST_Question]
    INNER JOIN [dbo].[MST_QuestionLevel] ON [dbo].[MST_Question].[QuestionLevelID] = [dbo].[MST_QuestionLevel].[QuestionLevelID]
    INNER JOIN [dbo].[MST_User] ON [dbo].[MST_Question].[UserID] = [dbo].[MST_User].[UserID]
END

-- Stored Procedure for MST_Question Search And Filter
CREATE OR ALTER PROC PR_MST_Question_SelectAll_Search_Filter
@UserID INT,
@QuestionLevel VARCHAR(100) = NULL,
@QuestionText VARCHAR(100) = NULL,
@QuestionID INT
AS
BEGIN
    SELECT 
        [dbo].[MST_Question].[QuestionID],
        [dbo].[MST_Question].[QuestionText],
        [dbo].[MST_Question].[QuestionLevelID],
        [dbo].[MST_Question].[OptionA],
        [dbo].[MST_Question].[OptionB],
        [dbo].[MST_Question].[OptionC],
        [dbo].[MST_Question].[OptionD],
        [dbo].[MST_Question].[CorrectOption],
        [dbo].[MST_Question].[QuestionMarks],
        [dbo].[MST_Question].[IsActive],
        [dbo].[MST_Question].[Created],
        [dbo].[MST_Question].[Modified],
        [dbo].[MST_QuestionLevel].[QuestionLevel],
        [dbo].[MST_User].[UserName]
    FROM [dbo].[MST_Question]
    INNER JOIN [dbo].[MST_QuestionLevel] ON [dbo].[MST_Question].[QuestionLevelID] = [dbo].[MST_QuestionLevel].[QuestionLevelID]
    INNER JOIN [dbo].[MST_User] ON [dbo].[MST_Question].[UserID] = [dbo].[MST_User].[UserID]
	WHERE [dbo].[MST_Question].[UserID] = @UserID
		AND ([dbo].[MST_Question].[QuestionText] LIKE '%' + @QuestionText + '%' OR @QuestionText IS NULL)
        AND ([dbo].[MST_Question].[QuestionID] LIKE '%' + @QuestionID + '%' OR @QuestionID IS NULL)
END

-- Stored Procedure for MST_Question Select by ID
--EXEC PR_MST_Question_SelectByID 1
CREATE OR ALTER PROC PR_MST_Question_SelectByID
	@QuestionID INT
AS
BEGIN
    SELECT 
        [dbo].[MST_Question].[QuestionID],
        [dbo].[MST_Question].[QuestionText],
        [dbo].[MST_Question].[QuestionLevelID],
        [dbo].[MST_Question].[OptionA],
        [dbo].[MST_Question].[OptionB],
        [dbo].[MST_Question].[OptionC],
        [dbo].[MST_Question].[OptionD],
        [dbo].[MST_Question].[CorrectOption],
        [dbo].[MST_Question].[QuestionMarks],
        [dbo].[MST_Question].[IsActive],
        [dbo].[MST_Question].[Created],
        [dbo].[MST_Question].[Modified],
        [dbo].[MST_QuestionLevel].[QuestionLevel],
        [dbo].[MST_User].[UserName]
    FROM [dbo].[MST_Question]
    INNER JOIN [dbo].[MST_QuestionLevel] ON [dbo].[MST_Question].[QuestionLevelID] = [dbo].[MST_QuestionLevel].[QuestionLevelID]
    INNER JOIN [dbo].[MST_User] ON [dbo].[MST_Question].[UserID] = [dbo].[MST_User].[UserID]
    WHERE [dbo].[MST_Question].[QuestionID] = @QuestionID
END

------------------------------- Stored Procedures for MST_QuestionLevel Table --------------------
-- Stored Procedures for MST_QuestionLevel Table Insert
-- EXEC PR_MST_QuestionLevel_Insert 'Medium',4
CREATE OR ALTER PROC PR_MST_QuestionLevel_Insert
    @QuestionLevel		NVARCHAR(100),
    @UserID				INT
AS
BEGIN
    DECLARE @Modified DATETIME = GETDATE()
    INSERT INTO [dbo].[MST_QuestionLevel] 
    (
        QuestionLevel,
        UserID,
        Modified
    )
    VALUES
    (
        @QuestionLevel,
        @UserID,
        @Modified
    )
END

-- Stored Procedures for MST_QuestionLevel Table Update
-- EXEC PR_MST_QuestionLevel_Update 1,10,2
CREATE OR ALTER PROC PR_MST_QuestionLevel_Update
		@QuestionLevelID	INT,
		@QuestionLevel		NVARCHAR(100),
		@UserID				INT
AS
BEGIN
	DECLARE @Modified DATETIME = GETDATE()
    UPDATE [dbo].[MST_QuestionLevel]
    SET 
        [dbo].[MST_QuestionLevel].[QuestionLevel] = @QuestionLevel,
        [dbo].[MST_QuestionLevel].[UserID] = @UserID,
        [dbo].[MST_QuestionLevel].[Modified] = @Modified
    WHERE [dbo].[MST_QuestionLevel].[QuestionLevelID] = @QuestionLevelID
END

-- Stored Procedure for MST_QuestionLevel Table Delete
-- EXEC PR_MST_QuestionLevel_Delete 7
CREATE OR ALTER PROC PR_MST_QuestionLevel_Delete
@QuestionLevelID INT
AS
BEGIN
    DELETE FROM [dbo].[MST_QuestionLevel]
    WHERE [dbo].[MST_QuestionLevel].[QuestionLevelID] = @QuestionLevelID
END

-- Stored Procedure for MST_QuestionLevel Table Selecting All
-- EXEC PR_MST_QuestionLevel_SelectAll
CREATE OR ALTER PROC PR_MST_QuestionLevel_SelectAll
AS
BEGIN
    SELECT 
        [dbo].[MST_QuestionLevel].[QuestionLevelID],
        [dbo].[MST_QuestionLevel].[QuestionLevel],
        [dbo].[MST_QuestionLevel].[UserID],
        [dbo].[MST_QuestionLevel].[Created],
        [dbo].[MST_QuestionLevel].[Modified],
        [dbo].[MST_User].[UserName]
    FROM 
        [dbo].[MST_QuestionLevel]
    INNER JOIN 
        [dbo].[MST_User] 
    ON 
        [dbo].[MST_QuestionLevel].[UserID] = [dbo].[MST_User].[UserID]
END

-- Stored Procedure for MST_QuestionLevel Table Selecte by ID
-- EXEC PR_MST_QuestionLevel_SelectByID 1
CREATE OR ALTER PROC PR_MST_QuestionLevel_SelectByID
	@QuestionLevelID	INT
AS
BEGIN
    SELECT 
        [dbo].[MST_QuestionLevel].[QuestionLevelID],
        [dbo].[MST_QuestionLevel].[QuestionLevel],
        [dbo].[MST_QuestionLevel].[UserID],
        [dbo].[MST_QuestionLevel].[Created],
        [dbo].[MST_QuestionLevel].[Modified]
    FROM 
        [dbo].[MST_QuestionLevel]
    WHERE 
        [dbo].[MST_QuestionLevel].[QuestionLevelID] = @QuestionLevelID
END

-------------------- Stored Procedures for MST_QuizWiseQuestions Table -------------------------
-- Stored Procedures for MST_QuizWiseQuestions Table Insert
-- EXEC PR_MST_QuizWiseQuestions_Insert 1,1,2
CREATE OR ALTER PROC PR_MST_QuizWiseQuestions_Insert
    @QuizID INT,
    @QuestionID INT,
    @UserID INT
AS
BEGIN
    DECLARE @Modified DATETIME = GETDATE()
    INSERT INTO [dbo].[MST_QuizWiseQuestions] 
    (
        QuizID,
        QuestionID,
        UserID,
        Modified
    )
    VALUES
    (
        @QuizID,
        @QuestionID,
        @UserID,
        @Modified
    )
END

-- Stored Procedures for MST_QuizWiseQuestions Table Update
--EXEC PR_MST_QuizWiseQuestions_Update 1,1,1,2
CREATE OR ALTER PROC PR_MST_QuizWiseQuestions_Update
		@QuizWiseQuestionsID	INT,
		@QuizID					INT,
		@QuestionID				INT,
		@UserID					INT
AS
BEGIN
		DECLARE @Modified DATETIME = GETDATE()
	UPDATE [dbo].[MST_QuizWiseQuestions]
	SET 
		[dbo].[MST_QuizWiseQuestions].[QuizID]					=	@QuizID,
		[dbo].[MST_QuizWiseQuestions].[QuestionID]				=	@QuestionID,
		[dbo].[MST_QuizWiseQuestions].[UserID]					=	@UserID,
		[dbo].[MST_QuizWiseQuestions].[Modified]				=	@Modified
	WHERE [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID] = @QuizWiseQuestionsID
END	

-- Stored Procedures for MST_QuizWiseQuestions Table Delete
-- EXEC PR_MST_QuizWiseQuestions_Delete
CREATE OR ALTER PROC PR_MST_QuizWiseQuestions_Delete
	@QuizWiseQuestionsID INT
AS
BEGIN
	DELETE FROM [dbo].[MST_QuizWiseQuestions]
	WHERE [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID] = @QuizWiseQuestionsID
END

-- Stored Procedures for MST_QuizWiseQuestions Table Select All
-- EXEC PR_MST_QuizWiseQuestions_SelectAll
CREATE OR ALTER PROC PR_MST_QuizWiseQuestions_SelectAll
AS
BEGIN
	SELECT 
		[dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID],
		[dbo].[MST_QuizWiseQuestions].[QuizID],
		[dbo].[MST_QuizWiseQuestions].[QuestionID],
		[dbo].[MST_QuizWiseQuestions].[UserID],
		[dbo].[MST_QuizWiseQuestions].[Created],
		[dbo].[MST_QuizWiseQuestions].[Modified],
		[dbo].[MST_Quiz].[QuizName],
		[dbo].[MST_Quiz].[TotalQuestions],
		[dbo].[MST_Question].[QuestionText]
	FROM [dbo].[MST_QuizWiseQuestions]
	INNER JOIN [dbo].[MST_Quiz] ON [dbo].[MST_QuizWiseQuestions].[QuizID] = [dbo].[MST_Quiz].[QuizID]
	INNER JOIN [dbo].[MST_Question] ON [dbo].[MST_QuizWiseQuestions].[QuestionID] = [dbo].[MST_Question].[QuestionID]
END
exec PR_MST_Question_SelectAll
exec PR_MST_Quiz_SelectAll

-- Stored Procedures for MST_QuizWiseQuestions Table Select By Id
-- EXEC PR_MST_QuizWiseQuestions_SelectByID 1
CREATE OR ALTER PROC PR_MST_QuizWiseQuestions_SelectByID
	@QuizWiseQuestionsID INT
AS
BEGIN
	SELECT 
		[dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID],
		[dbo].[MST_QuizWiseQuestions].[QuizID],
		[dbo].[MST_QuizWiseQuestions].[QuestionID],
		[dbo].[MST_QuizWiseQuestions].[UserID],
		[dbo].[MST_QuizWiseQuestions].[Created],
		[dbo].[MST_QuizWiseQuestions].[Modified]
	FROM [dbo].[MST_QuizWiseQuestions]
	WHERE [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID] = @QuizWiseQuestionsID
END



---------------------------------< ======= Dropdown queris ======= >---------------------------------------
------------------Dropdown query for MST_User table--------------------
-- Exec Dropdown_MST_User
CREATE OR ALTER PROC PR_MST_User_Dropdown
AS
BEGIN
	SELECT
		UserID,
		UserName
	FROM [dbo].[MST_User]
	ORDER BY UserName ASC
END

---------Dropdown query for MST_Quiz table---------
-- Exec Dropdown_MST_Quiz
CREATE OR ALTER PROC PR_MST_Quiz_Dropdown
AS
BEGIN
	SELECT
		QuizID,
		QuizName
	FROM [dbo].[MST_Quiz]
	ORDER BY QuizName ASC
END

---------Dropdown query for MST_Question table---------
-- Exec Dropdown_MST_Question
CREATE OR ALTER PROC PR_MST_Question_Dropdown
AS
BEGIN
	SELECT
		QuestionID,
		QuestionText
	FROM [dbo].[MST_Question]
	ORDER BY QuestionText ASC
END

---------Dropdown query for MST_QuestionLevel table---------
-- Exec Dropdown_MST_QuestionLevel
CREATE OR ALTER PROC PR_MST_QuestionLevel_Dropdown
AS
BEGIN
	SELECT
		QuestionLevelID,
		QuestionLevel
	FROM [dbo].[MST_QuestionLevel]
	ORDER BY QuestionLevel ASC
END

exec PR_MST_QuestionLevel_Insert 'Easy',2
exec PR_MST_QuestionLevel_Insert 'Medium',2
exec PR_MST_QuestionLevel_Insert 'Hard',2
exec PR_MST_QuestionLevel_Insert 'Expert',2

exec PR_MST_Question_Insert 'who are you ?',8,'eevev','aevvsv','wert','wesw','Option A',5,2


---------------Count-----------------
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

exec PR_Question_CountByLevel


------------------------------------------------------------------------------------------------
--model with data anomation
--select all column name
--Scroll
--add user
--user id