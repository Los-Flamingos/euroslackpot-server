BEGIN TRANSACTION;

CREATE TABLE Player (
	PlayerId INT IDENTITY(1, 1),
	PlayerName NVARCHAR(32) NOT NULL UNIQUE,
	Email NVARCHAR(64) NOT NULL UNIQUE,
	PhoneNumber NVARCHAR(32) NOT NULL UNIQUE,
	PRIMARY KEY(PlayerId)
);

CREATE TABLE Row (
	RowId INT IDENTITY(1, 1),
	Earnings FLOAT NULL,
	PRIMARY KEY (RowId)
)

CREATE TABLE Number (
	NumberId INT IDENTITY(1, 1),
	RowId INT NOT NULL,
	PlayerId INT NOT NULL,
	NumberType INT NOT NULL,
	Week INT NOT NULL,
	Value INT NOT NULL,
	PRIMARY KEY (NumberId),
	FOREIGN KEY(RowId) REFERENCES Row(RowId),
	FOREIGN KEY(PlayerId) REFERENCES Player(PlayerId)
);

INSERT INTO
	Player (PlayerName, Email, PhoneNumber)
VALUES
	('Maran', 'maran@euroslackpot.com', '0700111111'),
	('Ellis', 'ellis@euroslackpot.com', '0700222222'),
	('Gullberg', 'gullberg@euroslackpot.com', '0700333333'),
	('Lord Nelson', 'lordnelson@euroslackpot.com', '0700444444'),
	('Heman', 'heman@euroslackpot.com', '0700555555'),
	('Euro Slackpot Admin', 'admin@euroslackpot.com', '0700666666');

INSERT INTO
	Row (Earnings)
VALUES
	(40.45),
	(1233.12),
	(999999.65);

INSERT INTO
	Number (RowId, PlayerId, NumberType, Week, Value)
VALUES
	(1, 1, 0, datepart(ISO_WEEK, GETDATE()), 5),
	(1, 2, 0, datepart(ISO_WEEK, GETDATE()), 10),
	(1, 3, 0, datepart(ISO_WEEK, GETDATE()), 15),
	(1, 4, 0, datepart(ISO_WEEK, GETDATE()), 20),
	(1, 5, 0, datepart(ISO_WEEK, GETDATE()), 25),
	(1, 1, 1, datepart(ISO_WEEK, GETDATE()), 5),
	(1, 4, 1, datepart(ISO_WEEK, GETDATE()), 6),
	(2, 1, 0, datepart(ISO_WEEK, GETDATE()), 5),
	(2, 2, 0, datepart(ISO_WEEK, GETDATE()), 10),
	(2, 3, 0, datepart(ISO_WEEK, GETDATE()), 15),
	(2, 4, 0, datepart(ISO_WEEK, GETDATE()), 20),
	(2, 5, 0, datepart(ISO_WEEK, GETDATE()), 25),
	(2, 1, 1, datepart(ISO_WEEK, GETDATE()), 5),
	(2, 4, 1, datepart(ISO_WEEK, GETDATE()), 6),
	(3, 1, 0, datepart(ISO_WEEK, GETDATE()), 5),
	(3, 2, 0, datepart(ISO_WEEK, GETDATE()), 10),
	(3, 3, 0, datepart(ISO_WEEK, GETDATE()), 15),
	(3, 4, 0, datepart(ISO_WEEK, GETDATE()), 20),
	(3, 5, 0, datepart(ISO_WEEK, GETDATE()), 25),
	(3, 1, 1, datepart(ISO_WEEK, GETDATE()), 5),
	(3, 4, 1, datepart(ISO_WEEK, GETDATE()), 6);

COMMIT;