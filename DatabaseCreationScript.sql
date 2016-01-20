CREATE TABLE Issue (
Id INT IDENTITY (1,1) PRIMARY KEY,
Description varchar(1000)
)

CREATE TABLE [User] (
Id INT IDENTITY (1,1) PRIMARY KEY,
FirstName varchar(50),
LastName varchar(50),
UserName varchar(50)
)

CREATE TABLE Team (
Id INT IDENTITY (1,1) PRIMARY KEY,
Name varchar(50)
)

CREATE TABLE Status (
Id INT IDENTITY (1,1) PRIMARY KEY,
Name varchar(50)
)

CREATE TABLE TeamUser (
Team_Id INT CONSTRAINT FK_Team_TeamUser FOREIGN KEY REFERENCES Team(Id) ON DELETE CASCADE,
User_Id INT CONSTRAINT FK_User_TeamUser FOREIGN KEY REFERENCES [User](Id) ON DELETE CASCADE,
CONSTRAINT PK_Team_User PRIMARY KEY (Team_Id, User_Id) 
)

CREATE TABLE WorkItem (
Id INT IDENTITY (1,1) PRIMARY KEY,
User_Id INT CONSTRAINT FK_User FOREIGN KEY REFERENCES [User](Id) 
									ON DELETE SET NULL
									ON UPDATE CASCADE,
Team_Id INT CONSTRAINT FK_Team FOREIGN KEY REFERENCES Team(Id) 
									ON DELETE SET NULL
									ON UPDATE CASCADE,
Status_Id INT CONSTRAINT FK_Status FOREIGN KEY REFERENCES Status(Id) NOT NULL,
Issue_Id INT CONSTRAINT FK_Issue FOREIGN KEY REFERENCES Issue(Id) 
									ON DELETE SET NULL
									ON UPDATE CASCADE,
Title varchar(50),
Description varchar(500),
DateCreated date NOT NULL,
DateFinished date
)


INSERT INTO [User](FirstName, LastName, UserName)
VALUES (
'Therese',
'Nyberg',
'Tessilago'
),
(
'Anna',
'Verlehag',
'AnnaV88'
),
(
'Andreas',
'Dellrud',
'Genrael'
)

INSERT INTO Team(Name)
VALUES (
'Tigers'
),
(
'Rabbits'
)

INSERT INTO Status(Name)
VALUES(
'Created'
),
(
'In Progress'
),
(
'Done'
)

INSERT INTO TeamUser(User_Id, Team_Id)
VALUES
(1,2),
(2,2),
(3,1)

INSERT INTO Issue(Description)
VALUES
('This doesn''t work!')

INSERT INTO WorkItem(User_Id, Team_Id, Status_Id, Issue_Id, Title, Description, DateCreated, DateFinished)
VALUES
(
3,
1,
2,
1,
'Fix the database',
'Please fix the stupid database',
GetDate(),
null
),
(
2,
2,
3,
null,
'Finish repositories',
'Write all the logic for data handling',
GetDate(),
GetDate()
)