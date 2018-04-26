CREATE DATABASE KMDaycare
GO
USE KMDaycare
GO

CREATE TABLE [Event](
	EventID int IDENTITY(1,1) NOT NULL,	StartDateTime DATETIME NOT NULL,
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
	ChildFirstName varchar(50) NOT NULL,
	ChildLastName varchar(50) NOT NULL,
	Parent1FirstName varchar(50) NOT NULL,
	Parent1LastName varchar(50) NOT NULL,
	Parent2FirstName varchar(50) NULL,
	Parent2LastName varchar(50) NULL,
	HomeAddress varchar(50) NULL,
	PostalCode varchar(6) NULL,
	ContactNumber varchar(10) NOT NULL,
) 
CREATE TABLE [User](
	Email varchar(50) NOT NULL PRIMARY KEY,
	UserName varchar(50) NOT NULL FOREIGN KEY REFERENCES Member(UserName),
	Password varchar(100) NOT NULL,
	Role int NOT NULL FOREIGN KEY REFERENCES Role(RoleID),
	Active int NOT NULL
)

CREATE TABLE [PasswordChangeTicket](
	TicketID INT PRIMARY KEY IDENTITY(1,1), 
	Email varchar(50) NOT NULL FOREIGN KEY REFERENCES [User](Email),
	Ticket varchar(10) NOT NULL,
	Expiry DateTime NOT NULL
)

CREATE TABLE [Class](
	ClassID INT PRIMARY KEY IDENTITY(1,1), 
	ClassDescription varchar(50) NOT NULL
)

CREATE TABLE [DailyActivity](
	DailyActivityID INT PRIMARY KEY IDENTITY(1,1), 
	StartDateTime DateTime NOT NULL,
	EndDateTime DateTime NOT NULL,
	ClassID INT NOT NULL FOREIGN KEY REFERENCES [Class](ClassID),
	DescriptionOfActivity varchar(100) NOT NULL,
	Notes varchar(500) NOT NULL,
)

--------------------------------------EVENTS-------------------------------------------
GO
CREATE PROCEDURE FindAvailability(@StartDateTime DATETIME, @EndDateTime DATETIME) AS
DECLARE @ReturnCode INT
SET @ReturnCode = 1
IF @StartDateTime IS NULL
	RAISERROR('FindAvailability - Required Parameter : @StartDateTime',16,1)
IF @EndDateTime IS NULL
	RAISERROR('FindAvailability - Required Parameter : @EndDateTime',16,1)
ELSE
	BEGIN
		SELECT EventID, StartDateTime, EndDateTime, [Description], Notes
		FROM Event
		WHERE ((StartDateTime = @StartDateTime) -- event starts at the same time as another
		OR(EndDateTime = @EndDateTime) -- event ends at the same time as another
		OR(StartDateTime < @EndDateTime AND EndDateTime > @EndDateTime) -- event is inside another event completely
		OR(StartDateTime > @StartDateTime AND EndDateTime < @EndDateTime) -- event engulfs another event completely
		OR(StartDateTime < @EndDateTime AND StartDateTime > @EndDateTime) -- event start is inside another event
		OR(EndDateTime < @StartDateTime AND EndDateTime > @EndDateTime)) -- event end is inside another event 
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
	SELECT * FROM [User] WHERE UserName = @UserName AND Password = @Password AND Active = 1
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
CREATE PROCEDURE GetUserByEmail(@Email varchar(50)) AS
DECLARE @ReturnCode INT
SET @ReturnCode = 1
IF @Email IS NULL
	RAISERROR('GetUserByEmail - Required Parameter : @Email',16,1)
BEGIN
	SELECT * FROM [User] WHERE Email = @Email
	IF @@ERROR = 0
			SET @ReturnCode = 0
	ELSE
		RAISERROR('GetUserByEmail - Select Error at User table', 16,1)
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

GO
CREATE PROCEDURE GetMember(@UserName varchar(50)) AS
DECLARE @ReturnCode INT
SET @ReturnCode = 1
IF @UserName IS NULL
	RAISERROR('GetMember - Required Parameter : @UserName',16,1)
BEGIN
	SELECT * FROM [Member] WHERE Username = @UserName
	IF @@ERROR = 0
			SET @ReturnCode = 0
	ELSE
		RAISERROR('GetUser - Select Error at Member table', 16,1)
END

GO
CREATE PROCEDURE SearchMembers(@SearchQuery varchar(50)) AS
DECLARE @ReturnCode INT
SET @ReturnCode = 1
IF @SearchQuery IS NULL
	RAISERROR('SearchMembers - Required Parameter : @SearchQuery',16,1)
