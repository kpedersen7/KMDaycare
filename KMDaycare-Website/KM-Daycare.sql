--CREATE User[aspnet] for login
--[NAIT\webbaist$]
--GRANT Execute on AddEvent to [aspnet]
--GRANT Execute on DeleteEvent to [aspnet]
--GRANT Execute on FindAvailability to [aspnet]
--GRANT Execute on GetEvents to [aspnet]
SELECT * FROM [Member]
SELECT * FROM [User]

CREATE DATABASE KMDaycare
GO
USE KMDaycare
GO

CREATE TABLE [Event](
	EventID int IDENTITY(1,1) NOT NULL,
	StartDateTime DATETIME NOT NULL,
	EndDateTime DATETIME NOT NULL,
	[Description] VARCHAR(100) NOT NULL,
	Notes VARCHAR(500) NULL
)

CREATE TABLE [Role](
	RoleID int NOT NULL PRIMARY KEY,
	RoleName Varchar(50)
)
INSERT INTO [Role](RoleID, RoleName) VALUES(1, 'Admin')
INSERT INTO [Role](RoleID, RoleName) VALUES(2, 'Parent')
INSERT INTO [Role](RoleID, RoleName) VALUES(3, 'Staff')

CREATE TABLE [Member](
	UserName varchar(50) NOT NULL PRIMARY KEY,
	ChildFirstName varchar(20) NOT NULL,
	ChildLastName varchar(20) NOT NULL,
	Parent1FirstName varchar(20) NOT NULL,
	Parent1LastName varchar(20) NOT NULL,
	Parent2FirstName varchar(20) NULL,
	Parent2LastName varchar(20) NULL,
	HomeAddress varchar(20) NULL,
	PostalCode varchar(6) NULL,
	EmergencyContact varchar(10),
) 

CREATE TABLE [User](
	Email varchar(50) NOT NULL PRIMARY KEY,
	UserName varchar(50) NOT NULL FOREIGN KEY REFERENCES Member(UserName),
	Password varchar(100) NOT NULL,
	Role int NOT NULL FOREIGN KEY REFERENCES Role(RoleID),
)

--------------------------------------EVENTS-------------------------------------------
GO
CREATE PROCEDURE FindAvailability(@StartDateTime DATETIME, @EndDateTime DATETIME) AS
DECLARE @ReturnCode INT
SET @ReturnCode = 1

IF @StartDateTime IS NULL
	RAISERROR('FindAvailability - Required Parameter : @@StartDateTime',16,1)
IF @EndDateTime IS NULL
	RAISERROR('FindAvailability - Required Parameter : @@EndDateTime',16,1)
ELSE
	BEGIN
		SELECT EventID, StartDateTime, EndDateTime, [Description], Notes
		FROM Event
		WHERE StartDateTime BETWEEN @StartDateTime AND @EndDateTime 
		OR EndDateTime BETWEEN @StartDateTime AND @EndDateTime
		OR StartDateTime > @StartDateTime AND EndDateTime < @EndDateTime
		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('FindAvailability - Select Error from Event table', 16,1)
	END
	RETURN @ReturnCode
GO


CREATE PROCEDURE AddEvent(@StartDateTime DATETIME, @EndDateTime DATETIME, @Notes VARCHAR(500), @Description VARCHAR(50)) AS
DECLARE @ReturnCode INT
SET @ReturnCode = 1
IF @StartDateTime IS NULL
	RAISERROR('AddEvent - Required Parameter : @StartDateTime',16,1)
IF @EndDateTime IS NULL
	RAISERROR('AddEvent - Required Parameter : @EndDateTime',16,1)
IF @Description IS NULL
	RAISERROR('AddEvent - Required Parameter : @Description',16,1)
ELSE
	BEGIN
		INSERT INTO Event(StartDateTime, EndDateTime, [Description], Notes)
		VALUES (@StartDateTime, @EndDatetime, @Description, @Notes) 
		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('AddEvent - Insert Error at Event table', 16,1)
	END
	RETURN @ReturnCode

GO
CREATE PROCEDURE GetEvents(@minDay Datetime, @maxDay Datetime) AS
DECLARE @ReturnCode INT
SET @ReturnCode = 1
IF @minDay IS NULL
	RAISERROR('GetEvents - Required Parameter : @minDay',16,1)
IF @maxDay IS NULL
	RAISERROR('GetEvents - Required Parameter : @maxDay',16,1)
ELSE
	BEGIN
		SELECT EventID, StartDateTime, EndDateTime, [Description], Notes
		FROM Event
		WHERE StartDateTime BETWEEN @minDay AND @maxDay
		ORDER BY StartDateTime 
		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('GetEvents - Select Error from Event table', 16,1)
	END
	RETURN @ReturnCode

GO
CREATE PROCEDURE DeleteEvent(@eventID int) AS
DECLARE @ReturnCode INT
SET @ReturnCode = 1
IF @eventID IS NULL
	RAISERROR('DeleteEvent - Required Parameter : @eventID',16,1)
