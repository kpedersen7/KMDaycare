--CREATE User[aspnet] for login
--[NAIT\webbaist$]
--GRANT Execute on AddEvent to [aspnet]
--GRANT Execute on DeleteEvent to [aspnet]
--GRANT Execute on FindAvailability to [aspnet]
--GRANT Execute on GetEvents to [aspnet]

CREATE DATABASE BAISTCapstoneTest
USE KMDaycare
USE kpedersen7
CREATE TABLE Event(
	EventID int IDENTITY(1,1) NOT NULL,
	StartDateTime DATETIME NOT NULL,
	EndDateTime DATETIME NOT NULL,
	[Description] VARCHAR(100) NOT NULL,
	Notes VARCHAR(500) NULL
)
 
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

	ALTER Procedure [dbo].[SP_VerifyLogin]
	@EmailID varchar(50),
	@Password varchar(100)
	AS
	BEGIN
		Select * from tbllogin where EmailId=@EmailID AND [Password] = @Password
 	END


	select * from tbllogin