BEGIN
	SELECT * FROM [Member] 
	WHERE Username LIKE '%' + @SearchQuery + '%' 
	OR ChildLastName LIKE '%' + @SearchQuery + '%' 
	OR Parent1LastName = '%' + @SearchQuery + '%' 
	OR Parent2LastName = '%' + @SearchQuery + '%' 
	OR EmergencyContact = '%' + @SearchQuery + '%'
	IF @@ERROR = 0
			SET @ReturnCode = 0
	ELSE
		RAISERROR('GetUser - Select Error at Member table', 16,1)
END

GO
CREATE PROCEDURE UpdateMember(@userName varchar(20), @childFirstName varchar(20), @childLastName varchar(20), @parent1FirstName varchar(20), @parent1LastName varchar(20), @parent2FirstName varchar(20), @parent2LastName varchar(20), @homeAddress varchar(20), @postalCode varchar(6), @emergencyContact varchar(10)) AS
DECLARE @ReturnCode INT
SET @ReturnCode = 1
IF @userName IS NULL
	RAISERROR('UpdateMember - Required Parameter : @userName',16,1)
IF @childFirstName IS NULL
	RAISERROR('UpdateMember - Required Parameter : @childFirstName',16,1)
IF @childLastName IS NULL
	RAISERROR('UpdateMember - Required Parameter : @childLastName',16,1)
IF @parent1FirstName IS NULL
	RAISERROR('UpdateMember - Required Parameter : @parent1FirstName',16,1)
IF @parent1LastName IS NULL
	RAISERROR('UpdateMember - Required Parameter : @parent1LastName',16,1)
ELSE
	BEGIN
		UPDATE [Member]
		SET ChildFirstName = @childFirstName
			,ChildLastName = @childLastName
			,Parent1FirstName = @parent1FirstName
			,Parent1LastName = @parent1LastName
			,Parent2FirstName = @parent2FirstName
			,Parent2LastName = @parent2LastName
			,HomeAddress = @homeAddress
			,PostalCode = @postalCode
			,EmergencyContact = @emergencyContact
			WHERE UserName = @userName
		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('UpdateMember - Update Error at Member table', 16,1)
	END
RETURN @ReturnCode

GO
CREATE PROCEDURE ToggleUserActiveStatus(@userName varchar(50), @active int) AS
DECLARE @ReturnCode INT
SET @ReturnCode = 1
IF @userName IS NULL
	RAISERROR('ToggleUserActiveStatus - Required Parameter : @userName',16,1)
ELSE
	BEGIN
		UPDATE [User]
		SET Active = @active
		WHERE UserName = @userName
		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('UpdateMember - Update Error at Member table', 16,1)
	END
RETURN @ReturnCode
-------------------------------------------------/MEMBER----------------------------------------------------

----------------------------------------------PASSWORD TICKETS----------------------------
GO
CREATE PROCEDURE CreateTicket(@email varchar(50), @ticket varchar(10)) AS
DECLARE @ReturnCode INT
SET @ReturnCode = 1
IF @email IS NULL
	RAISERROR('CreateTicket - Required Parameter : @email',16,1)
IF @ticket IS NULL
	RAISERROR('CreateTicket - Required Parameter : @ticket',16,1)
ELSE
	BEGIN
		INSERT INTO [PasswordChangeTicket](Email,Ticket,Expiry)
		VALUES (@email, @ticket, (SELECT DATEADD(DAY, 1, GETDATE())))
		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('CreateTicket - Insert Error at Password Ticket table', 16,1)
	END
RETURN @ReturnCode

GO
CREATE PROCEDURE GetTicket(@ticket varchar(10)) AS
DECLARE @ReturnCode INT
SET @ReturnCode = 1
IF @ticket IS NULL
	RAISERROR('GetTicket - Required Parameter : @ticket',16,1)
BEGIN
	SELECT * FROM [PasswordChangeTicket] WHERE Ticket = @ticket
	ORDER BY Expiry DESC
	IF @@ERROR = 0
			SET @ReturnCode = 0
	ELSE
		RAISERROR('GetTicket - Select Error at Password Ticket table', 16,1)
END

GO
CREATE PROCEDURE UpdatePassword(@email varchar(50), @password varchar(100)) AS
DECLARE @ReturnCode INT
SET @ReturnCode = 1
IF @email IS NULL
	RAISERROR('UpdatePassword - Required Parameter : @email',16,1)
IF @password IS NULL
	RAISERROR('UpdatePassword - Required Parameter : @password',16,1)
BEGIN
	UPDATE [User]
	SET Password = @password
	WHERE Email = @email
	IF @@ERROR = 0
			SET @ReturnCode = 0
	ELSE
		RAISERROR('UpdatePassword - Update Error at User table', 16,1)
END
-------------------------------------------------/PASSWORD CHANGE TICKET-----------------------------------