ELSE
	BEGIN
		DELETE FROM Event WHERE EventID = @eventID
		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('DeleteEvent - Delete Error from Event table', 16,1)
	END
	RETURN @ReturnCode
--------------------------------------/EVENTS-------------------------------------------

-------------------------------------------------MEMBER----------------------------------------------------
GO
CREATE PROCEDURE CreateMember(@userName varchar(20), @childFirstName varchar(20), @childLastName varchar(20), @parent1FirstName varchar(20), @parent1LastName varchar(20), @parent2FirstName varchar(20), @parent2LastName varchar(20), @homeAddress varchar(20), @postalCode varchar(6), @emergencyContact varchar(10)) AS
DECLARE @ReturnCode INT
SET @ReturnCode = 1
IF @userName IS NULL
	RAISERROR('CreateMember - Required Parameter : @userName',16,1)
IF @childFirstName IS NULL
	RAISERROR('CreateMember - Required Parameter : @childFirstName',16,1)
IF @childLastName IS NULL
	RAISERROR('CreateMember - Required Parameter : @childLastName',16,1)
IF @parent1FirstName IS NULL
	RAISERROR('CreateMember - Required Parameter : @parent1FirstName',16,1)
IF @parent1LastName IS NULL
	RAISERROR('CreateMember - Required Parameter : @parent1LastName',16,1)
ELSE
	BEGIN
		INSERT INTO [Member](UserName,ChildFirstName,ChildLastName,Parent1FirstName,Parent1LastName, Parent2FirstName, Parent2LastName, HomeAddress, PostalCode,EmergencyContact)
		VALUES (@userName, @childFirstName, @childLastName, @parent1FirstName, @parent1LastName, @parent2FirstName, @parent2LastName, @homeAddress, @postalCode, @emergencyContact)
		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('CreateMember - Insert Error at Member table', 16,1)
	END
	RETURN @ReturnCode

GO
CREATE PROCEDURE CreateUser(@email varchar(50), @username varchar(50), @password varchar(100), @role int) AS
DECLARE @ReturnCode INT
SET @ReturnCode = 1
IF @email IS NULL
	RAISERROR('CreateUser - Required Parameter : @email',16,1)
IF @password IS NULL
	RAISERROR('CreateUser - Required Parameter : @password',16,1)
IF @role IS NULL
	RAISERROR('CreateUser - Required Parameter : @role',16,1)
ELSE
	BEGIN
		INSERT INTO [User](Email,UserName,Password,Role)
		VALUES (@email, @username ,@password, @role)
		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('CreateUser - Insert Error at User table', 16,1)
	END
	RETURN @ReturnCode

GO
CREATE PROCEDURE VerifyLogin(@UserName varchar(50), @Password varchar(100)) AS
DECLARE @ReturnCode INT
SET @ReturnCode = 1
IF @UserName IS NULL
	RAISERROR('VerifyLogin - Required Parameter : @UserName',16,1)
IF @Password IS NULL
	RAISERROR('VerifyLogin - Required Parameter : @Password',16,1)
BEGIN
	SELECT * FROM [User] WHERE UserName = @UserName AND Password = @Password
	IF @@ERROR = 0
			SET @ReturnCode = 0
	ELSE
		RAISERROR('VerifyLogin - Select Error at User table', 16,1)
END

GO
CREATE PROCEDURE GetUser(@UserName varchar(50)) AS
DECLARE @ReturnCode INT
SET @ReturnCode = 1
IF @UserName IS NULL
	RAISERROR('GetUser - Required Parameter : @Email',16,1)
BEGIN
	SELECT * FROM [User] WHERE Username = @UserName
	IF @@ERROR = 0
			SET @ReturnCode = 0
	ELSE
		RAISERROR('GetUser - Select Error at User table', 16,1)
END

GO
CREATE PROCEDURE GetUserRole(@RoleID int) AS
DECLARE @ReturnCode INT
SET @ReturnCode = 1
IF @RoleID IS NULL
	RAISERROR('GetUserRole - Required Parameter : @RoleID',16,1)
BEGIN
	SELECT * FROM [Role] WHERE RoleID = @RoleID
	IF @@ERROR = 0
			SET @ReturnCode = 0
	ELSE
		RAISERROR('GetUserRole - Select Error at Role table', 16,1)
END
-------------------------------------------------/MEMBER----------------------------------------------------


------------------------------------------------------------------------------------------------------------
create table tbllogin
(
UserUniqueID Uniqueidentifier,  --this will generate 32 bit encoded number
UserID int NOT NULL IDENTITY (1,1),
EmailId varchar(100),
[Password] varchar(50),
RoleID int,
Primary Key(UserUniqueID),
FOREIGN KEY (RoleID) REFERENCES tblUserRole(RoleID)
)

create table tblUserRole
(
RoleID int NOT NULL,
RoleName Varchar(50)
Primary Key(RoleID)
)

insert into tblUserRole
(
RoleID,RoleName
)
values (3,'User');

insert into tbllogin
(UserUniqueID, [Password],RoleID)
values(NEWID(),'123',1)

;



