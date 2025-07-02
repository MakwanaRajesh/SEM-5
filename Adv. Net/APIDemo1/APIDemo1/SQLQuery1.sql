use MyDB

CREATE TABLE Students (
    Id INT PRIMARY KEY,
    StudentName VARCHAR(255) NOT NULL,
    StudentGender VARCHAR(10),
    Age INT,
	Standard INT,
    FatherName VARCHAR(255) NOT NULL
);

INSERT INTO Students (Id, StudentName, StudentGender, Age, Standard, FatherName) VALUES
(1, 'Maya', 'Female', 17, 10, 'Rajesh'),
(2, 'Sameer', 'Male', 20, 12, 'Arjun'),
(3, 'Pooja', 'Female', 18, 11, 'Suresh'),
(4, 'Vikram', 'Male', 16, 9, 'Devraj'),
(5, 'Neha', 'Female', 19, 11, 'Girish'),
(6, 'Karan', 'Male', 21, 12, 'Anil'),
(7, 'Swati', 'Female', 17, 10, 'Vivek'),
(8, 'Arjun', 'Male', 18, 11, 'Ravi'),
(9, 'Rina', 'Female', 16, 9, 'Mohan'),
(10, 'Gautam', 'Male', 22, 12, 'Prakash');







------ For Hospital API


CREATE TABLE HospitalMaster (
    HospitalID INT PRIMARY KEY,
    HospitalName VARCHAR(150) NOT NULL,
    HospitalAddress VARCHAR(250) NOT NULL,
    ContactNumber VARCHAR(10),
    EmailAddress VARCHAR(250),
    OwnerName VARCHAR(250) NOT NULL,
    OpeningDate DATETIME NOT NULL,
    TotalStaffs INT NOT NULL,
    SundayOpen BIT NOT NULL
);




INSERT INTO HospitalMaster 
(HospitalID, HospitalName, HospitalAddress, ContactNumber, EmailAddress, OwnerName, OpeningDate, TotalStaffs, SundayOpen)
VALUES
(1, 'Sunrise Hospital', '123 MG Road, Mumbai', '9876543210', 'info@sunrise.com', 'Dr. Mehta', '2010-06-01', 150, 1),
(2, 'City Care', '78 Link Road, Delhi', '9823456789', 'contact@citycare.in', 'Dr. Sharma', '2015-09-10', 90, 0),
(3, 'LifeLine Medicals', '12 Park Street, Kolkata', '9988776655', 'lifeline@hospital.com', 'Dr. Sen', '2018-02-20', 110, 1);





CREATE PROCEDURE PR_Hospital_Insert
    @HospitalID INT,
    @HospitalName VARCHAR(150),
    @HospitalAddress VARCHAR(250),
    @ContactNumber VARCHAR(10),
    @EmailAddress VARCHAR(250),
    @OwnerName VARCHAR(250),
    @OpeningDate DATETIME,
    @TotalStaffs INT,
    @SundayOpen BIT
AS
BEGIN
    INSERT INTO HospitalMaster (
        HospitalID, HospitalName, HospitalAddress, ContactNumber, 
        EmailAddress, OwnerName, OpeningDate, TotalStaffs, SundayOpen
    )
    VALUES (
        @HospitalID, @HospitalName, @HospitalAddress, @ContactNumber,
        @EmailAddress, @OwnerName, @OpeningDate, @TotalStaffs, @SundayOpen
    )
END




CREATE PROCEDURE PR_Hospital_Update
    @HospitalID INT,
    @HospitalName VARCHAR(150),
    @HospitalAddress VARCHAR(250),
    @ContactNumber VARCHAR(10),
    @EmailAddress VARCHAR(250),
    @OwnerName VARCHAR(250),
    @OpeningDate DATETIME,
    @TotalStaffs INT,
    @SundayOpen BIT
AS
BEGIN
    UPDATE HospitalMaster
    SET 
        HospitalName = @HospitalName,
        HospitalAddress = @HospitalAddress,
        ContactNumber = @ContactNumber,
        EmailAddress = @EmailAddress,
        OwnerName = @OwnerName,
        OpeningDate = @OpeningDate,
        TotalStaffs = @TotalStaffs,
        SundayOpen = @SundayOpen
    WHERE HospitalID = @HospitalID
END





CREATE PROCEDURE PR_Hospital_Delete
    @HospitalID INT
AS
BEGIN
    DELETE FROM HospitalMaster
    WHERE HospitalID = @HospitalID
END



CREATE PROCEDURE PR_Hospital_SelectAll
AS
BEGIN
    SELECT * FROM HospitalMaster
END






CREATE PROCEDURE PR_Hospital_SelectByID
    @HospitalID INT
AS
BEGIN
    SELECT * FROM HospitalMaster
    WHERE HospitalID = @HospitalID
END