-------------------------------------------------CLASS----------------------------------------------------
GO
CREATE PROCEDURE AddClass( @ClassID int , @ClassDescription varchar(50)) AS
DECLARE @ReturnCode INT
SET @ReturnCode = 1
	BEGIN
		INSERT INTO Class( ClassID, ClassDescription)
		VALUES (@ClassID, @ClassDescription) 
		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('AddAcitvity - Insert Error at Class table', 16,1)
	END
RETURN @ReturnCode

-------------------------------------------------/CLASS----------------------------------------------------

--------------------------------------------------DAILY ACTIVITIES ----------------------------------------
GO
CREATE PROCEDURE AddDailyActivity(@StartDateTime DATETIME, @EndDateTime DATETIME, @Notes VARCHAR(500), @DescriptionOfActivity VARCHAR(50), @ClassID int) AS
DECLARE @ReturnCode INT
SET @ReturnCode = 1
IF @StartDateTime IS NULL
	RAISERROR('AddDailyActivity - Required Parameter : @StartDateTime',16,1)
IF @EndDateTime IS NULL
	RAISERROR('AddDailyActivity - Required Parameter : @EndDateTime',16,1)
IF @DescriptionofActivity IS NULL
	RAISERROR('AddDailyActivity - Required Parameter : @Description',16,1)
ELSE
	BEGIN
		INSERT INTO DailyActivity(StartDateTime, EndDateTime, DescriptionOfActivity, Notes, ClassID)
		VALUES (@StartDateTime, @EndDatetime, @DescriptionOfActivity, @Notes, @ClassID) 
		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('AddAcitvity - Insert Error at DailyActivity table', 16,1)
	END
RETURN @ReturnCode

GO
CREATE PROCEDURE FindAvailabilityForDailyActivity(@StartDateTime DATETIME, @EndDateTime DATETIME, @ClassID int) AS
DECLARE @ReturnCode INT
SET @ReturnCode = 1
IF @StartDateTime IS NULL
	RAISERROR('FindAvailabilityfordailyactivity - Required Parameter : @StartDateTime',16,1)
IF @EndDateTime IS NULL
	RAISERROR('FindAvailabilityfordailyactivity - Required Parameter : @EndDateTime',16,1)
IF @ClassID IS NULL
	RAISERROR('FindAvailabilityfordailyactivity - Required Parameter : @EndDateTime',16,1)
ELSE
	BEGIN
		SELECT DailyActivityID, StartDateTime, EndDateTime, [DescriptionofActivity], Notes, ClassID
		FROM DailyActivity 
		WHERE ClassID = @ClassID
		AND((StartDateTime = @StartDateTime) -- activity starts at the same time as another
		OR(EndDateTime = @EndDateTime) -- activity ends at the same time as another
		OR(StartDateTime < @EndDateTime AND EndDateTime > @EndDateTime) -- activity is inside another event completely
		OR(StartDateTime > @StartDateTime AND EndDateTime < @EndDateTime) -- activity engulfs another event completely
		OR(StartDateTime < @EndDateTime AND StartDateTime > @EndDateTime) -- activity start is inside another event
		OR(EndDateTime < @StartDateTime AND EndDateTime > @EndDateTime)) -- activity end is inside another event
		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('FindAvailability - Select Error from DailyAcivity table', 16,1)
	END
RETURN @ReturnCode

GO
CREATE PROCEDURE GetDailyActivities(@minDay Datetime, @maxDay Datetime) AS
DECLARE @ReturnCode INT
SET @ReturnCode = 1
IF @minDay IS NULL
	RAISERROR(' GetDailyActivities - Required Parameter : @minDay',16,1)
IF @maxDay IS NULL
	RAISERROR(' GetDailyActivities - Required Parameter : @maxDay',16,1)
ELSE
	BEGIN
		SELECT DailyActivityID, StartDateTime, EndDateTime, [DescriptionofActivity], Notes, Class.ClassDescription
		FROM DailyActivity inner join Class on  DailyActivity.ClassID = Class.ClassID
		WHERE StartDateTime BETWEEN @minDay AND @maxDay
		ORDER BY StartDateTime 
		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('GetDailyActivities  - Select Error from DailyAcitvity table', 16,1)
	END
RETURN @ReturnCode

GO
CREATE PROCEDURE DeleteActivity(@DailyActivityID int) AS
DECLARE @ReturnCode INT
SET @ReturnCode = 1
IF @DailyActivityID IS NULL
	RAISERROR('DeleteActivity - Required Parameter : @eventID',16,1)
ELSE
	BEGIN
		DELETE FROM [DailyActivity]  WHERE DailyActivityID = @DailyActivityID
		IF @@ERROR = 0
			SET @ReturnCode = 0
		ELSE
			RAISERROR('DeleteActivity - Delete Error from DailyActivity table', 16,1)
	END
	RETURN @ReturnCode
---------------------------------------------------/DAILY ACTIVITIES-----------------------------------------


